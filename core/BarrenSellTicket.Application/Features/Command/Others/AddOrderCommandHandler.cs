using AutoMapper;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public AddOrderCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var eventId = int.Parse(_httpContextAccessor.HttpContext.Request.RouteValues["eventId"].ToString());

            var ticket = await _unitOfWork.TicketRepository.GetTicketByEventIdAsync(eventId);
            if (ticket is null)
            {
                throw new SellTicketException("No tickets were found for the selected event.");
            }

            if (ticket.AvailableCount<request.Quantity)
            {
                throw new SellTicketException("Not enough tickets available.");
            }

            decimal totalAmount = ticket.Price * request.Quantity;

            var appliedCoupons = new List<Coupon>();
            if (request.CouponCodes!=null && request.CouponCodes.Any())
            {
                foreach (var couponCode in request.CouponCodes)
                {
                    var coupon = await _unitOfWork.CouponRepository.GetByCodeAsync(couponCode);
                    if (coupon is null)
                    {
                        throw new SellTicketException("Coupon code not found");
                    }
                    else if(coupon!=null)
                    {
                        if (coupon.DiscountType==DiscountType.Percent)
                        {
                            totalAmount -= (totalAmount * coupon.Discount / 100);
                        }
                        else if (coupon.DiscountType==DiscountType.FixedPrice)
                        {
                            totalAmount -= coupon.Discount;
                        }

                        appliedCoupons.Add(coupon);
                    }
                }
            }

            Address address = null; 
            if (request.Address!=null)
            {
                address = _mapper.Map<Address>(request.Address);

                await _unitOfWork.AddressRepository.AddAsync(address);
                await _unitOfWork.Commit();
            }

            var order = new Order
            {
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                Status = "Pending",
                TicketId = ticket.Id,
                Quantity = request.Quantity,
                AddressId=address?.Id,
                Ticket = ticket,
                OrderCoupons = appliedCoupons.Select(c => new OrderCoupon { CouponId = c.Id }).ToList()
            };

            try
            { 
                await _unitOfWork.OrderRepository.AddAsync(order);

                ticket.AvailableCount -= request.Quantity;
                _unitOfWork.TicketRepository.Update(ticket);

                await _unitOfWork.Commit();

                var payouts = _mapper.Map<Payouts>(request.Payout);
                payouts.OrderId = order.Id;

           
                await _unitOfWork.PayoutsRepository.AddAsync(payouts);

                await _unitOfWork.Commit();
            }
            catch (System.Exception ex)
            {

                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new SellTicketException($"Payouts kaydedilemedi: {innerMessage}");
            }
            
        }
    }
}
