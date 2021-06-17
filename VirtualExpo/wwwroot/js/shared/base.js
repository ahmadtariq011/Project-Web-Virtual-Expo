var IsHTML5 = false;

$(document).ready(function () {

    $.ajaxSetup({
        // Disable caching of AJAX responses
        cache: false
    });

    if (typeof Storage !== "undefined") {
        IsHTML5 = true;
    }

    $("body").on("click",".closedRow", function () {
        ExpandDetailRow($(this));
    });
    $("body").on("click", ".expandedRow", function () {
        CollapseDetailRow($(this));
    });

});

function ExpandDetailRow(btn)
{
    var trDetail = btn.parent().parent().next();
    trDetail.removeClass("hide");
    btn.removeClass("closedRow");
    btn.addClass("expandedRow");
}

function CollapseDetailRow(btn)
{
    var trDetail = btn.parent().parent().next();
    trDetail.addClass("hide");
    btn.addClass("closedRow");
    btn.removeClass("expandedRow");
}

function AddSortingHeaders(pId) {
    $(pId + ' thead th[sortable]').each(function () {
        var th = $(this);
        var datafield = th.attr("datafield");
        var caption = th.attr("caption");
        var html = caption;
        html += "<i></i><ul>";
        html += "<li sort=\"" + datafield + " Asc\"><a><i class=\"fa fa-arrow-down\"></i> Sort Ascending</a></li>";
        html += "<li sort=\"" + datafield + " Desc\"><a><i class=\"fa fa-arrow-up\"></i> Sort Descending</a></li>";
        html += "</ul>";
        th.append(html);
    });
}


function DisplayMessage() {

}

function DisplayError() {

}
