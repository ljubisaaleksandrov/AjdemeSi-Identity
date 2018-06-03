var Acc = function () {
    var settings = {
        const_dot = '.',
        const_sharp = '#',
        loginForm: '#login-form',
        registerForm: '#register-form',
        ajaxSignUpBtn: '.sign-in-button.sign-in-button-post',
        ajaxSignUpFormWrapper: '.sign-in-wrapper',
        ajaxMenuRegisterDriverBtnClass: 'sign-in-reg-driver-button',
        ajaxMenuRegisterDriverBtn: const_dot + 'sign-in-reg-driver-button',
        ajaxMenuRegisterUserBtnClass: 'sign-in-reg-user-button',
        ajaxMenuRegisterUserBtn: const_dot + 'sign-in-reg-user-button'
    };

    this.init = function () {
        $(document).on("submit", settings.loginForm, flfs),
        $(document).on("submit", settings.registerForm, frfs),
        $(document).on("click", settings.ajaxSignUpBtn, fasubc),
        $(document).on("click", settings.ajaxMenuRegisterDriverBtn, fmrdbc),
        $(document).on("click", settings.ajaxMenuRegisterUserBtn, fmrubc)
    },
    fmrdbc = function (e) {
        e.stopPropagation();
        e.preventDefault();

        $.ajax({
            type: 'get',
            url: $(e.target).attr('href'),
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
    },
    fasubc = function (e) {
        e.stopPropagation();
        e.preventDefault();

        $.ajax({
            type: 'get',
            url: $(e.target).attr('href'),
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
