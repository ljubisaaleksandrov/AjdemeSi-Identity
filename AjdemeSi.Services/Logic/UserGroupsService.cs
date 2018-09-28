using AjdemeSi.Business.Helpers;
using AjdemeSi.Domain;
using AjdemeSi.Domain.Models.UserGroups;
using AjdemeSi.Services.Interfaces;
using AutoMapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace AjdemeSi.Services.Logic
{
    public class UserGroupsService : IUserGroupsService
    {
        private readonly IMapper _mapper;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(UserGroupsService));
        private static int MaxMessageCountPerPage = 30;

        public UserGroupsService()
        {
            //_mapper = Mapper.Instance;
        }
        public UserGroupsService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserGroupsListViewModel InitUserGroups(string userId, string email)
        {
            using (DataContext db = new DataContext()) 
            {
                List<UserGroupViewModel> userGroups = new List<UserGroupViewModel>();
                foreach (var ugr in db.UsersGroups.Where(ug => ug.GroupMembers.Any(gm => gm.AspNetUser.Email == email)))
                {
                    userGroups.Add(new UserGroupViewModel()
                                        { 
                                            Id = ugr.Id,
                                            LastChatMessages = ugr.ChatMessages.OrderByDescending(cm => cm.DateCreated).Take(1).ToList().Select(cm => _mapper.Map<ChatMessageViewModel>(cm)).FirstOrDefault(),
                                            UserGroupMembers = ugr.GroupMembers.Select(gm => new UserGroupMemberViewModel()
                                            {
                                                Id = gm.Id,
                                                UserName = gm.AspNetUser.UserName
                                            }).ToList(),
                                            NewMessagesCount = ugr.ChatMessages.Where(cm => cm.SenderId != userId && !cm.SeenBy.Contains(email)).Count()
                                        });
                }

                var currentUserGroupViewModel = new UserGroupWithMessagesViewModel();
                userGroups = userGroups.OrderByDescending(ugvm => ugvm.LastChatMessages.DateCreated).ToList();

                if (userGroups.Any())
                {
                    var currentGroupId = userGroups.FirstOrDefault().Id;
                    var currentUserGroup = db.UsersGroups.Where(ug => ug.Id == currentGroupId).FirstOrDefault();

                    currentUserGroupViewModel.Id = currentUserGroup.Id;
                    currentUserGroupViewModel.ChatMessagesByDay = _mapper.Map<List<ChatMessageViewModel>>(currentUserGroup.ChatMessages
                                                                                .OrderByDescending(cm => cm.DateCreated)
                                                                                .Take(MaxMessageCountPerPage)
                                                                                .OrderBy(cm => cm.DateCreated))
                                                                            .GroupBy(cm => cm.DateCreated.Date)
                                                                            .OrderBy(g => g.Key);
                    currentUserGroupViewModel.UserGroupMembers = currentUserGroup.GroupMembers.Select(gm => new UserGroupMemberViewModel()
                                                                        {
                                                                            Id = gm.Id,
                                                                            UserName = gm.AspNetUser.UserName,
                                                                        }).ToList();
                    currentUserGroupViewModel.HasOlderMessages = currentUserGroup.ChatMessages.Count > MaxMessageCountPerPage;
                }

                UserGroupsListViewModel model = new UserGroupsListViewModel()
                {
                    UserGroupsList = userGroups, 
                    CurrentUserGroup = currentUserGroupViewModel,
                };

                return model;
            }
        }

        public List<ChatMessageViewModel> GetChatMessages(int userGroupId)
        {
            using (DataContext db = new DataContext())
            {
                return _mapper.Map<List<ChatMessageViewModel>>(db.UsersGroups.FirstOrDefault(u => u.Id == userGroupId).ChatMessages);
            }
        }

        public List<ChatMessageViewModel> GetNewChatMessages(int userGroupId)
        {
            using (DataContext db = new DataContext())
            {
                return _mapper.Map<List<ChatMessageViewModel>>(db.UsersGroups.FirstOrDefault(u => u.Id == userGroupId).ChatMessages);
            }
        }

        public List<object> SearchUserGroups(string query, string currentUserId)
        {
            using (DataContext db = new DataContext())
            {
                //  todo
                //var groups = db.UsersGroups.Where(ug => ug.GroupMembers.Any(u => u.AspNetUser.UserName.Contains(query)))
                //                           .OrderBy(ug => ug.GroupMembers.Count())
                //                           .Take(10);

                var users = db.AspNetUsers.Where(u => u.UserName.Contains(query) && u.Id != currentUserId)
                                          .OrderBy(u => u.UserName).Take(10);
                return users.Select(u => new { key = u.Id, value = u.UserName, isUserGroup = false }).ToList<object>();
            }
        }

        public object GetUserGroup(int userGroupId)
        {
            using (DataContext db = new DataContext())
            {
                UserGroupViewModel model = new UserGroupViewModel();

                var userGroup = db.UsersGroups.FirstOrDefault(u => u.Id == userGroupId);
                if (userGroup != null)
                {
                    model = _mapper.Map<UserGroupViewModel>(userGroup);
                }

                return model;
            }
        }

        public int CreateGroup(string senderId, string receiverUserName)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var receiver = db.AspNetUsers.FirstOrDefault(u => u.UserName == receiverUserName);
                    if(receiver != null)
                    {
                        string receiverId = receiver.Id;

                        UsersGroup userGroup = new UsersGroup()
                        {
                            UserCreatorId = senderId,
                            DateCreated = DateTime.Now
                        };

                        db.UsersGroups.Add(userGroup);
                        db.SaveChanges();

                        db.GroupMembers.Add(new GroupMember() { MemberId = senderId, UsersGroupId = userGroup.Id });
                        db.GroupMembers.Add(new GroupMember() { MemberId = receiverId, UsersGroupId = userGroup.Id });
                        db.SaveChanges();
                         
                        return userGroup.Id;
                    }
                    else
                    { 
                        return 0;
                    }
                }
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionLogingHelper.LogException(ex);

                return 0;
            }
        }

        public int StoreChatMessage(string senderId, int userGroupId, string message)
        {
            try
            {
                using (DataContext db = new DataContext())
                {
                    var newMessage = new ChatMessage()
                    {
                        SenderId = senderId,
                        UsersGroupId = userGroupId,
                        Message = message, 
                        SeenBy = String.Empty,
                        DateCreated = DateTime.Now
                    };
                    db.ChatMessages.Add(newMessage);
                    db.SaveChanges();
                    return newMessage.Id;
                }
            }
            catch (DbEntityValidationException ex)
            {
                ExceptionLogingHelper.LogException(ex);

                return 0;
            }
        }

        public List<UserGroupShortViewModel> GetUserGroupsByUser(string email)
        {
            using (DataContext db = new DataContext())
            {
                return db.UsersGroups.Where(ug => ug.GroupMembers.Any(gm => gm.AspNetUser.Email == email))
                                     .Select(ug => new UserGroupShortViewModel()
                                     {
                                         Id = ug.Id,
                                         NewMessagesCount = ug.ChatMessages.Where(cm => !cm.SeenBy.Contains(email)).Count(),
                                         LatestMessage = _mapper.Map<ChatMessageViewModel>(ug.ChatMessages.OrderByDescending(cm => cm.DateCreated).FirstOrDefault())
                                     })
                                     .OrderByDescending(ugs => ugs.LatestMessage.DateCreated)
                                     .ToList();
            }
        }

        public List<int> GetUserGroupsIDsByUser(string email)
        {
            using (DataContext db = new DataContext())
            {
                return db.UsersGroups.Where(ug => ug.GroupMembers.Any(gm => gm.AspNetUser.Email == email)).Select(ug => ug.Id).ToList();
            }
        }

        public UserGroupWithMessagesViewModel GetUserGroupWithMessages(int userGroupId, int userGroupLatMessageId)
        {
            using (DataContext db = new DataContext())
            {
                var currentUserGroupViewModel = new UserGroupWithMessagesViewModel(); 
                try
                {
                    var usersGroup = db.UsersGroups.FirstOrDefault(ug => ug.Id == userGroupId);
                    if(usersGroup != null)
                    {
                        currentUserGroupViewModel.Id = userGroupId;

                        if(userGroupLatMessageId == 0)
                        {
                            currentUserGroupViewModel.ChatMessagesByDay = _mapper.Map<List<ChatMessageViewModel>>(usersGroup.ChatMessages
                                                                                .OrderByDescending(cm => cm.DateCreated)
                                                                                .Take(MaxMessageCountPerPage))
                                                                            .GroupBy(cm => cm.DateCreated.Date)
                                                                            .OrderBy(g => g.Key);

                        }
                        else
                        {
                            currentUserGroupViewModel.ChatMessagesByDay = _mapper.Map<List<ChatMessageViewModel>>(usersGroup.ChatMessages
                                                                                .OrderBy(cm => cm.DateCreated)
                                                                                .SkipWhile(cm => userGroupLatMessageId == 0 ? true : cm.Id == userGroupLatMessageId)
                                                                                .Take(MaxMessageCountPerPage))
                                                                            .GroupBy(cm => cm.DateCreated.Date)
                                                                            .OrderBy(g => g.Key);
                        }
                        currentUserGroupViewModel.UserGroupMembers = usersGroup.GroupMembers.Select(gm => new UserGroupMemberViewModel()
                            {
                                Id = gm.Id,
                                UserName = gm.AspNetUser.UserName
                            }).ToList();
                    }
                }
                catch(Exception ex)
                {
                    ExceptionLogingHelper.LogException(ex);
                }

                return currentUserGroupViewModel;
            }
        }

        public bool MessageSeen(string userId, int[] messagesIDs)
        {
            using (DataContext db = new DataContext())
            {
                try
                {
                    // performances check - try to fetch user group first
                    foreach(var message in db.ChatMessages.Where(cm => messagesIDs.Contains(cm.Id)))
                    {
                        message.SeenBy += (!String.IsNullOrEmpty(message.SeenBy) ? "," : String.Empty) + userId;
                    }

                    //db.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException ex)
                {
                    ExceptionLogingHelper.LogException(ex);
                }
                catch (Exception ex)
                {
                    ExceptionLogingHelper.LogException(ex);
                }

                return false;
            }
        }
    }
}
