app.controller("CarouselDemoCtrl", function ($rootScope, $scope, $state, $location) {

    $scope.myInterval = 7000;
    $scope.$on('ImagesPushed', function (e, call) {
        $scope.slides = $scope.ReleventProductImages;

        var i, first = [], second;
        for (i = 0; i < $scope.slides.length; i++) {
            second = {
                image1: $scope.slides[i]
            };
            second.image2 = $scope.slides[i + 1];
            second.image3 = $scope.slides[i + 2];
            first.push(second);
        }
        $scope.groupedSlides = first;
    });
});

app.directive('dnDisplayMode', function ($window) {
    return {
        restrict: 'A',
        scope: {
            dnDisplayMode: '='
        },
        template: '<span class="desktop"></span>',
        link: function (scope, elem, attrs) {
            var markers = elem.find('span');
            function isVisible(element) {
                return element && element.style.display != 'none' && element.offsetWidth && element.offsetHeight;
            }
            function update() {
                angular.forEach(markers, function (element) {
                    if (isVisible(element)) {
                        scope.dnDisplayMode = element.className;
                        return false;
                    }
                });
            }
            var t;
            angular.element($window).bind('resize', function () {
                clearTimeout(t);
                t = setTimeout(function () {
                    update();
                    scope.$apply();
                }, 300);
            });
            update();
        }
    };
});