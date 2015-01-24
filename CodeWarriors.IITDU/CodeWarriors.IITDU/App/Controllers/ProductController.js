app.controller("ProductController", function ($scope, $http, ProductService, $stateParams) {
    
    init();

    function init() {
        var productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?ProductId="+productId).success(function (response) {
            $scope.Product = response;
        });
    }

});