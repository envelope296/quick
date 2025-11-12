using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Quick.BusinessLogic.Contracts.Exceptions.Common;
using Quick.BusinessLogic.Contracts.Exceptions.Users;
using Quick.BusinessLogic.Contracts.Responses;
using System.Net;
using System.Net.Mime;
using System.Text;

namespace Quick.API.Middlewares
{
    internal sealed class ExceptionHandlerMiddleware
    {
        private static JsonSerializerSettings ErrorResponseSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InvalidInitDataException ex)
            {
                await AssembleExceptionResponseAsync(context, ex, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                await AssembleExceptionResponseAsync(context, ex, HttpStatusCode.NotFound, ex.Message);
            }
            catch (Exception ex)
            {
                await AssembleExceptionResponseAsync(context, ex, HttpStatusCode.InternalServerError, "Непредвиденная ошибка");
            }
        }

        private async Task AssembleExceptionResponseAsync<TException>(
            HttpContext context, 
            TException exception,
            HttpStatusCode errorCode,
            string errorMessage) where TException : Exception
        {
            _logger.LogError(exception, "Ошибка при обработке запроса. Причина: {errorMessage}.", exception.Message);
            context.Response.StatusCode = (int)errorCode;
            context.Response.Headers.Append(HeaderNames.ContentType, MediaTypeNames.Application.Json);

            var errorResponse = new ErrorResponse
            {
                Code = (int)errorCode,
                Message = errorMessage
            };
            var jsonErrorResponse = JsonConvert.SerializeObject(errorResponse, ErrorResponseSerializerSettings);
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(jsonErrorResponse));
        }

        public static string GenerateErrorId()
        {
            var guid = Guid.NewGuid();
            var guidBytes = guid.ToByteArray();
            var id = Convert.ToBase64String(guidBytes).Substring(0, 22);
            return id;
        }
    }
}
