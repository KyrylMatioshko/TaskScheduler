namespace TaskSched.Middlewares
{
	public class ErrorMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ErrorMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			_next = next;
			_logger = loggerFactory.CreateLogger("ErrorLoggingMiddleware");
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Request processing error for {Method} {Path}", context.Request.Method, context.Request.Path);

				var statusCode = ex is ArgumentException ? StatusCodes.Status400BadRequest : StatusCodes.Status500InternalServerError;
				context.Response.StatusCode = statusCode;

				await context.Response.WriteAsync($"An error occurred: {ex.Message}");
			}
		}
	}
}
