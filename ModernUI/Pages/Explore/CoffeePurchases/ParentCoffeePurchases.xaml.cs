using System.Windows.Controls;
using UI.Windows.EntityWindows;

namespace UI.Pages.Explore.CoffeePurchases
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
            var selected = tabs.SelectedItem as Model.Entity.CoffeePurchase;
            if (selected != null)
                new CoffeePurchase(selected).ShowDialog();
        }
    }
}
