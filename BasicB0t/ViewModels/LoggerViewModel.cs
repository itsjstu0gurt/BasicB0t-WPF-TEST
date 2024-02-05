using BasicB0t.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace BasicB0t.ViewModels
{
    public class LoggerViewModel : ObservableObject
    {
        private readonly Logger Logger; // Store reference to Logger instance
        private readonly object _lockObj = new object();

        private ObservableCollection<string> _logMessages = new ObservableCollection<string>();
        public ObservableCollection<string> LogMessages
        {
            get => _logMessages;
            set => SetProperty(ref _logMessages, value);
        }

        public LoggerViewModel(Logger logger)
        {
            Logger = logger; // Store the passed logger instance
            LogMessages = new ObservableCollection<string>();

            // Subscribe to the log event
            logger.LogEvent += OnLogEvent;
            logger.Log("Logger Initialized", LogLevel.Info);
        }



        private void OnLogEvent(object? sender, LogEventArgs e)
        {
            // Add the log message to the collection
            lock (_lockObj)
            {
                LogMessages.Add(e.LogMessage);
            }
        }

    }
}
