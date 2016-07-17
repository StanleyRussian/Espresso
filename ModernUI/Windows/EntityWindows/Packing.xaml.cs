using System.Windows;
using MahApps.Metro.Controls;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewPacking.xaml
    /// </summary>
    public partial class Packing : MetroWindow
    {
        public Packing(object argPacking = null)
        {
            InitializeComponent();
            DataContext = new PackingViewModel(argPacking);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
