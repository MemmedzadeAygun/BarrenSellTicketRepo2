using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Features.Command.Others;
using BarrenSellTicket.Application.Features.Command.Others.DeleteCommand;
using BarrenSellTicket.Application.Features.Command.Others.UpdateCommand;
using BarrenSellTicket.Application.Features.Queries;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarrenSellTicket.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CouponController : ApiControllerBase
    {
        public CouponController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        [ActionName("coupon")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddCoupon(AddCouponCommand command)
        {
            await _mediator.Send(command);
            return await SuccessResult<string>("Coupon added successfully");
        }

        [HttpGet]
        [ActionName("getCoupon")]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<CouponViewDto>>>> GetCoupon()
        {
            var coupon =await _mediator.Send(new GetCouponQuery());
            return await SuccessResult("Coupon data is selected", coupon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateCoupon(int id, [FromBody] UpdateCouponCommand command)
        {
            if (id!=command.Id)
            {
                return BadRequest("Id not found");
            }

            await _mediator.Send(command);
            return await SuccessResult<string>("Coupon updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> DeleteCoupon(int id)
        {
            var command = new DeleteCouponCommand
            {
                Id = id
            };

            try
            {
                await _mediator.Send(command);
                return await SuccessResult<string>("Coupon deleted successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return await NotFoundResult<string>(ex.Message);
            }
            catch(Exception ex)
            {
                return await InternalServerErrorResult<string>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon>> getCouponById(int id)
        {
            var coupon = new GetCouponByIdQuery { couponId = id };
            var result = await _mediator.Send(coupon);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
