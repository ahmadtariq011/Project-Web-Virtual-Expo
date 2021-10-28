function UploadMediaLink() {
    debugger;
    if (!Validate("#BasicInfo")) {
        return;
    }
    $("#loader").show();
    $("#div_message").hide();

    var formData = new FormData();
    var ImageInput = $('#txtPicture')[0].files[0];
    var VideoInput = $('#txtVideo')[0].files[0];
    var DownloadInput = $('#txtDownload')[0].files[0];

    formData.append("Id", $("#hfUserId").val());
    formData.append("PictureDescription", $.trim($("#txtPictureName").val()));
    formData.append("VideoDescription", $.trim($("#txtVideoName").val()));
    formData.append("DownloadDescription", $.trim($("#txtDownloadName").val()));
    formData.append("LinkDescription", $.trim($("#txtLinkName").val()));
    formData.append("Exhibitor_Id", $.trim($("#hfExhibitorId").val()));
    formData.append("Link", $.trim($("#txtLink").val()));
    formData.append("PictureFile", ImageInput);
    formData.append("VideoFile", VideoInput);
    formData.append("PdfFile", DownloadInput);

    $.ajax({
        method: "post",
        url: '/api/UserAPI/SaveMediaLink',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            $("#loader").hide();
            ShowCallbackMessage(true, data.message);
        },
        error: function (data) {
            $("#loader").hide();
            ShowCallbackMessage(false, data.message);
        }
    });
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

function UploadSocialNetwork() {
    if (!Validate("#BasicInfo")) {
        return;
    }
    $("#loader").show();

    $("#div_message").hide();

    var User =
    {
        Facebook: $.trim($("#txtFacebookName").val()),
        Id: $("#hfUserId").val(),
        Instagram: $.trim($("#txtInstagramName").val()),
        Twitter: $.trim($("#txtTwitter").val()),
        SnapChat: $.trim($("#txtSnapcat").val()),
        Linkdin: $.trim($("#txtLinkdin").val()),
        Website: $.trim($("#txtWebsite").val()),
        Exhibitor_Id: $.trim($("#hfExhibitorId").val())
    };

    $.post("/api/UserAPI/SaveSocialExperience", User, SaveExhibitionCallback);
}

function SaveExhibitionCallback(data) {
    $("#loader").hide();
    if (!data.isSucceeded) {
        ShowCallbackMessage(false, data.message);
        return;
    }

    ShowCallbackMessage(true, data.message);
}
