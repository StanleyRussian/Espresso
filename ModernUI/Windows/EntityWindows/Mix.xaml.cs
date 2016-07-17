using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewMix.xaml
    /// </summary>
    public partial class Mix : MetroWindow
    {
        public Mix(object argMix = null)
        {
            InitializeComponent();
            DataContext = new MixViewModel(argMix);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
