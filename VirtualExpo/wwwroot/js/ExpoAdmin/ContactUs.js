var CustomerList;
var filters;
var filters_customer;
var CustomersGridPager;
var is_pages_set = false;
var sortOption = ""; var sortOption_license = "";

$(document).ready(function () {
    $("#aContactUs").addClass("navbar_selected");

    $('#editor').trumbowyg();
    $("#editor").html($("#hfDescription").val());

    $("#txtSearch").keyup(handler_enter_search);
    $("#divAddEdit input").keyup(handler_enter);
    AddSortingHeaders("#tbl");

    $("body").on("click", "#tbl thead th", function () {
        var th = $(this);
        if (!$(th).hasClass("gridSortingMenu")) {
            return
        }
        $(".gridSortingMenu").children("i").attr("class", "");
        if ($(this).hasClass("Asc")) {
            $(this).removeClass("Asc");

            $("#tbl thead th").removeClass("Asc");
            $("#tbl thead th").removeClass("Desc");
            $(this).addClass("Desc");


            sortOption = $(this).attr("datafield") + " Desc";

            th.children("i").attr("class", "fa fa-arrow-up");

        }
        else if ($(this).hasClass("Desc")) {
            $(this).removeClass("Desc");

            $("#tbl thead th").removeClass("Asc");
            $("#tbl thead th").removeClass("Desc");
            $(this).addClass("Asc");

            sortOption = $(this).attr("datafield") + " Asc";
            th.children("i").attr("class", "fa fa-arrow-down");
        }
        else {
            $(this).removeClass("Desc");
            $(this).addClass("Asc");
            sortOption = $(this).attr("datafield") + " Asc";
            th.children("i").attr("class", "fa fa-arrow-down");

        }
        filters.Sort = sortOption;
        LoadMessageWithCount();
    });

    //if (IsHTML5 && sessionStorage["CustomerFilters"] != null) {
    //    filters = $.parseJSON(sessionStorage["CustomerFilters"]);     
    //    $("#txtSearch").val(filters.Keyword);   
    //    LoadCustomersWithCount();
    //    return;
    //}

    SearchMessage();
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
        SaveCategory();
    }
}

function handler_enter_search(e) {
    var charCode;

    if (e && e.which) {
        charCode = e.which;
    } else if (window.event) {
        e = window.event;
        charCode = e.keyCode;
    }
    if (charCode == 13)
        SearchMessage();
}
function SearchMessage() {
    UpdateMessageFilters();
    LoadMessageWithCount();
}


function UpdateMessageFilters() {
    filters =
    {
        Keyword: $("#txtSearch").val(),
        PageIndex: 1,
        PageSize: 10,
        Sort: sortOption
    };


}
function LoadMessageWithCount() {
    if (IsHTML5) {
        sessionStorage["CustomerFilters"] = JSON.stringify(filters);
    }
    $("#loader").show();
    $.post("/api/ContactUsApi/LoadMessageWithCount", filters, LoadMessageWithCountCallBack);
}
function LoadMessageWithCountCallBack(data) {
    $("#loader").hide(); $("#divCustomerList").show();
    $("#tbl").show(); $("#div_no_found").hide(); $("#divPagerCustomers").show();
    $("#spanTotalRecords").text("(" + data.totalCount + " records)");

    if (data.TotalCount < 1) {
        $("#tbl").hide();
        $("#divPagerCustomers").hide();
        $("#div_no_found").show();
        return;
    }
    $("#tbl tbody").html($("#ListTemplateCustomers").render(data.message));

    if (CustomersGridPager == null) {
        CustomersGridPager = $("#divPagerCustomers").GridPager({
            TotalRecords: data.TotalCount,
            ChangePageSize: ChangePageCustomerResults,
            NavigateToPage: CustomersPageNavigation
        });
    }

    CustomersGridPager.GridPager("SetPageIndexAndSize", filters.PageIndex, filters.PageSize);
    CustomersGridPager.GridPager("SetPager", data.TotalCount);
}

function LoadMessagePageCallBack(data) {
    $("#loader").hide();
    $("#tbl tbody").html($("#ListTemplateCustomers").render(data.Message));
}

function LoadMessagePage() {
    if (IsHTML5) {
        sessionStorage["CustomerFilters"] = JSON.stringify(filters);
    }
    $("#loader").show();
    $.post("/api/ContactUsApi/GetMessage", filters, LoadMessagePageCallBack);
}

function ChangePageCustomerResults(pIndex, pSize) {
    filters.PageIndex = pIndex;
    filters.PageSize = pSize;
    LoadMessageWithCount();
}

function CustomersPageNavigation(pIndex) {
    filters.PageIndex = pIndex;
    LoadMessagePage();
}

