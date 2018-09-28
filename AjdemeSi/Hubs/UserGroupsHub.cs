using AjdemeSi.Services.Interfaces;
using AjdemeSi.Services.Logic;
using log4net;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using AjdemeSi.Helpers;

namespace AjdemeSi.Hubs
{
    public class UserGroupsHub : Hub
    {
        private readonly IUserGroupsService _userGroups;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UserGroupsHub));

        public UserGroupsHub(IUserGroupsService userGroups)
        {
            _userGroups = userGroups;
        }

        public override Task OnConnected()
        {
            if (Context.User.Identity.IsAuthenticated)
            {
                List<int> userGroups = _userGroups.GetUserGroupsIDsByUser(Context.User.Identity.Name);
                foreach(var userGroup in userGroups)
                {
                    Groups.Add(Context.ConnectionId, userGroup.ToString());
                }
            }
            else
            {
                Groups.Add(Context.ConnectionId, "Anonymous");
            }

            return base.OnConnected();
        }

        public void SendToGroup(string userGroupId, string message)
        {
            try
            {
                int groupId;
                string userId = Context.User.Identity.GetUserId();
                if (!Int32.TryParse(userGroupId, out groupId)) 
                {
                    groupId = _userGroups.CreateGroup(userId, userGroupId);
                    userGroupId = groupId.ToString();
                    Groups.Add(Context.ConnectionId, userGroupId);
                }
                
                var messageId = _userGroups.StoreChatMessage(Context.User.Identity.GetUserId(), groupId, message);
                Clients.Group(userGroupId).sendToGroup(groupId, message, messageId, userId, DateTime.Now.ToShortDateString(), DateTime.Now.ToString("hh:mm"));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
        }

        public void CauseError(int a)
        {
            if (a < 0)
            {
                throw new HubException("HubException - {0}", a);
            }
            else
            {
                throw new InvalidOperationException("Message not sent");
            }
        }
    }
}