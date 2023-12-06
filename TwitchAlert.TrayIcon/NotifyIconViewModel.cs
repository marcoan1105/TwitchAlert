using System.Threading;
using System.Windows.Input;
using TwitchAlert.Service.Services;

namespace TwitchAlert.TrayIcon
{
    public class NotifyIconViewModel
    {
        public static GUIService GuiService;        
        public static Thread AlertThread;
        public static IAlertService alertService;

        public NotifyIconViewModel()
        {
            
        }

        public static void StartAlert()
        {
            alertService = new AlertService(GuiService);

            AlertThread = new Thread(async () => await alertService.Worker());
            AlertThread.Start();
        }
        /// <summary>
        /// Open last streamer
        /// </summary>
        public ICommand ShowWindowCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = async () =>
                    {
                        GuiService.OpenLastStreamer();
                    }
                };
            }
        }

        /// <summary>
        /// Run read emails
        /// </summary>
        public ICommand VerifyCommand
        {
            get
            {
                return new DelegateCommand
                {
                    CommandAction = async () =>
                    {
                        alertService.ReadEmails(await GuiService.GetConfiguration());
                    }
                };
            }
        }


        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public ICommand ExitApplicationCommand
        {
            get
            {
                return new DelegateCommand { CommandAction = () =>
                {
                    AlertThread.Interrupt();
                    System.Windows.Application.Current.Shutdown();
                }
                };
            }
        }
    }
}
