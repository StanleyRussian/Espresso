using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewPackedCategory.xaml
    /// </summary>
    public partial class NewPackedCategory : Window
    {
        public NewPackedCategory()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewPackedCategoryViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
