using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace BookManagement.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy thời gian hiện tại
            var time = DateTime.Now;

            // Lấy Method và URL
            var method = context.Request.Method;
            var path = context.Request.Path;

            // Ghi log trước khi vào Controller
            Console.WriteLine(
                $"[{time:yyyy-MM-dd HH:mm:ss.fff}] " +
                $"Method: {method} - Path: {path}"
            );

            // Kiểm tra Book ID không hợp lệ
            if (path == "/Book/Detail/0" ||
                path == "/Book/Detail/-1")
            {
                context.Response.StatusCode = 400;

                await context.Response.WriteAsync(
                    "Book id khong hop le"
                );

                Console.WriteLine(
                    $"Status Code: {context.Response.StatusCode}"
                );

                return;
            }

            // Cho request đi tiếp
            await _next(context);

            // Ghi status code sau khi xử lý
            Console.WriteLine(
                $"Status Code: {context.Response.StatusCode}"
            );
        }
    }
}