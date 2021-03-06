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
    $.post("/api/RequestAdminApi/ChangeStatusAdmin", User, ChangeStatusCallback);
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
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
    window.location.href = "/Admin/RequestAdmin/"
}


$(document).ready(function () {
    //$(window).load(function () {
    //    $('#navbarlist li').click(function () {
    //        $(this).addClass("pageloadnavbar");
    //    });
    //});
    $("#aAdmins").addClass("navbar_selected");
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
function RequestAdmin() {
    debugger;
    if (!Validate("#divPersonal")) {
        return;
    }
    $("#loader").show();

    var formData = new FormData();
    var fileInput = $('#SponsorLisr')[0].files[0];

    formData.append("Id", $("#hfUserId").val());
    formData.append("Name", $.trim($("#txtName").val()));
    formData.append("Location", $.trim($("#txtLocation").val()));
    formData.append("Link", $.trim($("#txtLink").val()));
    formData.append("moto", $.trim($("#txtMoto").val()));
    formData.append("Email", $.trim($("#txtEmail").val()));
    formData.append("Telephone", $.trim($("#txtTelephone").val()));
    formData.append("DateFrom", $.trim($("#txtFrom").val()));
    formData.append("DateTo", $.trim($("#txtTo").val()));
    formData.append("Expected_Number_Of_Exhibitor", $.trim($("#txtNumberExhibitors").val()));
    formData.append("NameOfEvent", $.trim($("#txtEventName").val()));

    formData.append("Image", fileInput);


    $.ajax({
        method: "post",
        url: '/api/RequestAdminApi/RequestAdmin',
        data: formData,
        processData: false,
        contentType: false,
        success: function (data) {
            $("#loader").hide();
            ShowCallbackMessage(data.isSucceeded, data.message);
        }
    });
}

function UploadPicCallback(data) {
    //$("#loader").hide();
    debugger;
    if (!data.isSucceeded) {
        ShowCallbackMessage(false, data.message);
        return;
    }

    ShowCallbackMessage(true, data.message);
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
        UserType: 2,
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
    $.post("/api/RequestAdminApi/GetAdminRequestWithCount", filters, LoadUsersWithCountCallBack);
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
