using BasicB0t.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace BasicB0t.ViewModels
{
    public class LoggerViewModel : ObservableObject
    {
        private ObservableCollection<string> _logMessages = new ObservableCollection<string>();

        public ObservableCollection<string> LogMessages
        {
            get => _logMessages;
            set => SetProperty(ref _logMessages, value);
        }

        public LoggerViewModel()
        {
            Logger logger = Logger.GetInstance();
            logger.LogMessageLogged += OnLogMessageLogged;
            logger.Log("Logger Initializing...", LogLevel.Info);
            logger.Log("Logger Initialized", LogLevel.Info);
        }

        private void OnLogMessageLogged(object? sender, string logMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _logMessages.Add(logMessage);
            });
        }

    }
}
