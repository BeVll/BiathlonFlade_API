using System.Net.WebSockets;

namespace MB_API.Handlers
{
    public class WebSocketHandler
    {
        private readonly RequestDelegate _next;

        public WebSocketHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await HandleWebSocketAsync(webSocket);
            }
            else
            {
                await _next(context);
            }
        }

        private async Task HandleWebSocketAsync(WebSocket webSocket)
        {
            // Ваш код для обробки WebSocket-з'єднання
            // Наприклад, ви можете використовувати цикл while для отримання і відправлення повідомлень.
            // Приклад можна знайти в документації ASP.NET Core.
        }
    }
}
