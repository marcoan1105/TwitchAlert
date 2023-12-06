using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;

namespace TwitchAlert.TrayIcon
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {        
        private TaskbarIcon? notifyIcon;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");                    

            NotifyIconViewModel.GuiService = new GUIService(notifyIcon);
            NotifyIconViewModel.StartAlert();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            notifyIcon?.Dispose();
            base.OnExit(e);
        }
    }
}
