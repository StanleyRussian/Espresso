using System.Windows;
using ViewModels.EntityWindows;

namespace UI.Windows.EntityWindows
{
    /// <summary>
    /// Interaction logic for NewRoasting.xaml
    /// </summary>
    public partial class Roasting
    {
        public Roasting(object argRoasting = null)
        {
            InitializeComponent();
            DataContext = new RoastingViewModel(argRoasting);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
