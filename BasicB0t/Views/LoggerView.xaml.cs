using BasicB0t.ViewModels;
using BasicB0t.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BasicB0t.Views
{
    /// <summary>
    /// Interaction logic for LoggerView.xaml
    /// </summary>
    public partial class LoggerView : UserControl
    {
        public Logger Logger; // Create a new Logger instance
        public LoggerView()
        {
            InitializeComponent();
            Logger = new Logger(); // Create a new Logger instance
            DataContext = new LoggerViewModel(Logger);
        }
    }
}
