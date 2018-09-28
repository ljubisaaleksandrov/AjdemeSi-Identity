using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Domain.Models.UserGroups
{
    public class UserGroupWithMessagesViewModel
    {
        public int Id { get; set; }
        public bool HasOlderMessages { get; set; }
        public IEnumerable<IGrouping<DateTime, ChatMessageViewModel>> ChatMessagesByDay { get; set; }
        public List<UserGroupMemberViewModel> UserGroupMembers { get; set; }
    }
}
