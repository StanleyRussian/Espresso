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

namespace ModernUI.Pages.Statistic.CoffeeSorts
{
    /// <summary>
    /// Interaction logic for ParentCoffeeSorts.xaml
    /// </summary>
    public partial class ParentCoffeeSorts : UserControl
    {
        public ParentCoffeeSorts()
        {
            InitializeComponent();
        }

        private void btnNew_CLick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Windows.CreationWindows.NewCoffeeSort().ShowDialog();
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            new Windows.EditingWindows.CoffeeSorts().ShowDialog();
        }
    }
}
