using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTestApi
{
    public class MyOwnHub : Hub
    {
        /// <summary>
        /// Sends data to the listeners
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);

            return base.OnConnectedAsync();
        }
    }
}
