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
    public class BankAccountController : ApiControllerBase
    {
        public BankAccountController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        [ActionName("bankAccount")]
        public async Task<ActionResult<ApiResponseModel<IEnumerable<BankAccountViewDto>>>> GetBankAccountAsync()
        {
            var bankAccount = await _mediator.Send(new GetBankAccountQuery());

            return await SuccessResult("Bank account data is selected", bankAccount);
        }

        [HttpPost]
        [ActionName("bankAccount")]
        public async Task<ActionResult<ApiResponseModel<string>>> AddBankAccount(AddBankAccountCommand command)
        {
            await _mediator.Send(command);
            return await SuccessResult<string>("Bank Account added successfully");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> UpdateBankAccount(int id, [FromBody] UpdateBankAccountCommand command)
        {
            if (id!=command.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _mediator.Send(command);
            return await SuccessResult<string>("BankAccount updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponseModel<string>>> DeleteBankAccount(int id)
        {
            var command = new DeleteBankAccountCommand
            {
                Id = id
            };

            try
            {
               await _mediator.Send(command);
               return await SuccessResult<string>("Delete bankAccount successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return await NotFoundResult<string>(ex.Message);
            }
            catch (Exception ex)
            {
                return await InternalServerErrorResult<string>(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccount>> GetBankAccountById(int id)
        {
            var query = new GetBankAccountByIdQuery { bankAccountId = id };
            var result = await _mediator.Send(query);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
