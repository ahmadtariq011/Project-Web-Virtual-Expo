//Popup dialog
function popup(dialogId, dialogOverlayId) {

    // get the screen height and width 
    var maskHeight = $(document).height();
    var maskWidth = $(window).width();

    var dialogMargin= "-" + $(dialogId).outerWidth() / 2 + "px";

    // calculate the values for center alignment
    var dialogTop = getScreenCenterY();
    var dialogLeft = getScreenCenterX();

    // assign values to the overlay and dialog box
    $(dialogOverlayId).css({ height: maskHeight, width: maskWidth }).fadeIn("slow");
    $(dialogId).css({ "margin-left": dialogMargin }).fadeIn("slow");
}
function hidePopup(dialogId, dialogOverlayId)
{
    $(dialogOverlayId).hide();
    $(dialogId).hide();
}
function getScreenCenterY() {
    var y = 0;
    y = getScrollOffset() + (getInnerHeight() / 2);
    y = Math.round(y);
    return (y);
}
function getScreenCenterX() {
    var x = document.body.clientWidth / 2;
    x = Math.round(x);
    return (x);
}
function getInnerHeight() {
    var y;
    if (self.innerHeight) // all except Explorer
    {
        y = self.innerHeight;
    }
    else if (document.documentElement && document.documentElement.clientHeight) {
        y = document.documentElement.clientHeight;
    }
    else if (document.body) // other Explorers
    {
        y = document.body.clientHeight;
    }
    return (y);
}
function getScrollOffset() {
    var y;
    if (self.pageYOffset) // all except Explorer
    {
        y = self.pageYOffset;
    }
    else if (document.documentElement && document.documentElement.scrollTop) {
        y = document.documentElement.scrollTop;
    }
    else if (document.body) // all other Explorers
    {
        y = document.body.scrollTop;
    }
    return (y);
}