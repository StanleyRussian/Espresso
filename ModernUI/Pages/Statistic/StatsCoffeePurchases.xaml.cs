using System.Windows.Controls;

namespace UI.Pages.Statistic
{
    /// <summary>
    /// Interaction logic for StatsSorts.xaml
    /// </summary>
    public partial class StatsCoffeePurchases : UserControl
    {
        public StatsCoffeePurchases()
        {
            InitializeComponent();
            GridCoffee.SelectedIndex = 0;
        }
    }
}
