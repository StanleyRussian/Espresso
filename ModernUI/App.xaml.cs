using System.Windows;
using UI.Properties;
using UI.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            ViewModels.Properties.FirstLaunch = true;
            ShutdownMode = ShutdownMode.OnExplicitShutdown;

            if (ViewModels.Properties.FirstLaunch)
                new FirstLaunchWindow().ShowDialog();

            if (!ViewModels.Properties.FirstLaunch)
                new MainWindow().ShowDialog();
            Shutdown();
        }
    }
}
