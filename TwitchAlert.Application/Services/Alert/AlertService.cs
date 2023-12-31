﻿using MailKit;
using MailKit.Net.Imap;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TwitchAlert.Application.Configuration;
using TwitchAlert.Application.Services;

namespace TwitchAlert.Service.Services
{
    public class AlertService : IAlertService
    {
        private IGUIService _guiService;

        public AlertService(IGUIService guiService) {
            _guiService = guiService;
        }

        public async Task ReadEmails(Settings settings)
        {
            using (var client = new ImapClient())
            {
                var now = DateTime.Now;                

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                if (settings.IsDevelopment)
                {
                    await client.ConnectAsync(settings.Server,
                                              settings.Port, true);
                }
                else
                {
                    await client.ConnectAsync(settings.Server);
                }

                await client.AuthenticateAsync(settings.Username,
                                               settings.Password);

                var inbox = client.Inbox;

                inbox.Open(FolderAccess.ReadWrite);                

                var lastEmail = inbox.FirstUnread;

                var totalEmails = inbox.Count;

                for (var i = lastEmail; i < totalEmails; i++)
                {
                    var info = await client.Inbox.FetchAsync(new[] { i }, MessageSummaryItems.Flags | MessageSummaryItems.GMailLabels);

                    bool isRead = info.Any(e => e.Flags.Value.HasFlag(MessageFlags.Seen));

                    if (!isRead)
                    {
                        var message = await client.Inbox.GetMessageAsync(i);

                        var streamer = settings.Streamers.Where(e => message.Subject.ToUpper().Contains(settings.MessageValidation.ToUpper()) && message.Subject.ToUpper().Contains(e.EmailName.ToUpper())).FirstOrDefault();

                        if (streamer != null)
                        {
                            if (message.Date >= now.AddHours(-1))
                            {
                                bool isNotificated = await _guiService.NotificateStreamerOnline(streamer);

                                if (isNotificated)
                                {
                                    inbox.AddFlags(i, MessageFlags.Seen, true);
                                }
                            }
                            else
                            {
                                inbox.AddFlags(i, MessageFlags.Seen, true);
                            }                            
                        }
                    }
                }

                await client.DisconnectAsync(true);
            }
        }

        public async Task Worker()
        {
            Settings settings = await _guiService.GetConfiguration();

            while(true){

                await ReadEmails(settings);

                Thread.Sleep(settings.TimeVerificationMs);
            }
        }
    }
}
