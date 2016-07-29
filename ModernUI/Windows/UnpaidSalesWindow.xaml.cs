using MahApps.Metro.Controls;
using ViewModels.Windows;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for UnpaidSalesWindow.xaml
    /// </summary>
    public partial class UnpaidSalesWindow : MetroWindow
    {
        public UnpaidSalesWindow()
        {
            InitializeComponent();
            DataContext = new vmWinUnpaidSales();
        }
    }
}
