app.directive('scroll', function ($, $document, $window) {
    return function (scope, elm, attr) {
        var raw = elm[0];

        //$document.bind('scroll', function () {
        //    //if (raw.scrollTop + raw.offsetHeight >= raw.scrollHeight - 1) {
        //    //    scope.$apply(attr.scroll);
        //    //}
        //});
        $(window).bind('scroll', function () {
            if ($(window).scrollTop() + 100 >= $(document).height() - $(window).height()) {
                scope.$apply(attr.scroll);
            }
        });
    };
});