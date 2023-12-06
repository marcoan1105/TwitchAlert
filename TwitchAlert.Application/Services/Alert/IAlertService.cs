using System.Threading.Tasks;
using TwitchAlert.Application.Configuration;

namespace TwitchAlert.Service.Services
{
    public interface IAlertService
    {
        Task ReadEmails(Settings settings);
        Task Worker();
    }
}
