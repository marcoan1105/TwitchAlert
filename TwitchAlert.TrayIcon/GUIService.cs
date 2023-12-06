using Hardcodet.Wpf.TaskbarNotification;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TwitchAlert.Application.Configuration;
using TwitchAlert.Application.Services;

namespace TwitchAlert.TrayIcon
{
    public class GUIService : IGUIService
    {
        private TaskbarIcon _notifyIcon;
        Streamer lastStreamer;

        public GUIService(TaskbarIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;

            _notifyIcon.TrayBalloonTipClicked += notifyIcon_TrayBalloonTipClicked;
        }

        public async Task<Settings> GetConfiguration()
        {
            var json = await File.ReadAllTextAsync("./appsettings.json");
            var data = JsonConvert.DeserializeObject<Settings>(json);

            return data;
        }

        public async Task<bool> NotificateStreamerOnline(Streamer streamer)
        {
            _notifyIcon.ShowBalloonTip("Notificação", string.Format("O streamer {0} está online", streamer.EmailName), BalloonIcon.Info);
            lastStreamer = streamer;
            return true;
        }

        public void notifyIcon_TrayBalloonTipClicked(object sender, EventArgs e)
        {
            OpenLastStreamer();
        }

        public void OpenLastStreamer()
        {
            if (lastStreamer == null)
            {
                _notifyIcon.ShowBalloonTip("Notificação", "Nenhum streamer foi detectado online ainda.", BalloonIcon.Info);
                return;
            }

            System.Diagnostics.Process.Start(new ProcessStartInfo(lastStreamer.Url) { UseShellExecute = true });
        }
    }
}
