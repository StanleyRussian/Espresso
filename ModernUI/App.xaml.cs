using System.Linq;
using System.Windows;
using Model;
using UI.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            //ViewModels.Properties.FirstLaunch = true;
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            StartupSplash _splash = new StartupSplash();
            _splash.Show();

            if (!ContextManager.Context.Accounts.Any())
            {
                FirstLaunchWindow _firstLaunch = new FirstLaunchWindow();
                _splash.Hide();
                _firstLaunch.ShowDialog();

                _splash.Show();
            }

            if (ContextManager.Context.Accounts.Any())
            {
                MainWindow _mainWindow = new MainWindow();
                _splash.Close();
                _mainWindow.ShowDialog();
            }

            Shutdown();
        }
    }
}
