using MahApps.Metro.Controls;
using ViewModels.Windows;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for UnpaidWindow.xaml
    /// </summary>
    public partial class UnpaidPurchasesWindow : MetroWindow
    {
        public UnpaidPurchasesWindow()
        {
            InitializeComponent();
            DataContext = new vmWinUnpaidPurchases();
        }
    }
}
