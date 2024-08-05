using BarrenSellTicket.Application.Exception;
using System.Net;
using System.Text.Json;

namespace BarrenSellTicket.WebApi.Middlewares
{
    public class ExceptionHandlerMiddleWare
    {
        private const string AppJsonContentType = "application/json";
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                List<string> messages = new List<string>();
                var httpStatusCode = HttpStatusCode.BadRequest;

                switch (e)
                {
                 
                    case BarrenSellTicketValidationException validationException:
                        messages.AddRange(validationException.ValidationFailures.Select(x => x.ErrorMessage).ToList());
                        break;
                     default:
                        messages.Add(e.Message);
                        break;
                }

                var responseModel = new ApiResponseModel<string>
                {
                    StatusCode = (int)httpStatusCode,
                    Messages = messages,
                    Result = null
                };

                var responseAsJson = JsonSerializer.Serialize(responseModel);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = AppJsonContentType;
                await context.Response.WriteAsync(responseAsJson);
            }
        }
    }
}
