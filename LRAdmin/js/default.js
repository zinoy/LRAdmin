$(function() {
    $(window).resize(adjustHeight);
    $('.left-nav dt a').click(toggleNavigator);
    setNav();
    adjustHeight();
});

function adjustHeight() {
    var h = $(window).height() - 80 - 10 - 55;
    var w = $(window).width();
    if ($('.content').height() + 125 > $(window).height()) {
        h = $('.content').height() + 105 - 55;
        if ($('#datatable_wrapper').length > 0)
            h -= 72;
    }
    $('.left-frame').css("height", h);
    $('.content').css({ "width": w - 200 - 20 });
}

function setNav() {
    var nl;
    var p = window.location.pathname.substr(1).toLowerCase().split('/');
    var path = p[p.length - 1];
    $('.left-nav a').each(function(i) {
        var href = $(this).attr("href").toLowerCase();
        if (path == href) {
            nl = $(this);
            return false;
        }
    });
    if (nl != null) {
        nl.parent().addClass('cur');
        var rt = nl.parents('dl');
        rt.addClass('expand');
        $('#Tabs h4').text(rt.children('dt').text());
        document.title = nl.text() + " | " + document.title;
    }
}

function toggleNavigator(e) {
    e.preventDefault();
    var _p = $(this).parents('dl');
    if (_p.hasClass('expand')) {
        if (_p.children('dd.cur').length > 0)
            return;
        _p.removeClass('expand');
    } else {
        _p.addClass('expand');
    }
}

function bindRangeDatepicker(f, t) {
    //$.datepicker.setDefaults($.datepicker.regional[""]);
    var dates = $("#" + f + ", #" + t).datepicker({
        defaultDate: "-1m",
        changeMonth: true,
        numberOfMonths: 3,
        beforeShow: function(input) {
            var selectedDate = this.id == f ? $('#' + t).val() : $('#' + f).val();
            var regdate = /\d{4}-[01]\d{1}-[0123]\d{1}/;
            if (regdate.test(selectedDate)) {
                var option = this.id == f ? "maxDate" : "minDate",
			    instance = $(this).data("datepicker"),
                date = $.datepicker.parseDate(
				instance.settings.dateFormat ||
				$.datepicker._defaults.dateFormat,
				selectedDate, instance.settings);
                $(this).datepicker("option", option, date);
            }
        }
    });
}