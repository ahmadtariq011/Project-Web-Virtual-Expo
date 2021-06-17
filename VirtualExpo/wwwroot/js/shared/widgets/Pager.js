(function ($) {

    $.widget("LillyLifestyle.GridPager", {

        // default options
        options: {
            PageIndex: 1,
            PageSize: 20,
            TotalRecords: 0,
            ChangePageSize: null,
            NavigateToPage: null
        },

        // the constructor
        _create: function () {
            this.obj = this.element;
            this.btnNextPage = this.obj.find("#aNextPage");
            this.btnPreviousPage = this.obj.find("#aPreviousPage");

            this.ddlPageSize = this.obj.find("#ddlPageSize");
            this.spanTotalPages = this.obj.find("#spanTotalPages");
            this.txtPageNumber = this.obj.find("#txtPageNumber");
            this.spanTotalRecords = this.obj.find("#spanTotalRecords");

            this.ddlPageSize.data("eventClick", this.options.ChangePageSize);
            this.btnNextPage.data("eventClick", this.options.NavigateToPage);
            this.btnPreviousPage.data("eventClick", this.options.NavigateToPage);

            this.ddlPageSize.change(function () {
                var pageSize = parseInt($(this).val());
                self.options.PageSize = pageSize;
                self.options.PageIndex = 1;
                var eventChangePageSize = $(this).data("eventClick");
                eventChangePageSize(1, pageSize);
            });

            var self = this;

            this.btnPreviousPage.click(function () {
                self.options.PageIndex--;
                self._SetPagerNavigation();
                var eventNavigateToPage = $(this).data("eventClick");
                eventNavigateToPage(self.options.PageIndex);
            });

            this.btnNextPage.click(function () {
                self.options.PageIndex++;
                self._SetPagerNavigation();
                var eventNavigateToPage = $(this).data("eventClick");
                eventNavigateToPage(self.options.PageIndex);
            });

        },

        _SetPagerNavigation: function () {
            this.txtPageNumber.val(this.options.PageIndex);
            this._EnableDisablePagerNavigation(true);

            if (this.options.PageIndex == 1) {
                this.btnPreviousPage.addClass("disabled");
            }
            if (this.options.PageIndex == parseInt(this.spanTotalPages.text())) {
                this.btnNextPage.addClass("disabled");
            }
        },

        _EnableDisablePagerNavigation: function (IsEnabled) {
            if (IsEnabled) {
                this.btnPreviousPage.removeClass("disabled");
                this.btnNextPage.removeClass("disabled");
            }
            else {
                this.btnPreviousPage.addClass("disabled");
                this.btnNextPage.addClass("disabled");
            }
        },

        SetPageIndexAndSize: function (pageIndex, pageSize) {
            this.options.PageIndex = pageIndex;
            this.options.PageSize = pageSize;
        },

        SetPager: function (totalRecords) {
            this.ddlPageSize.val(this.options.PageSize);
            var pageSize = this.ddlPageSize.val();
            this.options.TotalRecords = totalRecords;
            this.spanTotalRecords.text(totalRecords);
          

            this.obj.removeClass("hide");

            var TotalPages = 0;

            for (i = 1; i <= Math.ceil(this.options.TotalRecords / pageSize) ; i++) {
                TotalPages++;
            }

            this.spanTotalPages.text(TotalPages);
            this.txtPageNumber.val(this.options.PageIndex);
            if (TotalPages < 2) {
                this._EnableDisablePagerNavigation(false);
                return;
            }

            this._SetPagerNavigation();
        },

        HidePager: function () {
            this.ddlPageSize.val("20");
            this.obj.addClass("hide");
        }

    });

})(jQuery);