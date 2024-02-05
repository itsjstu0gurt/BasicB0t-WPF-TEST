using System;
using System.IO;

namespace BasicB0t.Logging
{
    public class Logger
    {
        
        public event EventHandler<string>? LogMessageLogged;
        

        private readonly string _logDirectory;
        private readonly string _logFile;
        private readonly object _lockObj = new object();

        public Logger()
        {
            // Get the path to the log directory
            _logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            // Ensure the log directory exists
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }

            // Get the path to the log file for the current day
            _logFile = Path.Combine(_logDirectory, $"{DateTime.Now:yyyy-MM-dd}.log");
        }

        public void Log(string message, LogLevel logLevel)
        {
            // Log message using your chosen logging framework or method

            // Raise event to notify subscribers about the new log message
            LogMessageLogged?.Invoke(this, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}");

            lock (_lockObj)
            {
                try
                {
                    // Ensure the log file for the current day exists
                    if (!File.Exists(_logFile))
                    {
                        // Create the log file
                        File.Create(_logFile).Close();
                    }

                    // Append the log message to the log file
                    File.AppendAllText(_logFile, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{logLevel}] {message}" + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write to log file: {ex.Message}");
                }
            }
        }
    }
    public class LogEventArgs : EventArgs
    {
        public string LogMessage { get; }

        public LogEventArgs(string logMessage)
        {
            LogMessage = logMessage;
        }
    }
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }
}
