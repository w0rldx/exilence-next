namespace API.Hubs
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using API.Interfaces;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Shared.Models;

    [Authorize]
    public partial class BaseHub : Hub
    {
        private readonly IAccountService _accountService;
        private readonly IGroupService _groupService;

        private readonly string _instanceName;

        private readonly ILogger<BaseHub> _logger;
        private readonly string _loggerPassword;
        private readonly IMapper _mapper;
        private readonly ISnapshotService _snapshotService;
        private readonly Stopwatch _timer;


        public BaseHub(IMapper mapper,
            ILogger<BaseHub> logger,
            IConfiguration configuration,
            IGroupService groupService,
            ISnapshotService snapshotService,
            IAccountService accountService)
        {
            _logger = logger;
            _mapper = mapper;
            _instanceName = configuration.GetSection("Settings")["InstanceName"];
            _loggerPassword = configuration.GetSection("Logger")["Password"];

            _snapshotService = snapshotService;
            _accountService = accountService;
            _groupService = groupService;

            _timer = Stopwatch.StartNew();
        }

        private string ConnectionId => Context.ConnectionId;
        private string AccountName => Context.User.Identity.Name;
        private bool IsAdmin => Context.User.IsInRole("Admin");
        private bool IsPremium => Context.User.IsInRole("Premium");

        public override async Task OnConnectedAsync()
        {
            //Close already existing connection for the same account
            var existingConnection = await _accountService.GetConnection(AccountName);
            if (existingConnection != null)
            {
                await CloseConnection(existingConnection.ConnectionId);
            }

            var connection = new ConnectionModel
            {
                ConnectionId = ConnectionId,
                InstanceName = _instanceName
            };
            await _groupService.AddConnection(connection, AccountName);
            await base.OnConnectedAsync();
            LogDebug($"ConnectionId: {ConnectionId} connected in " + _timer.ElapsedMilliseconds + " ms.");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var groupModel = await _groupService.GetGroupForConnection(ConnectionId);
            if (groupModel != null)
            {
                await LeaveGroup(groupModel.Name);
            }

            await _groupService.RemoveConnection(ConnectionId);
            await base.OnDisconnectedAsync(exception);
            LogDebug($"ConnectionId: {ConnectionId} disconnected in " + _timer.ElapsedMilliseconds + " ms.");
        }

        public async Task CloseConnection(string connectionId)
        {
            await Clients.Client(connectionId).SendAsync("OnCloseConnection");
            LogDebug($"Told connectionId: {connectionId} to close");
        }

        private void LogDebug(string message)
        {
            message = $"[Account: {AccountName}] -  " + message;
            _logger.LogDebug(message);
        }

        private void LogError(string message)
        {
            message = $"[Account: {AccountName}] -  " + message;
            _logger.LogError(message);
        }
    }
}
