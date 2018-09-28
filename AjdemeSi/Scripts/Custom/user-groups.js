var dotSelector = '.';
var dateDelimiterToday =    '<div class="user-group-chat-messages-date">' +
                                '<div class="separator"><hr></div>' +
                                '<span>Today</span>' +
                                '<div class="separator"><hr></div>' +
                            '</div>';

var isScrollerInitialized = false;

var _messageNotSeenClass = 'not-seen';
var _messageSeenCandidateClass = 'message-seen-candidate';
var _messageSeenClass = 'message-seen';

var _CurrentUserId = "#CurrentUserId";
var _currentUserEmail = "#CurrentUserEmail";

var currentUserId;
var currentUserEmail;


var _UserGroupsListContainer = "#UserGroupsListContainer";
var _userGroupsChatsContainer = ".user-groups-chat-container";
var _EmptyUserGroup = "#EmptyUserGroup";
var _userGroupChat = ".user-group-chat";
var _userGroupChatActiveClass = "user-group-chat-active";
var _userGroupChatActive = dotSelector + _userGroupChatActiveClass;
var _userGroupItem = ".user-group-item";
var _userGroupItemActiveClass = "user-group-item-active";
var _userGroupItemActive = dotSelector + _userGroupItemActiveClass;
var _userGroupItemNewMsgsCount = ".user-group-item-new-msgs-count";
var _messagesContainer = '.user-group-chat-messages-container';
var _userGroupsChatContainer = '.user-groups-chat-container';
var _messageNew = '.user-group-chat-new-message';

var _LastMessageSender = ".last-message-sender";
var _LastMessageDate = ".last-message-date";
var _LastMessageTime = ".last-message-time";

