using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AjdemeSi.Domain.Models.UserGroups
{
    public class ChatMessageViewModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string Message { get; set; }
        public bool IsEdited { get; set; }
        public bool IsRemoved { get; set; }
        public DateTime DateCreated { get; set; }
        public string SeenBy { get; set; }
    }
}
