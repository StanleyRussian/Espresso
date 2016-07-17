using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for Recipients.xaml
    /// </summary>
    public partial class Recipients : Window
    {
        public Recipients()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.RecipientsViewModel();
        }
    }
}