var initChat = function () {
    currentUserId = $(_CurrentUserId).val();
    currentUserEmail = $(_currentUserEmail).val();

    var chat = $.connection.userGroupsHub;

    chat.client.sendToGroup = function (groupId, message, messageId, senderId, date, time) {
        var messageContainer;
        var _sidebarCurrentGroup = $('*[data-user-group-item-id="' + groupId + '"]');
        var _conversationCurrentGroup = $('#user-group-chat-' + groupId);
        var isCurentUserSender = currentUserId == senderId;

        if (_conversationCurrentGroup.length == 0) {
            //  adding new user group chat messages container
            $(_EmptyUserGroup).removeClass(_userGroupChatActiveClass);
            _conversationCurrentGroup = $(_userGroupsChatsContainer).children(_EmptyUserGroup).clone();
            $(_conversationCurrentGroup).attr('id', 'user-group-chat-' + groupId);
            $(_conversationCurrentGroup).addClass(_userGroupChatActiveClass);
            $(_userGroupsChatsContainer).append(_conversationCurrentGroup);
        }

        var messageClass = "user-group-chat-message";
        var groupItemClass = "user-group-item";
        if (isCurentUserSender) {
            messageClass += " right";
            groupItemClass += " " + _userGroupItemActiveClass;
            $(_userGroupItemActive).removeClass(_userGroupItemActiveClass);
        }

        if (_sidebarCurrentGroup.length == 0) {     // the group is new - create the group
            //  adding new user group in the sidebar
            var _sidebarCurrentGroup = '<div class="' + groupItemClass + '" data-user-group-item-id="' + groupId + '">' + groupId + '</div>';
            $(_UserGroupsListContainer).prepend(_sidebarCurrentGroup);

            $(_conversationCurrentGroup).children(_messagesContainer).append(dateDelimiterToday);

            messageContainer = '<div class="' + messageClass + '">' +
                                    '<div class="user-group-chat-message-avatar">' +
                                        '<span class="glyphicon glyphicon-user"></span>' +
                                    '</div>' +
                                    '<span class="user-group-chat-message-time">' + time + '</span>' +
                                    '<span class="user-group-chat-message-content" class="' + messageClass + '" data-id="' + messageId + '">' + message + '</span>' +
                                '</div>';
        }
        else {
            var lastMessageSender = $(_conversationCurrentGroup).find(_LastMessageSender).val();
            var lastMessageDate = $(_conversationCurrentGroup).find(_LastMessageDate).val();
            var lastMessageTime = $(_conversationCurrentGroup).find(_LastMessageTime).val();

            console.log(lastMessageSender + " - " + senderId + " - " + currentUserId);

            if (lastMessageDate != date) {
                $(_conversationCurrentGroup).children(_messagesContainer).append(dateDelimiterToday);
            }

            if (lastMessageDate != date || lastMessageSender != senderId || lastMessageDate != date || lastMessageTime != time) {
                messageContainer = '<div class="' + messageClass + '">' +
                                        '<br/>' +
                                        '<div class="user-group-chat-message-avatar">' +
                                            '<span class="glyphicon glyphicon-user"></span>' +
                                        '</div>' +
                                        '<span class="user-group-chat-message-time">' + time + '</span>' +
                                        '<span class="user-group-chat-message-content" class="' + messageClass + '" data-id="' + messageId + '">' + message + '</span>' +
                                    '</div>';
            }
            else {
                messageContainer = '<div class="' + messageClass + '">' +
                                        '<span class="user-group-chat-message-content" class="' + messageClass + '" data-id="' + messageId + '">' + message + '</span>' +
                                    '</div>';
            }

            //  update new messages count if chat is not active
            if (!_sidebarCurrentGroup.hasClass(_userGroupItemActiveClass)) {
                var newMsgsElement = $(_sidebarCurrentGroup).children(_userGroupItemNewMsgsCount);
                var newMsgsCount = parseInt($(newMsgsElement).text()) + 1;
                $(newMsgsElement).text(newMsgsCount);

                $(newMsgsElement).fadeIn();

                $({ deg: 0 }).delay(1000).animate({ deg: 360 }, {
                    duration: 200,
                    easing: "swing",
                    step: function (now) {
                        newMsgsElement.css({
                            '-moz-transform': 'rotate(' + now + 'deg)',
                            '-webkit-transform': 'rotate(' + now + 'deg)',
                            '-o-transform': 'rotate(' + now + 'deg)',
                            '-ms-transform': 'rotate(' + now + 'deg)',
                            'transform': 'rotate(' + now + 'deg)'
                        });
                    },
                    complete: function (event) {
                    } || $.noop
                });
            }
        }

        $(_conversationCurrentGroup).children(_messagesContainer).append(messageContainer);
        $(_messagesContainer).scrollTop(3000);

        updateLatestMessageHidden(_conversationCurrentGroup, senderId, date, time);
    };

    $.connection.hub.start().done(function () {
        $(document).on('keypress', _messageNew, function (e) {
            if (e.which == 13) {
                var message = $(this).val();
                var userGroupId = $('.user-group-chat-active').attr('id').replace('user-group-chat-', '');
                if (!$.isNumeric(userGroupId)) {
                    userGroupId = $('.user-group-chat-active').data('tmp-user');
                }
                chat.server.sendToGroup(userGroupId, message);
                $(this).val('');
            }
        });

    }).fail(function (e) {
        if (e.source === 'HubException') {
            console.log(e.message + ' : ' + e.data.user);
        }
        console.log(e);
    });

    $.connection.hub.error(function (error) {
        console.log(error);
    });
};

var updateLatestMessageHidden = function (_currentGroup, sender, date, time) {
    $(_currentGroup).find(_LastMessageSender).val(sender);
    $(_currentGroup).find(_LastMessageDate).val(date);
    $(_currentGroup).find(_LastMessageTime).val(time);
}

