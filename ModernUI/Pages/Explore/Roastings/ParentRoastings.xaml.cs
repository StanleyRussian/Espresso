using System.Windows.Controls;
using UI.Windows.EntityWindows;

namespace UI.Pages.Explore.Roastings
{
    /// <summary>
    /// Interaction logic for ParentRoastings.xaml
    /// </summary>
    public partial class ParentRoastings : UserControl
    {
        public ParentRoastings()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Roasting().ShowDialog();
        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.Roasting;
            if (selected != null)
                new Roasting(selected).ShowDialog();
        }
    }
}
