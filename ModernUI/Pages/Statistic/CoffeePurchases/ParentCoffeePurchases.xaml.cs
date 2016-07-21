using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Windows.EntityWindows;
using ViewModels.Statistics.CoffeePurchases;

namespace UI.Pages.Statistic.CoffeePurchases
{
    /// <summary>
    /// Interaction logic for ParentCoffeePurchases.xaml
    /// </summary>
    public partial class ParentCoffeePurchases : UserControl
    {
        public ParentCoffeePurchases()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new CoffeePurchase().ShowDialog();
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as IndividualCoffeePurchaseViewModel;
            if (selected != null)
                new CoffeePurchase(selected.Purchase).ShowDialog();
        }
    }
}
