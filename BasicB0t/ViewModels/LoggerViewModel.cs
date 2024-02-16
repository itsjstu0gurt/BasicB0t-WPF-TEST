using BasicB0t.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

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

        private ScrollViewer _scrollViewer;

        public ScrollViewer LogScrollViewer
        {
            get { return _scrollViewer; }
            set
            {
                _scrollViewer = value;
                OnPropertyChanged(nameof(LogScrollViewer));
            }
        }

        // Method to scroll to the bottom
        private void ScrollToBottom()
        {
            if (_scrollViewer != null)
            {
                _scrollViewer.ScrollToBottom();
            }
        }

        // Method to handle new log messages
        private void OnLogMessageLogged(object? sender, string logMessage)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _logMessages.Add(logMessage);
                ScrollToBottom(); // Scroll to the bottom after adding a new log message
            });
        }


    }
}
