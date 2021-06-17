
$(document).ready(function () {
    debugger;
    if ($("#hfProductId").val() > 0) {
        $("#hiddenPictureDiv").removeClass("hide");
    }
    LoadUserImage();

    var uri = window.location.toString();
    if (uri.indexOf("?") > 0) {
        var clean_uri = uri.substring(0, uri.indexOf("?"));
        window.history.replaceState({}, document.title, clean_uri);
    }
    $("#aUsers").addClass("navbar_selected");
    $("#div_AddEdit input").keyup(handler_enter);
    $("#txtExpirationDate").datepicker({ dateFormat: 'mm/dd/yy' });

    $("#txtCreditCardNumber").keypress(DigitsOnly);
    $("#txtMonth").keypress(DigitsOnly);
    $("#txtYear").keypress(DigitsOnly);
    $("#txtCVV").keypress(DigitsOnly);
    $("#txtNumberOfAgents").keypress(DigitsOnly);

    //$(document).tooltip({
    //    content: function () {
    //        return $(this).prop('title');
    //    }
    //    , position: {
    //        my: "left top",
    //        at: "left bottom"
    //    }
    //    , show: {
    //        effect: "slideDown"
    //    }
    //    ,position: {
    //        my: "center bottom-20",
    //        at: "center top",
    //        using: function (position, feedback) {
    //            $(this).css(position);
    //            $("<div>")
    //                .addClass("arrow")
    //                .addClass(feedback.vertical)
    //                .addClass(feedback.horizontal)
    //                .appendTo(this);
    //        }
    //    }
    //});
});
function handler_enter(e) {
    var charCode;

    if (e && e.which) {
        charCode = e.which;
    } else if (window.event) {
        e = window.event;
        charCode = e.keyCode;
    }
    if (charCode == 13) {
        SaveUser();
    }
}

function SaveUser() {
    if (!Validate("#BasicInfo")) {
        return;
    }

    $("#div_message").hide();

    var User =
    {
        Telephone: $.trim($("#txtTelephone").val()),
        Id: $("#hfUserId").val(),
        FirstName: $.trim($("#txtFirstName").val()),
        LastName: $.trim($("#txtLastName").val()),
        UserName: $.trim($("#txtUserName").val()),
        Email: $.trim($("#txtEmail").val()),
        Password: $.trim($("#txtPassword").val()),
        CNIC: $.trim($("#txtCNIC").val()),
        UserTypeName: $.trim($("#txtUserType").val()),
        GenderTypename: $.trim($("#txtGender").val())
    };

    $.post("/api/UserAPI/SaveUser", User, SaveUserCallback);
}

function SaveUserCallback(data) {
    $("#loader").hide();
    if (!data.isSucceeded) {
        ShowCallbackMessage(false, data.message);
        return;
    }

    ShowCallbackMessage(true, data.message);
}


function LoadUserImage() {

    var productId = $("#hfProductId").val();
    var filters =
    {
        Id: productId
    };
    $.post("/api/UserAPI/GetUserImage", filters, LoadUserImageCallBack);
}

function LoadUserImageCallBack(data) {
    $("#div_no_found").hide();
    $("#divLogos").show();

    if (data.totalCount < 1) {
        $("#divLogos").hide();
        $("#div_message_upload_file").hide();
        $("#div_no_found").show();
        return;
    }

    $("#divLogos").html($("#ListTemplateLogos").render(data.message));
}


function Delete(id) {
    var r = confirm('Are you sure you want to delete?');
    if (!r) {
        return;
    }
    $("#loader").show();
    var filters = {
        Id: id
    };
    $.post("/api/UserAPI/DeletePicture", filters, function (data) {
        $("#loader").hide();
        if (!data.isSucceeded) {
            $("#div_message1").removeClass("success");
            $("#div_message1").addClass("failure");
            $("#div_message1").show();
            $("#span_message1").html(data.message);
        }
        else {
            $("#div_message1").removeClass("failure");
            $("#div_message1").addClass("success");
            $("#div_message1").show();
            $("#span_message1").html(data.message);
        }
        $("#div_message_upload_file").hide();
        LoadUserImage();
    });
}





function UploadAgent() {
    if (!Validate("#UploadAgents"))
        return;

    $("#loader").show();
    $("#div_message").hide();

    $("#frmAgent").ajaxSubmit({
        url: '/api/BlogCategoryApi/UploadCategory',
        type: 'post',
        success: UploadAgentCallback,
        error: function (result) {
        }
    });
}


