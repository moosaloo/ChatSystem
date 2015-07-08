using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Concurrent;
using ChatSystem.Models;
using System.Threading.Tasks;

namespace ChatSystem.Hubs
{
    [Authorize]
    [HubName("chat")]
    public class ChatHub : Hub
    {
        #region SendMessageToAll
        public void SendMessage(string roomName, string message)
        {

            using (var db = new ApplicationDbContext())
            {
                var owner = db.Users.FirstOrDefault(a => a.UserName == Context.User.Identity.Name);
                db.Messages.Add(new Message { SendDateTime = DateTime.Now, RoomName = roomName, OwnerId = owner.Id, Content = message });
                db.SaveChanges();
            }
            var user = GetUser();
            Clients.Group(roomName, user.ConnectionIds.ToArray()).newMessage(Context.User.Identity.Name + ":" + message);
        }

        #endregion

        #region mapping connectionIds to system users
        private static readonly ConcurrentDictionary<string, ApplicationUser> Users =
            new ConcurrentDictionary<string, ApplicationUser>();

        public override Task OnConnected()
        {
            Connect();
            return base.OnConnected();
        }

        private void Connect()
        {
            var connectionId = Context.ConnectionId;
            var user = GetUser();
            lock (user.ConnectionIds)
            {
                user.ConnectionIds.Add(connectionId);
            }
        }

        public override Task OnReconnected()
        {
            Connect();
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userName = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;

            ApplicationUser user;
            Users.TryGetValue(userName, out user);
            if (user != null)
            {
                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));

                    if (!user.ConnectionIds.Any())
                    {
                        ApplicationUser removedUser;
                        Users.TryRemove(userName, out removedUser);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region JoinRoom
        public void JoinRoom(string roomName)
        {
            var user = GetUser();
            Clients.Group(roomName, user.ConnectionIds.ToArray()).newNotification(Context.User.Identity.Name + "وارد اتاق شد ");
            Groups.Add(Context.ConnectionId, roomName);
        }
        #endregion

        #region LeaveRoom
        public void LeaveRoom(string roomName)
        {
            var user = GetUser();
            Clients.Group(roomName, user.ConnectionIds.ToArray()).newNotification( Context.User.Identity.Name+"از اتاق خارج شد " );
            Groups.Remove(Context.ConnectionId, roomName);
        }
        #endregion

        #region GetUser


        private ApplicationUser GetUser()
        {
            var userName = Context.User.Identity.Name;
            var user = Users.GetOrAdd(userName,
               _ => new ApplicationUser
               {
                   UserName = userName,
                   ConnectionIds = new HashSet<string>()
               });

            return user;
        }
        #endregion
    }


}