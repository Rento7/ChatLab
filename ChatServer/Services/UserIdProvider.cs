﻿using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatServer.Services
{
    public class UserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
