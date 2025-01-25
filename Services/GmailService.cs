namespace WebApplication2.Services
{
    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Gmail.v1;
    using Google.Apis.Gmail.v1.Data;
    using Google.Apis.Services;
    using Google.Apis.Util.Store;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public class GmailServiceHelper
    {
        private static readonly string[] Scopes = { GmailService.Scope.GmailReadonly, GmailService.Scope.GmailCompose };
        private static readonly string ApplicationName = "Gmail API .NET Integration";
        private static readonly string CredentialsPath = "credentials.json"; // Path to your OAuth credentials

        // Get a Gmail service instance
        public async Task<GmailService> GetGmailServiceAsync()
        {
            UserCredential credential;

            // Load OAuth 2.0 credentials
            using (var stream = new FileStream(CredentialsPath, FileMode.Open, FileAccess.Read))
            {
                var credPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    ".credentials/gmail-dotnet-quickstart.json");
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true));
            }

            // Create the Gmail API service
            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            return service;
        }

        // Fetch emails from Gmail API
        public async Task<List<Message>> GetEmailsAsync()
        {
            var service = await GetGmailServiceAsync();
            var request = service.Users.Messages.List("me");
            request.LabelIds = "INBOX"; // Only fetch emails in the inbox
            request.Q = "is:unread";    // Optional: Filter for unread emails

            var response = await request.ExecuteAsync();
            var emails = new List<Message>();

            if (response.Messages != null)
            {
                foreach (var message in response.Messages)
                {
                    var msg = await service.Users.Messages.Get("me", message.Id).ExecuteAsync();
                    emails.Add(msg);
                }
            }

            return emails;
        }
    }
}
