using System.Windows;
using System.Windows.Controls;
using ViewModels.Windows;

namespace UI.Windows
{
    /// <summary>
    /// Interaction logic for FirstLaunchWindow.xaml
    /// </summary>
    public partial class FirstLaunchWindow 
    {
        public FirstLaunchWindow()
        {
            InitializeComponent();
            DataContext = new vmWinFirstLaunch();
        }

        private void EventSetter_OnHandler(object sender, RoutedEventArgs e)
        {
            var grid = (DataGrid)sender;
            grid.CommitEdit(DataGridEditingUnit.Row, true);
        }
    }
}
