using System.Net;
using System.Text.Json;
using HireMeNowWebApi.Exceptions;

namespace HireMeNowWebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                var st = "";
                if (ex.StackTrace != null)
                {
                    st = ex.StackTrace.ToString();
                }
           
                var response = new ApiException(context.Response.StatusCode, ex.Message, st);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
			catch (UserAlreadyExistException ex)
			{
				_logger.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				var st = "";
				if (ex.StackTrace != null)
				{
					st = ex.StackTrace.ToString();
				}

				var response = new ApiException(context.Response.StatusCode, ex.Message, st);
				var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json = JsonSerializer.Serialize(response, options);

				await context.Response.WriteAsync(json);
			}
			
			catch (Exception ex)
            {
				switch (ex)
				{
					case NotFoundException e:
						//errorResult.StatusCode = (int)e.StatusCode;
						//if (e.ErrorMessages is not null)
						//{
						//	errorResult.Messages = e.ErrorMessages;
						//}
						context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					case KeyNotFoundException:
						context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
						break;

					default:
						context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}

				_logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var st = "";
                if(ex.StackTrace != null){
                    st = ex.StackTrace.ToString();
                }
                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, st)
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}