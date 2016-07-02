using System.Windows;
using Core.ViewModels.Listing;

namespace ModernUI.Windows.EditingWindows
{
    /// <summary>
    /// Interaction logic for PackedCategories.xaml
    /// </summary>
    public partial class PackedCategories
    {
        public PackedCategories()
        {
            InitializeComponent();
            DataContext = new EditPackedCategoriesViewModel();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            new CreationWindows.NewPackedCategory().ShowDialog();
        }
    }
}
