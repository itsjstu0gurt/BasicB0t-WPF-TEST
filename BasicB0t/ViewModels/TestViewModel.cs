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
       
        private readonly Logger _logger;
        public ICommand ButtonClickCommand { get; }

        public TestViewModel(Logger logger)
        {
            _logger = logger;
            ButtonClickCommand = new RelayCommand(ButtonClick);
            
        }

        private void ButtonClick()
        {
            // Add a log message
            
            _logger.Log("Button Pressed", LogLevel.Info);

        }
    }
}