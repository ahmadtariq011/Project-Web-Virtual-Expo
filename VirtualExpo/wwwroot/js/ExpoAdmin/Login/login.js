
function handler_enter_login(e) {
    var charCode;

    if (e && e.which) {
        charCode = e.which;
    } else if (window.event) {
        e = window.event;
        charCode = e.keyCode;
    }
    if (charCode == 13)
        Login();
}
function Login() {
    if (!Validate('#divLogin')) {
        return;
    }        

    $("#div_message").hide();
    $("#login-button").attr("disabled", true);

    var login = {
        UserName: $("#txtEmail").val(),
        Password: $("#txtPassword").val(),
    };

    $.post("/api/AccountApi/Login", login, LoginCallBack);
}
function LoginCallBack(data) {
    $("#login-button").attr("disabled", false);
    
    if (data.isSucceeded) {
        window.location.href = data.message;
    } else {
        $("#div_message").show();
        $("#div_message").removeClass("success");
        $("#div_message").addClass("failure");
        $("#span_message").html(data.message);
    }
}

function SendForgotPasswordRequest() {
    if (!Validate('#divPassword'))
        return;

    $("#loader").show();
    $("#div_message").hide();
    $("#btnSubmit").attr("disabled", true);

    var login = {
        Email: $("#txtEmail").val()
    };

    $.post("/api/AccountApi/ForgotPassword", login, SendForgotPasswordRequestCallBack);
}

function SendForgotPasswordRequestCallBack(data) {
    $("#btnSubmit").attr("disabled", false);
    $("#loader").hide();
    if (data.IsSucceeded) {
        $("#divPassword").hide();
        $("#divConfirmation").show();
    } else {
        $("#div_message").show();
        $("#div_message").removeClass("success");
        $("#div_message").addClass("failure");
        $("#span_message").html(data.Message);
    }
}

function ResetPassword() {
    if (!Validate('#divPassword'))
        return;

    $("#loader").show();
    $("#div_message").hide();
    $("#btnSubmit").attr("disabled", true);

    var login = {
        Email: $("#hfEmail").val(),
        NewPassword: $("#txtNewPassword").val()        
    };

    $.post("/api/AccountApi/ResetPassword", login, SendForgotPasswordRequestCallBack);
}

function Logout(type) {
    $.post("api/AccountApi/Logout", type, logoutcallback);
}
function logoutcallback(data) {
    window.location.href = data.message;
}