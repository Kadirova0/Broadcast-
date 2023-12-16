using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Pustok.Services.Abstract;
using Pustok.Services.Concretes;
using System;
using System.Threading.Tasks;

namespace Pustok.Hubs;

[Authorize]
public class AlertHub : Hub
{
    private readonly IUserService _userService;
    private readonly IHubContext<UsersPageHub> _usersPageHubContext;

    public AlertHub(IUserService userService, IHubContext<UsersPageHub> usersPageHubContext)
    {
        _userService = userService;
        _usersPageHubContext = usersPageHubContext;
    }

    public override Task OnConnectedAsync()
    {
        _userService
            .AddCurrentUserConnection(Context.ConnectionId);

        _usersPageHubContext.Clients.All
            .SendAsync(
            "ReceiveUserStatus",
            new { UserId = _userService.CurrentUser.Id, IsOnline = true });

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        _userService
            .RemoveCurrentUserConnection(Context.ConnectionId);

        _usersPageHubContext.Clients.All
            .SendAsync(
            "ReceiveUserStatus",
            new { UserId = _userService.CurrentUser.Id, IsOnline = false });

        return base.OnDisconnectedAsync(exception);
    }


    public class BroadcastController : ControllerBase
    {
        private readonly IHubContext<AlertHub> _hub;

        public BroadcastController(IHubContext<AlertHub> hub)
        {
            _hub = hub;
        }

        [HttpGet]
        public async Task Get(string user, string message)
        {
            await _hub.Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
} 
