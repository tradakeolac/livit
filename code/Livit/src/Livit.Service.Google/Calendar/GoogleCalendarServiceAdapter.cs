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
    using Data.Repositories;
    using Model.ServiceObjects;

    public class GoogleCalendarServiceAdapter : LeaveManagementService, ILeaveManagementService
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        private readonly IGoogleObjectFactory ObjectFactory;

        public GoogleCalendarServiceAdapter(IAsyncUnitOfWork unitOfWork, IAsyncDataLoader dataLoader, IGoogleObjectFactory objectFactory)
            : base(unitOfWork, dataLoader)
        {
            this.ObjectFactory = objectFactory;
        }

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
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });

            // Define parameters of request.
            var request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            var events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    var when = eventItem.Start.DateTime.ToString();
                    if (string.IsNullOrEmpty(when))
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

        private CalendarService CalendarService
        {
            get
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
                return new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName
                });
            }
        }

        private static async Task<TokenEntity> GetValue()
        {
            string code = "4/CjKkR1joCQfAWlx-CskRtQZvjIoC-Q6ctT9vHsZXhUo";

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

        public override async Task<bool> AddLeaveRequest(Livit.Model.ServiceObjects.LeaveServiceObject leaveObject)
        {
            //Event newEvent = new Event
            //{
            //    Summary = "Google I/O 2015",
            //    Location = "800 Howard St., San Francisco, CA 94103",
            //    Description = "A chance to hear more about Google's developer products.",
            //    Start = new EventDateTime
            //    {
            //        DateTime = DateTime.Parse("2015-05-28T09:00:00-07:00"),
            //        TimeZone = "America/Los_Angeles"
            //    },
            //    End = new EventDateTime
            //    {
            //        DateTime = DateTime.Parse("2015-05-28T17:00:00-07:00"),
            //        TimeZone = "America/Los_Angeles"
            //    },
            //    Recurrence = new string[] { "RRULE:FREQ=DAILY;COUNT=2" },
            //    Attendees = new EventAttendee[] {
            //        new EventAttendee { Email = "lpage@example.com" },
            //        new EventAttendee { Email = "sbrin@example.com" }
            //    },
            //    Reminders = new Event.RemindersData
            //    {
            //        UseDefault = false,
            //        Overrides = new EventReminder[] {
            //            new EventReminder { Method = "email", Minutes = 24 * 60 },
            //            new EventReminder { Method = "sms", Minutes = 10 }
            //        }
            //    }
            //};

            var newEvent = this.ObjectFactory.Create<Event>(leaveObject);

            string calendarId = Guid.NewGuid().ToString();

            EventsResource.InsertRequest request = CalendarService.Events.Insert(newEvent, calendarId);
            var createdEvent = await request.ExecuteAsync();

            return await Task.FromResult<bool>(createdEvent != null);
        }
    }
}
