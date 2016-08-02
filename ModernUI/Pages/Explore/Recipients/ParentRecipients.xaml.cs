using System.Windows.Controls;
using UI.Windows.EntityWindows;
using ViewModels.Pages.Explore;

namespace UI.Pages.Explore.Recipients
{
    /// <summary>
    /// Interaction logic for ParentRecipients.xaml
    /// </summary>
    public partial class ParentRecipients : UserControl
    {
        public ParentRecipients()
        {
            InitializeComponent();
        }

        private void OnNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            new Recipient().ShowDialog();
            var viewModel = (vmRecipients) DataContext;
            viewModel.cmdReload.Execute(null);

        }

        private void OnEditClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = tabs.SelectedItem as Model.Entity.Recipient;
            if (selected != null)
            {
                new Recipient(selected).ShowDialog();
                var viewModel = (vmRecipients) DataContext;
                viewModel.cmdReload.Execute(null);
            }
        }
    }
}
