using AjdemeSi.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace AjdemeSi.Controllers
{
    [Authorize]
    public class UserGroupsController : Controller
    {
        private readonly IUserGroupsService _userGroupsService;

        public UserGroupsController(IUserGroupsService userGroupsService)
        {
            _userGroupsService = userGroupsService;
        }

        public ActionResult Index()
        {
            var model = _userGroupsService.InitUserGroups(HttpContext.User.Identity.GetUserId(), HttpContext.User.Identity.Name);
            return View(model);
        }

        public ActionResult GetUserGroups(string term)
        {
            var data = _userGroupsService.SearchUserGroups(term, HttpContext.User.Identity.GetUserId()); 
            return Json(new { results = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserGroup(int userGroupId)
        {
            //  todo
            var data = _userGroupsService.GetUserGroup(userGroupId);
            return Json(new { group = data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUserGroupChatMessages(int userGroupId, int userGroupLatMessageId)
        {
            var model = _userGroupsService.GetUserGroupWithMessages(userGroupId, userGroupLatMessageId);
            return PartialView(userGroupLatMessageId == 0 ? "_UserGroupChatMessages" : "_UserGroupChatMessagesBody", model);
        }

        public ActionResult MessagesSeen(int[] messagesIDs)
        {
            bool result = _userGroupsService.MessageSeen(HttpContext.User.Identity.GetUserId(), messagesIDs);
            return Json(new { status = result }, JsonRequestBehavior.AllowGet);
        }
    }
}