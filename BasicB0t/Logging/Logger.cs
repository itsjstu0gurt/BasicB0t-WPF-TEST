using System;
using System.IO;

namespace BasicB0t.Logging
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Critical
    }

    public interface ILogEventPublisher
    {
        event EventHandler<LogEventArgs> LogEvent;
        void PublishLogEvent(string message);
    }


    public class Logger
    {
        public event EventHandler<LogEventArgs>? LogEvent;

       
        private readonly string logFile;
        private readonly object lockObj = new object();

        public Logger()
        {
            string logDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logger");

            logFile = Path.Combine(logDirectoryPath, $"{DateTime.Now:yyyy-MM-dd}.log");
            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }
        }

        public void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var formattedMessage = $"{timestamp} [{logLevel}] {message}";

            PublishLogEvent(formattedMessage);

            lock (lockObj)
            {
                try
                {
                    File.AppendAllText(logFile, formattedMessage + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to write to log file: {ex.Message}");
                }
            }
        }

        public void PublishLogEvent(string message)
        {
            LogEvent?.Invoke(this, new LogEventArgs(message));
        }

        // Convenience methods for different log levels
        public void Debug(string message) => Log(message, LogLevel.Debug);
        public void Info(string message) => Log(message, LogLevel.Info);
        public void Warning(string message) => Log(message, LogLevel.Warning);
        public void Error(string message) => Log(message, LogLevel.Error);
        public void Critical(string message) => Log(message, LogLevel.Critical);
    }

    public class LogEventArgs : EventArgs
    {
        public string LogMessage { get; }
        

        public LogEventArgs(string logMessage)
        {
            LogMessage = logMessage;
            
        }
    }
}
