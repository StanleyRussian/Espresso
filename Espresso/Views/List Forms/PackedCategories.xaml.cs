using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for PackedCategories.xaml
    /// </summary>
    public partial class PackedCategories : Window
    {
        public PackedCategories()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.PackedCategoriesViewModel();
        }
    }
}
