using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for OtherProfits.xaml
    /// </summary>
    public partial class OtherProfits : Window
    {
        public OtherProfits()
        {
            InitializeComponent();
            DataContext = new ViewModels.Listing.OtherProfitsViewModel();
        }
    }
}
