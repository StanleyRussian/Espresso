using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewPacking.xaml
    /// </summary>
    public partial class NewPacking : Window
    {
        public NewPacking()
        {
            InitializeComponent();

            ViewModels.PackingsViewModel vm = new ViewModels.PackingsViewModel();
            DataContext = vm;
            comboCategory.ItemsSource = vm.PackedCategories;
            comboMix.ItemsSource = vm.Mixes;
            comboPackage.ItemsSource = vm.Packages;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
