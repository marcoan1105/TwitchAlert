using System.Threading.Tasks;
using TwitchAlert.Application.Configuration;

namespace TwitchAlert.Application.Services
{
    public interface IGUIService
    {
        Task<Settings> GetConfiguration();
        Task<bool> NotificateStreamerOnline(Streamer streamer);
    }
}
