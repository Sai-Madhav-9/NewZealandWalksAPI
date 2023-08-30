using System.Net;

namespace NewZealandWalks.API.Middlewares
{
    public class ExeceptionHandlerMiddleware
    {
        private readonly ILogger<ExeceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExeceptionHandlerMiddleware(ILogger<ExeceptionHandlerMiddleware> logger
            ,RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);

            }
            catch(Exception ex)             
            {
                var errorId = Guid.NewGuid();

                logger.LogError(ex,$"{errorId} : {ex.Message}");

                httpContext.Response.StatusCode = (int)HttpStatusCode .InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage =" something went wrong"
                };

                await httpContext.Response.WriteAsJsonAsync(error); 
            }
        }
    }
}
