app.controller("AppController", function ($rootScope, $scope, $state, $location) {

    $rootScope.getAddedProduct = function (product) {
        //alert(product.ProductName);
    };
    $scope.SearchText = "";

    $scope.SearchProduct = function () {
        $location.path("search/" + $scope.SearchText);
    }
});