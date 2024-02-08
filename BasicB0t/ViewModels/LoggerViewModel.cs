using BasicB0t.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace BasicB0t.ViewModels
{
    public class LoggerViewModel : ObservableObject
    {
        public Logger _logger;
        private ObservableCollection<string> _logMessages = new ObservableCollection<string>();

        public ObservableCollection<string> LogMessages
        {
            get => _logMessages;
            set => SetProperty(ref _logMessages, value);
        }

        public LoggerViewModel(Logger logger)
        {
            _logger = logger;
            logger.LogMessageLogged += OnLogMessageLogged;
            logger.Log("Logger Initialized", LogLevel.Info);
        }

        private void OnLogMessageLogged(object? sender, string logMessage)
        {
        LogMessages.Add(logMessage);
        }

    }
}
