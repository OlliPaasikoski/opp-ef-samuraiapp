using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Storage;

namespace SamuraiApp.Core.Data
{
    public class MyLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                // File.AppendAllText(@"C:\temp\log.txt", formatter(state, exception));

                if (state is DbCommandLogData) {
                    Console.WriteLine(formatter(state, exception));
                    Console.WriteLine();
                }              
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        } 
    }
}