namespace Livit.Service.Google.Calendar
{
    using global::Google.Apis.Auth.OAuth2;
    using global::Google.Apis.Auth.OAuth2.Flows;
    using global::Google.Apis.Auth.OAuth2.Responses;
    using global::Google.Apis.Calendar.v3;
    using global::Google.Apis.Calendar.v3.Data;
    using global::Google.Apis.Services;
    using Livit.Service.Google.DataStorage;
    using Livit.Service.Google.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GoogleCalendarServiceAdapter
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        static void Main(string[] args)
        {
            UserCredential credential = null;

            TokenEntity tokenA = null;

            var task = GetValue();

            task.Wait();

            tokenA = task.Result;

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = "497145328019-7dl84cpo4d6n09ce2kdh34v46kvvmfm9.apps.googleusercontent.com",
                    ClientSecret = "tSpThzSrFZRecmP2agrewfwk"
                },
                Scopes = Scopes,
                DataStore = new DatabaseDataStore(null, null, null)
            });

            var token = new TokenResponse
            {
                AccessToken = tokenA.AccessToken,
                RefreshToken = tokenA.RefreshToken
            };

            credential = new UserCredential(flow, "user", token);

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
            Console.Read();
        }

        private static async Task<TokenEntity> GetValue()
        {
            string code = "4/_9zMfY0Wgan0eP_axsxFP97wwIEE_z77B6jn786z0DQ";

            var dic = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", "497145328019-7dl84cpo4d6n09ce2kdh34v46kvvmfm9.apps.googleusercontent.com" },
                { "client_secret", "tSpThzSrFZRecmP2agrewfwk" },
                { "grant_type", "authorization_code" },
                { "redirect_uri", "urn:ietf:wg:oauth:2.0:oob" }
            };

            var formContent = new FormUrlEncodedContent(dic);

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsync("https://accounts.google.com/o/oauth2/token", formContent);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenEntity>(result);

            return await Task.FromResult(token);
        }
    }
}
