using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ChatSignalRProject.Hubs
{
    public class Chat : Hub
    {
        private static Dictionary<string, string> users = new Dictionary<string, string>();
       public void SendMessage(string username,string message)
       {
            Clients.All.receiveMessage(username, message); //Data Enjoy for all users

       }

        public void SendMessageToRoom(string username,string message,string roomName)
        {
            Clients.Group(roomName).receiveMessage(username, message); //Data Enjoy for one Grouo Users
        }

        public void JoinRoom(string roomName)
        {
            this.Groups.Add(this.Context.ConnectionId, roomName);
        }

        public void LeaveRoom(string roomName)
        {
            this.Groups.Remove(this.Context.ConnectionId, roomName);
        }

        public void JoinChat(string username)
        {
            users[username] = this.Context.ConnectionId;

        }

        public void SendMessageToUser(string userFrom,string message, string userTo)
        {
            if (users.ContainsKey(userTo))
            {
                
                this.Clients.Client(users[userTo]).receiveMessage(userFrom, message);
            }
        }
    }
}