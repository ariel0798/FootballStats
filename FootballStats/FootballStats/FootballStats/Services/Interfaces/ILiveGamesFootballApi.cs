﻿using Refit;
using System.Net.Http;
using System.Threading.Tasks;

namespace FootballStats.Services.Interfaces
{
    [Headers(Config.ApiKey, Config.ApiHost)]
    public interface ILiveGamesFootballApi
    {
        [Get("/fixtures/live")]
        Task<HttpResponseMessage> GetFixturesLive();
    }
}
