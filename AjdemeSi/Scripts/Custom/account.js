var Acc = function () {
    var settings = {
        const_dot: '.',
        const_sharp: '#',
        loginForm: '#login-form',
        registerForm: '#register-form',
        ajaxSignUpBtn: '.sign-in-button.sign-in-button-post',
        ajaxSignUpFormWrapper: '.home-content-wrapper',
        ajaxMenuRegisterDriverBtnClass: 'sign-in-reg-driver-button',
        ajaxMenuRegisterUserBtnClass: 'sign-in-reg-user-button',
        ajaxMenuRegisterDriverBtnClass: 'sign-in-reg-driver-button',
        ajaxMenuRegisterUserBtnClass: 'sign-in-reg-user-button',
        ajaxMenuRegisterDriverBtnLabel: 'Register Driver',
        ajaxMenuRegisterUserBtnLabel: 'Register User',
        ajaxMenuRegisterDriverBtnAction: '/Account/RegisterDriver',
        ajaxMenuRegisterUserBtnAction: '/Account/Register',
    };

    this.init = function () {
        $(document).on("submit", settings.loginForm, flfs),
        $(document).on("submit", settings.registerForm, frfs),
        $(document).on("click", settings.ajaxSignUpBtn, fasubc),
        $(document).on("click", settings.const_dot + settings.ajaxMenuRegisterDriverBtnClass, fasubc),
        $(document).on("click", settings.const_dot + settings.ajaxMenuRegisterUserBtnClass, fasubc)
    },
    fasubc = function (e) {
        var currentAction = $(e.target).attr('href');
        if (typeof currentAction != 'undefined') {
            e.stopPropagation();
            e.preventDefault();

            if ($(e.target).hasClass(settings.ajaxMenuRegisterDriverBtnClass)) {
                if (currentAction == settings.ajaxMenuRegisterDriverBtnAction){
                    $(e.target).attr('href', settings.ajaxMenuRegisterUserBtnAction);
                    $(e.target).text(settings.ajaxMenuRegisterUserBtnLabel);
                }
                else {
                    $(e.target).attr('href', settings.ajaxMenuRegisterDriverBtnAction);
                    $(e.target).text(settings.ajaxMenuRegisterDriverBtnLabel);
                }
            }

            $.ajax({
                type: 'get',
                url: currentAction,
                success: function (data) {
                    $(settings.ajaxSignUpFormWrapper).children().fadeOut(400, function () {
                        $(settings.ajaxSignUpFormWrapper).html(data);
                    });
                    $(settings.ajaxSignUpFormWrapper).children().fadeIn(400, function () { });
                },
                error: function (err) {
                    console.log("AJAX error in request: " + JSON.stringify(err, null, 2));
                }
            });
        }
    },
    flfs = function (e) {
        var isFormValid = true;

        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

        var email = $(t.loginForm).find('#Email').val();
        var pass = $(t.loginForm).find('#Password').val();

        if ($.trim(email) == '') { isFormValid = false; }
        else if (!emailReg.test(email)) { isFormValid = false; }
        if ($.trim(pass) == '') { isFormValid = false; }

        if (isFormValid) {
            $.ajax({
                url: '/umbraco/Surface/Users/LoginAsync',
                type: 'POST',
                data: {
                    email: email,
                    password: pass,
                    __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
                },
                context: this,
                success: function (data) {
                    if (!data.isValid) { console.log(data.error); }
                    else { console.log("Logged in..."); location.reload(); }
                },
                error: function (data) {
                    console.log("error", data);
                }
            });

            return false;
        }
        else {
            return isFormValid;
        }
    },
    frfs = function (e) {
        var isFormValid = true;

        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

        var email = $(t.registerForm).find('#Email').val();
        var pass = $(t.registerForm).find('#Password').val();
        var passConfirmed = $(t.registerForm).find('#ConfirmPassword').val();

        if ($.trim(email) == '') { isFormValid = false; }
        else if (!emailReg.test(email)) { isFormValid = false; }
        if ($.trim(pass) == '') { isFormValid = false; }
        if ($.trim(passConfirmed) == '') { isFormValid = false; }
        else if (pass != passConfirmed) { isFormValid = false; }

        if (isFormValid) {
            $.ajax({
                url: '/umbraco/Surface/Users/RegisterAsync',
                type: 'POST',
                data: {
                    email: email,
                    password: pass,
                    __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
                },
                context: this,
                success: function (data) {
                    if (!data.isValid) { console.log(data.error); }
                    else { console.log("User registered! Reloading the form ..."); location.reload(); }
                },
                error: function (data) {
                    console.log("error", data);
                }
            });

            return false;
        }
        else {
            return isFormValid;
        }
    }
}

$(document).ready(function (event) {
    var acc = new Acc();
    acc.init();
});
