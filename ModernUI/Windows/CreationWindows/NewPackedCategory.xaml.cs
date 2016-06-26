using MahApps.Metro.Controls;
using System.Windows;
using Core.ViewModels.Creational;

namespace ModernUI.Windows.CreationWindows
{
    /// <summary>
    /// Interaction logic for NewPackedCategory.xaml
    /// </summary>
    public partial class NewPackedCategory : MetroWindow
    {
        public NewPackedCategory()
        {
            InitializeComponent();
            DataContext = new NewPackedCategoryViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