var checkSeenMessages = function () {
    // get seen messages
    var notSeenMessages = $(_userGroupChatActive).find('.user-group-chat-message-content.not-seen');
    $.each(notSeenMessages, function (index, element) {
        isInViewport(element);
    });

    var seenMessages = [];

    // update seen messages style (if any)
    $.each($(_userGroupChatActive).find(dotSelector + _messageSeenClass), function (index, element) {
        seenMessages.push($(element).data('id'));
        $(element).removeClass(_messageSeenClass);
        $(element).children('span').animate({
            backgroundColor: "white",
            color: "black",
            fontWidth: "normal",
        }, 1000);
        $(element).removeClass(_messageNotSeenClass, 1000);
    });


    if (seenMessages.length > 0) {
        // update the sidebar user group not-seen messages count
        var newMsgsElement = $(_userGroupItemActive).children(_userGroupItemNewMsgsCount);
        var newMsgsCount = parseInt($(newMsgsElement).text());
        newMsgsCount -= seenMessages.length;

        $({ deg: 0 }).delay(1000).animate({ deg: 360 }, {
            duration: 200,
            easing: "swing",
            step: function (now) {
                newMsgsElement.css({
                    '-moz-transform': 'rotate(' + now + 'deg)',
                    '-webkit-transform': 'rotate(' + now + 'deg)',
                    '-o-transform': 'rotate(' + now + 'deg)',
                    '-ms-transform': 'rotate(' + now + 'deg)',
                    'transform': 'rotate(' + now + 'deg)'
                });
            },
            complete: function (event) {
                $(newMsgsElement).text(newMsgsCount);

                if (newMsgsCount == 0) {
                    $(newMsgsElement).delay(2000).fadeOut();
                }
            } || $.noop
        });

        // update the database entries
        $.ajax({
            url: "/UserGroups/MessagesSeen",
            type: "POST",
            dataType: "json",
            data: {
                userEmail: currentUserEmail,
                messagesIDs: seenMessages
            },
            success: function (data) {
                //console.log(data.status);
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
    }
}

var isInViewport = function(element) {
    var elementTop = $(element).offset().top;
    var elementBottom = elementTop + $(element).outerHeight();

    var container = $(element).parents(_messagesContainer)[0];
    var viewportTop = $(container).offset().top;
    var viewportBottom = viewportTop + $(container).height();

    if (elementTop >= viewportTop && elementBottom <= viewportBottom) {
        if (!$(element).hasClass(_messageSeenCandidateClass)) {
            $(element).addClass(_messageSeenCandidateClass);
        }
        else {
            $(element).removeClass(_messageSeenCandidateClass);
            $(element).addClass(_messageSeenClass);
        }
    }
    else {

    }
};


setInterval(checkSeenMessages, 2 * 1000);

var initAutocomplete = function(){
    $.each($('.input-autocomplete'), function (index, input) {
        $(input).autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: "/UserGroups/GetUserGroups",
                    type: "GET",
                    dataType: "json",
                    data: request,
                    success: function (data) {
                        var escapedTerm = request.term.replace(/([\^\$\(\)\[\]\{\}\*\.\+\?\|\\])/gi, "\\$1");
                        var regex = new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + escapedTerm + ")(?![^<>]*>)(?![^&;]+;)", "gi");
                        var result = $.map(data.results, function (result) {
                            var itemLabel = result.value.replace(regex, "<span class='highlight'>$1</span>");
                            return {
                                label: itemLabel,
                                value: result.value,
                                id: result.key,
                                isUserGroup: result.isUserGroup
                            };
                        });
                        response(result);
                    },
                    error: function (request, status, error) {
                        console.log(request.responseText);
                    }
                });
            },
            select: function (event, ui) {
                event.preventDefault();
                event.stopPropagation();
                userGroupSelected(ui.item);
                $(this).val('');
            },
            focus: function (event, ui) {
                event.preventDefault();
                event.stopPropagation();
                $(this).val(ui.item.value);
            },
            minLength: 2
        }).focus(function (event) {
            event.preventDefault();
            $(this).autocomplete("search");
        }).data('ui-autocomplete')._renderItem = function (ul, item) {
            var listItem = $('<li></li>').data('ui-autocomplete-item', item);
            var re = new RegExp('^' + this.term, 'i');
            var t = item.label.replace(re, '<span class="required-drop required-drop-chat" id="' + item.id + '">' + this.term + '</span>');
            listItem.html('<a>' + t + '</a>');
            return listItem.appendTo(ul);
        };
    });
}

