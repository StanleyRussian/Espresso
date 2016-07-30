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
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.Mixes
{
    /// <summary>
    /// Interaction logic for ParentMixes.xaml
    /// </summary>
    public partial class ParentMixes : UserControl
    {
        public ParentMixes()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, RoutedEventArgs e)
        {
            new Mix().ShowDialog();
            var viewModel = (vmMixes)DataContext;
            viewModel.cmdReload.Execute(null);
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.CoffeeSort;
            if (selected != null)
            {
                new Mix(selected).ShowDialog();
                var viewModel = (vmMixes)DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
