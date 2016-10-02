using System.Windows.Controls;

namespace UI.Pages.Statistic
{
    /// <summary>
    /// Interaction logic for StatsSorts.xaml
    /// </summary>
    public partial class StatsCoffeeStocks : UserControl
    {
        public StatsCoffeeStocks()
        {
            InitializeComponent();
            GridCoffee.SelectedIndex = 0;
        }
    }
}
