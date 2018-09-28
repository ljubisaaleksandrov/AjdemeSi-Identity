using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Domain.Models.UserGroups
{
    public class UserGroupShortViewModel
    {
        public int Id { get; set; }
        public int NewMessagesCount { get; set; }
        public ChatMessageViewModel LatestMessage { get; set; }
    }
}
