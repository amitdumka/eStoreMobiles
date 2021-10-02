using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace eStoreMobile.Features
{
    class EmailService
    {
        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync (message);
            }
            catch ( FeatureNotSupportedException fbsEx )
            {
                // Email is not supported on this device
            }
            catch ( Exception ex )
            {
                // Some other exception occurred
            }
        }
        public async Task SendEmailWithAttactment(string subject, string body, List<string> recipients, List<string>files)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                foreach ( var fileName in files )
                {
                    var file = Path.Combine (FileSystem.CacheDirectory, fileName);
                    message.Attachments.Add (new EmailAttachment (file));
                }
                await Email.ComposeAsync (message);
            }
            catch ( FeatureNotSupportedException fbsEx )
            {
                // Email is not supported on this device
            }
            catch ( Exception ex )
            {
                // Some other exception occurred
            }
        }
    }
}
