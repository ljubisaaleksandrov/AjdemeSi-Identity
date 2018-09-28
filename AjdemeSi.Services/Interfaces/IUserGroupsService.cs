using AjdemeSi.Domain.Models.UserGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Services.Interfaces
{
    public interface IUserGroupsService
    {
        UserGroupsListViewModel InitUserGroups(string userId, string email);
        List<ChatMessageViewModel> GetChatMessages(int userGroupId);
        List<ChatMessageViewModel> GetNewChatMessages(int userGroupId);
        List<object> SearchUserGroups(string query, string currentUserId);
        object GetUserGroup(int userGroupId);

        int CreateGroup(string senderId, string receiverId);
        int StoreChatMessage(string senderId, int userGroupId, string message);
        List<int> GetUserGroupsIDsByUser(string email);
        List<UserGroupShortViewModel> GetUserGroupsByUser(string email);
        UserGroupWithMessagesViewModel GetUserGroupWithMessages(int userGroupId, int userGroupLatMessageId);
        bool MessageSeen(string userId, int[] messagesIDs);
    }
}
