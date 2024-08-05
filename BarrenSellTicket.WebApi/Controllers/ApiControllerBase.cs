using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BarrenSellTicket.WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;
        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<ActionResult<ApiResponseModel<T>>> SuccessResult<T>()
        {
            return await AsActionResultAsync<T> (HttpStatusCode.OK,"Complated operation",default);
        }

        protected async Task<ActionResult<ApiResponseModel<T>>> SuccessResult<T>(string message)
        {
            
            return await AsActionResultAsync<T>(HttpStatusCode.OK, message, default);
        }

        protected async Task<ActionResult<ApiResponseModel<T>>> SuccessResult<T>(string message,T Result)
        {
            return await AsActionResultAsync<T>(HttpStatusCode.OK, message, Result);
        }
        protected async Task<ActionResult<ApiResponseModel<T>>> NotFoundResult<T>(string message)
        {
            return await AsActionResultAsync<T>(HttpStatusCode.NotFound, message, default);
        }
        protected async Task<ActionResult<ApiResponseModel<T>>> InternalServerErrorResult<T>(string message)
        {
            return await AsActionResultAsync<T>(HttpStatusCode.InternalServerError, message, default);
        }
        protected async Task<ActionResult<ApiResponseModel<T>>> BadRequestResult<T>(string message)
        {
            return await AsActionResultAsync<T>(HttpStatusCode.BadRequest, message, default);
        }

        private async Task<ActionResult<ApiResponseModel<T>>> AsActionResultAsync<T>(
            HttpStatusCode statusCode,string errormessage,T? result)
        {
            string? message = null;
            if(errormessage is not null)
            {
                message = errormessage;
            }

            var statusCodeAsInt = (int)statusCode;
            var responseModel = new ApiResponseModel<T>
            {
                Result = result,
                StatusCode = statusCodeAsInt,
                Messages = message is not null ? new[] { message } : Array.Empty<string>()
            };

            return StatusCode(statusCodeAsInt,responseModel);
        }
    }
}
