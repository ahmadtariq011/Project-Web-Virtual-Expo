// it takes Parent Control's Id
function Validate(pId) {
    var resultBool = true;
    $(pId + ' :input[required]').each(function () {
        $(this).removeClass("error");
        if ($(this).val() == "") {
            $(this).addClass("error");
            resultBool = false;
        }
    });   
    $(pId + ' :input[IsDecimal]').each(function () {
        if (IsNotRequired($(this)))
            $(this).removeClass("error");

        var value = $(this).val();//GetCtrlValue($(this));
        if (value != "" && IsInvalidDecimal(value)) {
            $(this).addClass("error");
            resultBool = false;
        }
    });

    $(pId + ' :input[IsInteger]').each(function () {
        if (IsNotRequired($(this)))
            $(this).removeClass("error");
        if (IsInvalidInteger($(this).val())) {
            $(this).addClass("error");
            resultBool = false;
        }
    });

    $(pId + ' :input[IsEmail]').each(function () {
        if (IsNotRequired($(this)))
            $(this).removeClass("error");
        if (IsInValidEmail($(this).val())) {
            $(this).addClass("error");
            ShowCallbackMessage(false, "Enter valid Email");
            resultBool = false;
        }
    });

    $(pId + ' :input[IsURL]').each(function () {
        if (IsNotRequired($(this)))
            $(this).removeClass("error");
        if (isURL($(this).val())) {
            $(this).addClass("error");
            ShowCallbackMessage(false, "Enter valid URL or Link with Protocols");
            resultBool = false;
        }
    });

    //$(pId + ' :input[IsUserName]').each(function () {
    //    if (IsNotRequired($(this)))
    //        $(this).removeClass("error");
    //    if (IsUserName($(this).val())) {
    //        $(this).addClass("error");
    //        ShowCallbackMessage(false, "Enter valid UserName");
    //        resultBool = false;
    //    }
    //});


    $(pId + ' :input[Compare]').each(function () {
        var compareWith = $(pId + ' :input[CompareWith]').val();
        if ($(this).val() != compareWith) {
            $(this).addClass("error");
            resultBool = false;
        }
    });

    $(pId + ' :input[IsUserName]').each(function () {
        if ($(this).val().length < 6) {
            $(this).addClass("error");
            ShowCallbackMessage(false, "Enter Valid UserName (min:6)");
            resultBool = false;
        }
    });

    $(pId + ' :input[MinPassLength]').each(function () {
        if ($(this).val().length <11) {
            $(this).addClass("error");
            ShowCallbackMessage(false, "Enter valid Phone Number (min:11)");
            resultBool = false;
        }
    });

    $(pId + ' :input[IsCNIC]').each(function () {
        if ($(this).val().length < 13) {
            $(this).addClass("error");
            ShowCallbackMessage(false, "Enter valid Phone Number (min:13)");
            resultBool = false;
        }
    });


    if(!resultBool)
        $(pId + " input.error").first().focus();

    return resultBool;
}
function IsNotRequired(ctrl) {
    var attr = ctrl.attr('required');
    if (typeof attr !== 'undefined' && attr !== false) {
        return false;
    }
    return true;
}
function IsInvalidDecimal(value) {
    var regDeci = /^(?:\d*\.\d{1,2})$/;
    if (regDeci.test(value))
        return false;
    else
        return true;
}

function IsInvalidInteger(value) {
    if (Math.floor(value) == value)
        return false;
    else
        return true;
}

function IsInValidEmail(email) {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    if (mailformat.test(email))
        return false;
    else
        return true;
}
function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function IsUserName(str) {
    var mailformat = /^[a-zA-Z0-9_]{5,}[a-zA-Z]+$/;

    if (mailformat.test(str))
        return false;
    else
        return true;
}

function isURL(str) {
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.?)+[a-z]{2,}|' + // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
        '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
        '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator

    if (pattern.test(str))
        return false;
    else
        return true;
}


function ShowCallbackMessage(isSucceeded, message) {
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