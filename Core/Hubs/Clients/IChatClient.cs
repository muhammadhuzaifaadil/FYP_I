using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Hubs.Clients
{
    public interface IChatClient
    {
        Task ReceiveMessage(string username, string message);
        Task ReceiveUsersInRoom(string[] users);
    }
}