var userGroupSelected = function (userGroupItem) {
    if (userGroupItem.isUserGroup) {
        $.ajax({
            url: "/UserGroups/GetUserGroup",
            type: "POST",
            dataType: "json",
            data: userGroupSelected.id,
            success: function (data) {
                console.log(data);
            },
            error: function (request, status, error) {
                console.log(request.responseText);
            }
        });
    }
    else {
        var emptyUserGroup = $("#EmptyUserGroup");
        $(emptyUserGroup).find('.user-group-chat-header-username').html(userGroupItem.value);
        $(emptyUserGroup).find(_messagesContainer).html('');
        $(emptyUserGroup).find(_messageNew).val('');
        $(emptyUserGroup).data('tmp-user', userGroupItem.value);
        $(_userGroupChatActive).removeClass(_userGroupChatActiveClass);
        $(emptyUserGroup).addClass(_userGroupChatActiveClass);
        $(emptyUserGroup).find(_messageNew).focus();
    }
}

var adjustBottom = function (topOffset) {
    $(_messagesContainer).height($(window).height() - topOffset);
    $(_messagesContainer).css('max-height', $(window).height() - topOffset);;
    $(_messagesContainer).scrollTop(3000);
}

$(document).ready(function () {
    initChat();
    initAutocomplete();

    var topOffset = $(_userGroupsChatsContainer).offset().top + $('.new-message-container').height() + 100 + 35;
    adjustBottom(topOffset);

    if ($('.user-group-chat').length == 1) {
        $(_EmptyUserGroup).addClass(_userGroupChatActiveClass);
    } else {
        $('.user-group-chat:eq(1)').addClass(_userGroupChatActiveClass);
    }

    $(window).resize(function () {
        adjustBottom();
    })

    $(document).on('click', _userGroupItem, function (event) {
        if(!$(this).hasClass(_userGroupItemActiveClass)){
            $(_userGroupItemActive).removeClass(_userGroupItemActiveClass);
            $(this).addClass(_userGroupItemActiveClass);
            var userGroupid = $(this).data('user-group-item-id');
            var userGroupChatSelected = $(_userGroupsChatContainer).children('#user-group-chat-' + userGroupid);

            console.log(userGroupid + " " + userGroupChatSelected.length);

            if (userGroupChatSelected.length == 1) {
                $(_userGroupsChatContainer).children(_userGroupChatActive).removeClass(_userGroupChatActiveClass);
                $(userGroupChatSelected).addClass(_userGroupChatActiveClass);
                adjustBottom(topOffset);
            }
            else if (userGroupChatSelected.length == 0) {
                $.ajax({
                    url: "/UserGroups/GetUserGroupChatMessages",
                    type: "POST",
                    data: {
                        userGroupId: userGroupid,
                        userGroupLatMessageId: 0
                    },
                    success: function (data) {
                        $(_userGroupsChatContainer).append(data);
                        $(_userGroupsChatContainer).children(_userGroupChatActive).removeClass(_userGroupChatActiveClass);
                        $(_userGroupsChatContainer).children('#user-group-chat-' + userGroupid).addClass(_userGroupChatActiveClass);
                        adjustBottom(topOffset);
                    },
                    error: function (request, status, error) {
                        console.log(request.responseText);
                    }
                });
            }
        }
    });
});

window.onload = function (event) {
    $(_messagesContainer).scrollTop(3000);
};

$(_messagesContainer).scroll(function () {
    if (isScrollerInitialized) {
        if ($(this).scrollTop() == 0) {
            var that = this;
            var groupId = $(_userGroupChatActive).attr('id').replace('user-group-chat-', '');
            var groupLatMessageId = $(this).find('.user-group-chat-message-content:first').data('id');

            $.ajax({
                url: "/UserGroups/GetUserGroupChatMessages",
                type: "POST",
                data: {
                    userGroupId: groupId,
                    userGroupLatMessageId: groupLatMessageId
                },
                success: function (data) {
                    $(that).prepend(data);
                },
                error: function (request, status, error) {
                    console.log(request.responseText);
                }
            });
        }
    }
    else {
        isScrollerInitialized = true;
    }
});

