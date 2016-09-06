using System.Windows;
using UI.Windows.EntityWindows;
using ViewModels.Pages;
using CoffeePurchase = UI.Windows.EntityWindows.CoffeePurchase;
using Sale = UI.Windows.EntityWindows.Sale;
using Packing = UI.Windows.EntityWindows.Packing;
using Roasting = UI.Windows.EntityWindows.Roasting;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home
    {
        public Home()
        {
            InitializeComponent();
        }

        private void NewPurchase(object sender, RoutedEventArgs e)
        {
            new CoffeePurchase().ShowDialog();
            var ViewModel = (vmHome) DataContext;
            ViewModel.cmdReload.Execute(null);;
        }

        private void NewRoasting(object sender, RoutedEventArgs e)
        {
            new Roasting().ShowDialog();
            var ViewModel = (vmHome)DataContext;
            ViewModel.cmdReload.Execute(null); ;
        }

        private void NewPacking(object sender, RoutedEventArgs e)
        {
            new Packing().ShowDialog();
            var ViewModel = (vmHome)DataContext;
            ViewModel.cmdReload.Execute(null); ;
        }

        private void NewSale(object sender, RoutedEventArgs e)
        {
            new Sale().ShowDialog();
            var ViewModel = (vmHome)DataContext;
            ViewModel.cmdReload.Execute(null); ;
        }

        private void NewPackagePurchase(object sender, RoutedEventArgs e)
        {
            new PackagePurchase().ShowDialog();
            var ViewModel = (vmHome)DataContext;
            ViewModel.cmdReload.Execute(null); ;
        }
    }
}
