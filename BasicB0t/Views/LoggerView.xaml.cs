using BasicB0t.ViewModels;
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
using BasicB0t.Logging;

namespace BasicB0t.Views
{
    public partial class LoggerView : UserControl
    {
        public LoggerView()
        {
            InitializeComponent();           
            DataContext = new LoggerViewModel();
        }
    }
}
