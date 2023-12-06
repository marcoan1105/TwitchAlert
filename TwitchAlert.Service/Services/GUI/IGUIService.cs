using TwitchAlert.Application.Configuration;

namespace TwitchAlert.Application.Services
{
    public interface IGUIService
    {
        public Task<Settings> GetConfiguration();
        Task<bool> NotificateStreamerOnline(Streamer streamer);
    }
}
