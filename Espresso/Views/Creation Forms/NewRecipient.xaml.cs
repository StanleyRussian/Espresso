using System.Windows;

namespace Espresso.Views
{
    /// <summary>
    /// Interaction logic for NewRecipient.xaml
    /// </summary>
    public partial class NewRecipient : Window
    {
        public NewRecipient()
        {
            InitializeComponent();
            DataContext = new ViewModels.Creational.NewRecipientViewModel();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
