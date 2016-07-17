using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewPackage.xaml
    /// </summary>
    public partial class NewPackage : Window
    {
        public NewPackage()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewPackageViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
