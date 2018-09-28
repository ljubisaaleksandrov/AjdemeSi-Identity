using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Domain.Models.UserGroups
{
    public class UserGroupsListViewModel
    {
        public List<UserGroupViewModel> UserGroupsList { get; set; }
        public UserGroupWithMessagesViewModel CurrentUserGroup { get; set; }
    }
}
