$(document).ready(function () {
    $("#comment").val("");
    $("#divContactUs input").keyup(handler_enter_login);
});
function handler_enter_login(e) {
    var charCode;

    if (e && e.which) {
        charCode = e.which;
    } else if (window.event) {
        e = window.event;
        charCode = e.keyCode;
    }
    if (charCode == 13)
        SaveComment();
}


function SaveFeedback() {

    if (!Validate("#divContactUs")) {
        return;
    }
    $("#loader").show();
    $("#span_message").html("");

    var Comment =
    {
        Name: $.trim($("#name").val()),
        Email: $.trim($("#email").val()),
        Telephone: $.trim($("#telephone").val()),
        Message: $.trim($("#comment").val()),
        ExhibitionId: $.trim($("#ExhibitionId").val())
    };

    $.post("/api/ContactUsApi/SaveFeedback", Comment, SaveFeedbackCallback);
}

function SaveFeedbackCallback(data) {
    $("#loader").hide();
    if (data.IsSucceeded === false) {
        ShowCallbackMessage(false, data.message);
        return;
    }
    else {
        ShowCallbackMessage(true, data.message);
        $("#name").val("");
        $("#email").val("");
        $("#telephone").val("");
        $("#comment").val("");
        return;
    }

    $("#divConfirmation").show();

}


function SaveComment() {

    if (!Validate("#divContactUs")) {
        return;
    }
    $("#loader").show();
    $("#span_message").html("");

    var Comment =
    {
        Name: $.trim($("#name").val()),
        Email: $.trim($("#email").val()),
        Telephone: $.trim($("#telephone").val()),
        Message: $.trim($("#comment").val())
    };

    $.post("/api/ContactUsApi/SaveComment", Comment, SaveCommentCallback);
}

function SaveCommentCallback(data) {
    $("#loader").hide();
    if (data.IsSucceeded === false) {
        ShowCallbackMessage(false, data.message);
        return;
    }
    else {
        ShowCallbackMessage(true, data.message);
        $("#name").val("");
        $("#email").val("");
        $("#telephone").val("");
        $("#comment").val("");
        return;
    }

    $("#divConfirmation").show();

}


function ShowCallbackMessage(isSucceeded, message) {
    $("#loader").hide();
    if (isSucceeded) {
        $("#div_message").removeClass("failure");
        $("#div_message").addClass("success");
    }
    else {
        $("#div_message").removeClass("success");
        $("#div_message").addClass("failure");
    }
    $("#div_message").show();
    $("#span_message").html(message);
}
