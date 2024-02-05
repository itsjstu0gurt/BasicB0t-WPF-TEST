using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BasicB0t.Logging;
using BasicB0t.ViewModels;
using BasicB0t.Views;



namespace BasicB0t.ViewModels
{
    public class TestViewModel : ObservableRecipient
    {
        public ICommand ButtonClickCommand { get; }
        private readonly Logger logger;

        public TestViewModel(Logger logger)
        {
            ButtonClickCommand = new RelayCommand(ButtonClick);
            
        }

        private void ButtonClick()
        {
            // Add a log message
            
            logger.Log("Button Pressed", LogLevel.Info);

        }
    }
}