using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Domain.Models.UserGroups
{
    public class UserGroupViewModel
    {
        public int Id { get; set; }
        public ChatMessageViewModel LastChatMessages { get; set; }
        public int NewMessagesCount { get; set; }
        public List<UserGroupMemberViewModel> UserGroupMembers { get; set; }
    }
}