function UploadAgentCallback(data) {
    var result = $.parseJSON(data);

    ShowCallbackMessage(result.IsSucceeded, result.Message);
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



function carrierLimits() {
    var totalNoOfCarriers = $("#txtNumberOfCarriers").val();
    if (totalNoOfCarriers < $("input[name='chkCarriers']:checked").length) {
        alert("No of selected carriers is greater you are enter values here");
        $("#txtNumberOfCarriers").val($("input[name='chkCarriers']:checked").length);
    }
}

function varifyNoOfCarriers(v) {
    var totalNoOfCarriers = $("#txtNumberOfCarriers").val();
    if (totalNoOfCarriers < $("input[name='chkCarriers']:checked").length) {
        alert("No of selected carriers limit is exeeded");
        $(v).prop('checked', false);
    }
}

function VarifyBanyanClearView(sender) {
    if ($("#chkBanyan").is(":checked") && $("#chClearView").is(":checked")) {
        alert("You can't select Banyan and ClearView carriers at same time.");
        $(sender).prop('checked', false);
    }
}



function Cancel() {
    window.location.href = "/" + $("#hfUserType").val() + "/Customers";
}

function Reset(option) {
    if (option == "ReportSettings") {
        $("#ddlReportFrequncy").val('');
    }
    else if (option == "ListSettings") {
        $("#txtRemoveBlackIPAfterNdays").val('');
    }
    else if (option == "RedListSettings") {
        $("#ddlRedListOptions").val('');
        $("#txtRemoveRedIPAfterNdays").val('');
        $("#txtAuthAPIKey").val('');
    }
    else if (option == "EmailSettings") {
        $("#txtSMTP").val('');
        $("#txtSMTPort").val('');
        $("#txtUserName").val('');
        $("#txtPassword").val('');
        $("#chkEnableSsl").prop("checked", false);
    }
}

function OpenTab(evt, tabName) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = $(".tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = $(".tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(tabName).style.display = "block";

    $("#btn" + tabName).addClass("active");
    //evt.currentTarget.className += " active";
}




function ShowHideShipLinkCentralCredential() {
    $("#divShipLinkCentralCredential").hide();
    $("#txtShipLinkCentralUsername").removeAttr("required");
    $("#txtShipLinkCentralPassword").removeAttr("required");

    if ($("#chkIsEndOfDay").is(":checked") == true) {
        $("#divShipLinkCentralCredential").show();
        $("#txtShipLinkCentralUsername").attr("required", "required");
        $("#txtShipLinkCentralPassword").attr("required", "required");
    }
}

function ShowHidePassword(sender, target) {
    var senderObj = $(sender);
    document.getElementById(target).type = senderObj.is(":checked") ? 'text' : 'password';
}

function ShowHidePasswordInLabel(sender, lblPassword, password) {
    var stars = password.replace(/./g, "*");

    var senderObj = $(sender);
    $("#" + lblPassword).text(senderObj.is(":checked") ? password : stars);
}

function Cancel() {
    window.location.href = 'Index';
}

function UpdateProfile() {
    if (!Validate("#BasicInfo")) {
        return;
    }

    $("#loader").show();
    $("#div_message").hide();

    var customer =
    {
        User_Id: $("#hfUserId").val(),
        Telephone: $.trim($("#txtTelephone").val()),
        Address: $("#txtAddress").val(),
        City: $("#txtCity").val(),
        State: $("#txtState").val(),
        NumberOfAgents: $("#txtNumberOfAgents").val(),
        ExpirationDate: $.trim($("#txtExpirationDate").val()),
        User:
        {
            Id: $("#hfUserId").val(),
            FirstName: $.trim($("#txtFirstName").val()),
            LastName: $.trim($("#txtLastName").val()),
            Email: $.trim($("#txtEmail").val()),
            UserId: $.trim($("#txtUserId").val()),
            Password: $.trim($("#txtPassword").val())
        }
    }

    $.post("/api/CustomerApi/UpdateProfile", customer, UpdateProfileCallBack);
}

function UpdateProfileCallBack(data) {
    $("#loader").hide();

    ShowCallbackMessage(data.IsSucceeded, data.Message);
}

function DigitsOnly(e) {
    //if the letter is not digit then display error and don't type anything
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
}

function SelectPackage() {
    if ($("#ddlPackage").val() == "") {
        $("#divPackageInfo").hide();
        return;
    }
    $("#divPackageInfo").show();
    $("#lblPackageDescription").text("$" + $("#ddlPackage option:selected").data("description"));
    $("#lblAmount").text("$" + $("#ddlPackage option:selected").data("amount"));
    $("#lblNumberOfAgents").text($("#ddlPackage option:selected").data("agents"));
}

function CalculatePrice() {
    if ($("#txtNumberOfAgents").val() == "") {
        $("#lblAmount").text("0");
        return;
    }
    var no_agents = parseInt($("#txtNumberOfAgents").val());
    var price = parseInt($("#hfAmount").val());

    var total_price = no_agents * price;

    $("#lblAmount").text(total_price);
}