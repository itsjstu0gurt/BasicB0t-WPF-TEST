using BasicB0t.ViewModels;
using System.Windows.Controls;

namespace BasicB0t.Views
{
    /// <summary>
    /// Interaction logic for FollowedChannelsTabControlView.xaml
    /// </summary>
    public partial class FollowedChannelsTabControlView : UserControl
    {
        public FollowedChannelsTabControlView()
        {
            InitializeComponent();
            DataContext = new FollowedChannelsViewModel();
        }
    }
}
