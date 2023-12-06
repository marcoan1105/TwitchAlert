using System.Collections.Generic;

namespace TwitchAlert.Application.Configuration
{
    public class Settings
    {
        public string? Server { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        public int TimeVerificationMs { get; set; } = 5000;
        public bool IsDevelopment { get; set; } = true;
        public string MessageValidation { get; set; } = "";

        public List<Streamer> Streamers = new List<Streamer>();
    }
}
