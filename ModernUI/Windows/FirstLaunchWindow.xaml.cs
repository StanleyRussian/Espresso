using ViewModels.Windows;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for FirstLaunchWindow.xaml
    /// </summary>
    public partial class FirstLaunchWindow 
    {
        public FirstLaunchWindow()
        {
            InitializeComponent();
            DataContext = new vmWinFirstLaunch();
        }
    }
}
