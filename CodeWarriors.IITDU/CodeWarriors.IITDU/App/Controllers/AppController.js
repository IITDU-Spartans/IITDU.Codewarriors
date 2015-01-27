app.controller("AppController", function ($rootScope, $scope, $state) {

    $rootScope.getAddedProduct = function (product) {
        alert(product.ProductName);
    };
});