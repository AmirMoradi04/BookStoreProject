using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace BookStore.Domain.Loggings
{
    public class FileLogger : ILogger
    {
   
        private readonly string _filePath;

        private static readonly object _lock = new();
        public FileLogger(string filePath) => _filePath = filePath;
        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel >=
        LogLevel.Information;

        public void Log<TState>(LogLevel logLevel, EventId eventId,
        TState state, Exception? exception, Func<TState, Exception?, string>
        formatter)
        {
            var entry = new
            {
                Timestamp = DateTime.UtcNow,
                Level = logLevel.ToString(),
                Message = formatter(state, exception),
                Exception = exception?.Message
            };
            var json = JsonSerializer.Serialize(entry);
            lock (_lock)
                File.AppendAllText(_filePath, json + Environment.NewLine);
        }
    }
}
