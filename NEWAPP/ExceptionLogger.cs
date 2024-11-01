using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace NEWAPP
{
    public class ExceptionLogger
    {
        private readonly string _logDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        private readonly ILogger<ExceptionLogger> _logger;

        public ExceptionLogger(ILogger<ExceptionLogger> logger)
        {
            _logger = logger;
        }

        public void LogException(Exception ex, string message)
        {
            try
            {
                // Ensure the log directory exists
                Directory.CreateDirectory(_logDirectoryPath);

                // Create or append to the log file
                string logFilePath = Path.Combine(_logDirectoryPath, $"log_{DateTime.Now:yyyyMMdd}.txt");
                using (StreamWriter writer = new StreamWriter(logFilePath, true)) // Append mode
                {
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
                    writer.WriteLine(ex.ToString());
                    writer.WriteLine(ex.Message);
                    writer.WriteLine(ex.StackTrace);
                    writer.WriteLine("----------------------------------------------------");
                }
            }
            catch (Exception fileEx)
            {
                // If logging fails, log the error with ILogger
                _logger.LogError(fileEx, "Failed to log the error to a file.");
            }
        }
    }
}
