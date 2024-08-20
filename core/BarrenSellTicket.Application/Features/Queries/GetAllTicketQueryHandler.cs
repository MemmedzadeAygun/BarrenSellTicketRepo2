using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetAllTicketQueryHandler : IRequestHandler<GetAllTicketQuery, List<TicketDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllTicketQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<TicketDto>> Handle(GetAllTicketQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _unitOfWork.TicketRepository.GetAll();
            if (tickets is null)
            {
                throw new NullReferenceException("ticket is null");
            }

            var ticketDtos = new List<TicketDto>();
            foreach (var ticket in tickets)
            {
                var ticketDto = new TicketDto
                {
                    Id = ticket.Id,
                    Price = ticket.Price,
                    AvailableCount = ticket.AvailableCount,

                    Event=new EventViewDto
                    {
                        Id=ticket.Event.Id,
                        Name=ticket.Event.Name,
                        EventDate=ticket.Event.EventDate,
                        BeginTime=ticket.Event.BeginTime,
                        EndTime=ticket.Event.EndTime,
                        Duration=ticket.Event.Duration,
                        Description=ticket.Event.Description,

                        Type=new TypeDto
                        {
                            Name=ticket.Event.EventType.Name
                        },

                        Category=new CategoryDto
                        {
                            Name=ticket.Event.EventCategory.Name
                        },

                        Address=new AddressDto
                        {
                            //Id=ticket.Event.Address.Id,
                            Country=ticket.Event.Address.Country,
                            City=ticket.Event.Address.City,
                            Addres=ticket.Event.Address.Addres
                        }
                    }
                };

                ticketDtos.Add(ticketDto);
            }

            return ticketDtos;
        }
    }
}
