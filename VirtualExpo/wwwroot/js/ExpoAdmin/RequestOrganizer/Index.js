var CustomerList;
var filters;
var filters_customer;
var CustomersGridPager;
var is_pages_set = false;
var sortOption = ""; var sortOption_license = "";
//$("#navbarlist li a").click(function () {
//    $(this).parent().addClass('pageloadnavbar').siblings().removeClass('pageloadnavbar');

//});
function ChangeStatus(NewStatus) {
    $("#loader").show();
    debugger;
    $("#div_messagef").hide();
    var User =
    {
        Id: $("#hfUserId").val(),
        ExhibitionStatusStr: NewStatus
    };
    $.post("/api/RequestAdminApi/ChangeStatus", User, ChangeStatusCallback);
}
function ChangeStatusCallback(data) {
    $("#loader").hide();
    debugger;
    if (!data.isSucceeded) {
        $("#div_message").removeClass("success");
        $("#div_message").addClass("failure");
        $("#div_message").show();
        $("#span_message").html(data.message);
        return;
    }

    $("#div_message").removeClass("failure");
    $("#div_message").addClass("success");
    $("#div_message").show();
    $("#span_message").html(data.message);
    window.location.href = "/Organizer/RequestOrganizer/";
}

$(document).ready(function () {
    //$(window).load(function () {
    //    $('#navbarlist li').click(function () {
    //        $(this).addClass("pageloadnavbar");
    //    });
    //});
    $("#aUsers").addClass("navbar_selected");
    $("#txtSearch").keyup(handler_enter_search);
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
        LoadUsersWithCount();
    });

    //if (IsHTML5 && sessionStorage["CustomerFilters"] != null) {
    //    filters = $.parseJSON(sessionStorage["CustomerFilters"]);     
    //    $("#txtSearch").val(filters.Keyword);   
    //    LoadCustomersWithCount();
    //    return;
    //}

    SearchUsers();
});

function handler_enter_search(e) {
    var charCode;

    if (e && e.which) {
        charCode = e.which;
    } else if (window.event) {
        e = window.event;
        charCode = e.keyCode;
    }
    if (charCode == 13)
        SearchUsers();
}
function SearchUsers() {
    UpdateUsersFilters();
    LoadUsersWithCount();
}

function ResetUsers() {
    $("#txtSearch").val("");
    SearchUsers();
}

function UpdateUsersFilters() {
    filters =
    {
        Keyword: $("#txtSearch").val(),
        Exhibition_Id: $("#ExhibitionId").val(),
        PageIndex: 1,
        PageSize: 10,
        Sort: sortOption
    };


}
function LoadUsersWithCount() {
    if (IsHTML5) {
        sessionStorage["CustomerFilters"] = JSON.stringify(filters);
    }
    $("#loader").show();
    $.post("/api/RequestAdminApi/GetOrganizerRequestWithCount", filters, LoadUsersWithCountCallBack);
}
function LoadUsersWithCountCallBack(data) {
    $("#loader").hide(); $("#divCustomerList").show();
    $("#tbl").show(); $("#div_no_found").hide(); $("#divPagerUsers").show();
    $("#spanTotalRecords").text("(" + data.totalCount + " records)");

    if (data.totalCount < 1) {
        $("#tbl").hide();
        $("#divPagerUsers").hide();
        $("#div_no_found").show();
        return;
    }
    $("#tbl tbody").html($("#ListTemplateCustomers").render(data.message));

    if (CustomersGridPager == null) {
        CustomersGridPager = $("#divPagerUsers").GridPager({
            TotalRecords: data.TotalCount,
            ChangePageSize: ChangePageCustomerResults,
            NavigateToPage: CustomersPageNavigation
        });
    }

    CustomersGridPager.GridPager("SetPageIndexAndSize", filters.PageIndex, filters.PageSize);
    CustomersGridPager.GridPager("SetPager", data.TotalCount);
}

function LoadCustomerPagedCallBack(data) {
    $("#loader").hide();
    $("#tbl tbody").html($("#ListTemplateCustomers").render(data.Message));
}