function LoadMessage() {
    $("#loader").show();
    filters =
    {
        Keyword: $("#txtSearch").val(),
        PageIndex: (pageIndex - 1),
        PageSize: pageSize
    };

    $.post("/api/ContactUsApi/GetMessage", filters, LoadMessageCallBack);
}
function LoadMessageCallBack(data) {
    $("#loader").hide();
    $("#tbl tbody").html($("#ListTemplateCustomers").render(data.Message));
}

function DeleteMessage(CustomerId) {
    var r = confirm('Are you sure you want to delete?');
    if (!r)
        return;

    $("#loader").show();
    $("#div_message").hide();
    Customer =
    {
        Id: CustomerId
    };

    $.post("/api/ContactUsApi/DeleteMessage", Customer, DeleteMessageCallBack);
}
function DeleteMessageCallBack(data) {
    $("#loader").hide();
    if (data.IsSucceeded) {
        $("#div_message").show();
        $("#div_message").removeClass("failure");
        $("#div_message").addClass("success");
        $("#span_message").html("Message has been successfully deleted.");
        LoadMessageWithCount();
    } else {
        $("#div_message").show();
        $("#div_message").removeClass("success");
        $("#div_message").addClass("failure");
        $("#span_message").html(data.Message);
    }
}

function ResetMessage() {
    $("#txtSearch").val("");
    SearchMessage();
}



function OpenPopup(id) {
    $("#div_message").hide();
   
    $("#editMeLabel").text("Add Recipients");
    loadRecipientsEmails();
}
function loadRecipientsEmails() {
    $("#loader").show();
    var RecipientsEmails =
    {
        Id: 1
    };
    $.post("/api/ContactUsApi/GetRecipientsEmails", RecipientsEmails,loadRecipientsEmailsCallBack);
}
function loadRecipientsEmailsCallBack(data) {
    $("#loader").hide();
    $("#txtRecipientsName").val(data.Message);
    popup("#divAddEdit", "#popupBackground");

}

function OpenPopupReply(id) {
    $("#div_message").hide();
    $("#ReplyLabel").text("Reply User");
    LoadReplyTemplate(id);
}

function LoadReplyTemplate(id) {
    $("#loader").show();
    var ReplyTemplate =
    {
        Id: id
    };
    $.post("/api/ContactUsApi/GetReplyTemplate", ReplyTemplate, LoadReplyTemplateCallBack);
}
function LoadReplyTemplateCallBack(data) {
    $("#loader").hide();
    $("#txtToEmail").val(data.message);
    popup("#divReply", "#popupBackground");

}

function SendReply() {
    $("#loader").show();
    var EmailBody =
    {
        ToEmail: $.trim($("#txtToEmail").val()),
        Message: $.trim( $("#editor").html())
    };
    $.post("/api/ContactUsApi/SendReply", EmailBody, SendReplyCallBack);
}

function SendReplyCallBack(data) {
    $("#loader").hide();
    if (!data.IsSucceeded) {
        ShowCallbackMessage(false, data.Message);
        return;
    }
    ShowCallbackMessage(true, data.Message);
    hidePopup('#divReply', '#popupBackground');
    
}

function Reset() {
    $("#ReplyLabel").text("Reply User");
    $("#hfId").val("0");
    $("#txtCategoryName").val("");
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


function SaveRecipients() {
    if (!Validate("#divAddEdit")) {
        return;
    }

    $("#loader").show();
    $("#div_message").hide();

    var RecipientsEmails =
    {
        Id: 1,
        ContactUsEmailAddresses: $.trim($("#txtRecipientsName").val())
    };

    $.post("/api/ContactUsApi/SaveRecipients", RecipientsEmails, SaveRecipientsCallback);
}

function SaveRecipientsCallback(data) {
    $("#loader").hide();
    if (!data.IsSucceeded) {
        ShowCallbackMessage(false, data.Message);
        return;
    }
    ShowCallbackMessage(true, data.Message);
    hidePopup('#divAddEdit', '#popupBackground');
    
}

var _sender;
function CheckRead(sender) {
    _sender = sender;
    var IsRead = $(sender).attr("IsRead");
    var id = $(sender).attr("id");
    
    if (IsRead === "true") {        
        return;
    }

    var ContactInfoId =
    {
        Id: id
    };
    $.post("/api/ContactUsApi/CheckRead", ContactInfoId, CheckReadsCallback);

}

function CheckReadsCallback(data) {
    if (!data.IsSucceeded) {
        return;
    }

    $(_sender).attr("IsRead", true);
    $(_sender).parent().parent().removeClass("unRead");
    $(_sender).parent().parent().addClass("read");
}