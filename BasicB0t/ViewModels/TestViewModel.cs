using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using BasicB0t.Logging;

namespace BasicB0t.ViewModels
{
    public class TestViewModel : ObservableRecipient
    {
        private readonly Logger logger;
        public ICommand ButtonClickCommand { get; }

        public TestViewModel()
        {
            logger = Logger.GetInstance(); // Assigning the logger instance to the class field
            logger.Log("TestViewModel Initializing...", LogLevel.Info);
            ButtonClickCommand = new RelayCommand(ButtonClick);
            logger.Log("TestViewModel Initialized.", LogLevel.Info);
        }

        private void ButtonClick()
        {
            // Add a log message
            logger.Log("Button Pressed", LogLevel.Info);
        }
    }
}
