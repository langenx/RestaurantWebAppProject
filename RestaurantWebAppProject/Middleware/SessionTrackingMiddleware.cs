namespace RestaurantWebAppProject.Middleware
{

    public class SessionTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string LogFilePath = "session_logs.txt";

        public SessionTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //sprawdzenie czy czas wejścia został już zapisany
            if (!context.Items.ContainsKey("SessionStartTime"))
            {
                // zapisuje czas rozpoczęcia sesji
                context.Items["SessionStartTime"] = DateTime.UtcNow;
            }

            // po przetworzeniu żądania przechodzimy dalej do pipeline
            await _next(context);

            // po zakończeniu przetwarzania zapisuje czas zakończenia sesji i oblicza czas trwania
            if (context.Items.ContainsKey("SessionStartTime"))
            {
                var sessionStartTime = (DateTime)context.Items["SessionStartTime"];
                var sessionDuration = DateTime.UtcNow - sessionStartTime;

                // zapisuje czas sesji do pliku
                LogSessionTime(context, sessionDuration);
            }
        }

        private void LogSessionTime(HttpContext context, TimeSpan sessionDuration)
        {
            try
            {
                // tworzenie i dopisywanie logu
                using (StreamWriter writer = new StreamWriter(LogFilePath, append: true))
                {
                    var logEntry = $"{DateTime.UtcNow}: IP: {context.Connection.RemoteIpAddress} - Sesja trwała: {sessionDuration.TotalSeconds} sekund.";
                    writer.WriteLine(logEntry);
                }
            }
            catch (Exception ex)
            {
                // w razie bledu
                Console.WriteLine($"Błąd zapisu logu: {ex.Message}");
            }
        }
    }
}
