using System.Windows.Controls;
using Core.ViewModels;

namespace ModernUI.Pages
{
    /// <summary>
    /// Interaction logic for Editing.xaml
    /// </summary>
    public partial class Editing : UserControl
    {
        public Editing()
        {
            InitializeComponent();
            DataContext = new EditingTabsViewModel();
        }
    }
}
