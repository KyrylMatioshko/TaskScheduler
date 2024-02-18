namespace TaskSched.Middlewares
{
	public static class ErrorMiddlewareExtensions
	{
		public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ErrorMiddleware>();
		}

	}
}
