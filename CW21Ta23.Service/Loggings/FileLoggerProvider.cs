using BookStore.Domain.Loggings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Domain.Logging
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;
        public FileLoggerProvider(string filePath) => _filePath = filePath;
        public ILogger CreateLogger(string categoryName) => new
        FileLogger(_filePath);
        public void Dispose() { }
    }
}
