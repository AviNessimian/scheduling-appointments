using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace SchedulingAppointments.Filters
{
    public class ServiceExceptionInterceptorAsync : IAsyncExceptionFilter
    {
        private readonly ILogger<ServiceExceptionInterceptorAsync> logger;
        public ServiceExceptionInterceptorAsync(ILogger<ServiceExceptionInterceptorAsync> logger)
        {
            this.logger=logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var validationEx = context.Exception as ValidationException;
            if (validationEx is not null)
            {
                logger.LogWarning("Validation exception", validationEx);

                var validationError = new ErrorDetail
                {
                    Title = nameof(StatusCodes.Status400BadRequest),
                    StatusCode = StatusCodes.Status400BadRequest,
                    Extensions = validationEx?.Errors?.ToDictionary(key => key.ErrorCode, value => value.ErrorMessage),
                    Detail = $"Validation Error"
                };
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Result = new JsonResult(validationError);
                return Task.CompletedTask;
            }


            logger.LogError(context.Exception.Message, context.Exception);
            var error = new ErrorDetail
            {
                Title = nameof(StatusCodes.Status500InternalServerError),
                StatusCode = StatusCodes.Status500InternalServerError,
                Detail = $"Something went wrong! Internal Server Error-{context.Exception}"
            };

            context.Result = new JsonResult(error);
            return Task.CompletedTask;
        }
    }

    public class ErrorDetail
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Detail { get; set; }
        public Dictionary<string, string> Extensions { get; set; }
    }
}
