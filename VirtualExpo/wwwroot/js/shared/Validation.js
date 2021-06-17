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
            resultBool = false;
        }
    });

    $(pId + ' :input[Compare]').each(function () {
        var compareWith = $(pId + ' :input[CompareWith]').val();
        if ($(this).val() != compareWith) {
            $(this).addClass("error");
            resultBool = false;
        }
    });

    $(pId + ' :input[MinPassLength]').each(function () {
        if ($(this).val().length <8) {
            $(this).addClass("error");
            ShowErrorMessage("#errorEdit", $('#hdfPasswordLength').val())
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