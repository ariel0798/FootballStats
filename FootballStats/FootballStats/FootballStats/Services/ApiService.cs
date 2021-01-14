using Refit;
using System;
using Fusillade;
using System.Net.Http;
using ModernHttpClient;
using FootballStats.Services.Interfaces;

namespace FootballStats.Services
{
    public class ApiService<T>: IApiService<T>
    {
        readonly Func<HttpMessageHandler, T> createClient;
        public ApiService(string apiBaseAddress)
        {
            createClient = messageHandler =>
            {
                var client = new HttpClient(messageHandler)
                {
                    BaseAddress = new Uri(apiBaseAddress)
                };

                return RestService.For<T>(client);
            };
        }

        public T GetApi(Priority priority)
        {
            switch (priority)
            {
                case Priority.Background:
                    return Background;

                case Priority.UserInitiated:
                    return UserInitiated;

                case Priority.Speculative:
                    return Speculative;

                default:
                    return UserInitiated;
            }
        }

        private T Background
        {
            get
            {
                return new Lazy<T>(() => createClient(
                    new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Background))).Value;
            }
        }
        private T UserInitiated
        {
            get
            {
                return new Lazy<T>(() => createClient(
                    new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.UserInitiated))).Value;
            }
        }

        private T Speculative
        {
            get
            {
                return new Lazy<T>(() => createClient(
                    new RateLimitedHttpMessageHandler(new NativeMessageHandler(), Priority.Speculative))).Value;
            }
        }
    }
}