function LoadCustomerPaged() {
    if (IsHTML5) {
        sessionStorage["CustomerFilters"] = JSON.stringify(filters);
    }
    $("#loader").show();
    $.post("/api/UserApi/GetUsers", filters, LoadCustomerPagedCallBack);
}

function ChangePageCustomerResults(pIndex, pSize) {
    filters.PageIndex = pIndex;
    filters.PageSize = pSize;
    LoadUsersWithCount();
}

function CustomersPageNavigation(pIndex) {
    filters.PageIndex = pIndex;
    LoadCustomerPaged();
}

function LoadUsers() {

    $("#loader").show();
    filters =
    {
        Keyword: $("#txtSearch").val(),
        PageIndex: (pageIndex - 1),
        PageSize: pageSize
    };

    $.post("/api/LeadsApi/GetLeads", filters, LoadCustomersCallBack);
}
function LoadCustomersCallBack(data) {
    $("#loader").hide();
    $("#tbl tbody").html($("#ListTemplateCustomers").render(data.Message));
}
//function RequestOrganizer() {
//    if (!Validate("#divPersonal")) {
//        return;
//    }
//    debugger;
//    $("#div_message").hide(); 
    
//    var User =
//    {
//        Telephone: $.trim($("#txtTelephone").val()),
//        Id: $("#hfUserId").val(),
//        Name: $.trim($("#txtFirstName").val()),
//        Description: $.trim($("#txtLastName").val()),
//        WebsiteLink: $.trim($("#txtUserName").val()),
//        Email: $.trim($("#txtEmail").val()),
//        ExhibitionId: $.trim($("#txtExhibitionId").val())

//    };

//    $.post("/api/RequestAdminApi/RequestOrganizer", User, SaveUserCallback);
////}
//function SaveUserCallback(data) {
//    //if (!data.isSucceeded) {
//    //    ShowCallbackMessage(false, data.message);
//    //    return;
//    //}

//    //ShowCallbackMessage(true, data.message);
//}

function UploadPic() {
    debugger;
    if (!Validate("#divPersonal")) {
        return;
    }
    $("#loader").show();

    var formData = new FormData();
    var fileInput = $('#OrganizerImage')[0].files[0];

    formData.append("Id", $("#hfUserId").val());
    formData.append("Telephone", $.trim($("#txtTelephone").val()));
    formData.append("Name", $.trim($("#txtFirstName").val()));
    formData.append("Description", $.trim($("#txtLastName").val()));
    formData.append("WebsiteLink", $.trim($("#txtUserName").val()));
    formData.append("Email", $.trim($("#txtEmail").val()));
    formData.append("ExhibitionId", $.trim($("#txtExhibitionId").val()));
    formData.append("Image", fileInput);

    formData.append("Image", fileInput);

    $.ajax({
        method: "post",
        url: '/api/RequestAdminApi/UploadPic',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            ShowCallbackMessage(data.isSucceeded, data.message);
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
function UploadPicCallback(data) {
    $("#loader").hide();

    debugger;
    if (!data.isSucceeded) {
        ShowCallbackMessage(false, data.message);
        return;
    }

    ShowCallbackMessage(true, data.message);
}
function DeleteUsers(CustomerId) {
    var r = confirm('Are you sure you want to delete?');
    if (!r)
        return;

    $("#loader").show();
    $("#div_message").hide();
    Customer =
    {
        Id: CustomerId
    };

    $.post("/api/UserApi/DeleteUser", Customer, DeleteUsersCallBack);
}
function DeleteUsersCallBack(data) {
    $("#loader").hide();
    if (data.isSucceeded) {
        $("#div_message").show();
        $("#div_message").removeClass("failure");
        $("#div_message").addClass("success");
        $("#span_message").html("User has been successfully deleted.");
        LoadUsersWithCount();
    } else {
        $("#div_message").show();
        $("#div_message").removeClass("success");
        $("#div_message").addClass("failure");
        $("#span_message").html(data.Message);
    }
}

function ResetUsers() {
    $("#txtSearch").val("");
    SearchUsers();
}
