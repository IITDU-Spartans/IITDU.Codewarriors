app.controller("ProductController", function ($scope, $http, ProductService, $stateParams, $location) {

    init();
    function init() {
        $scope.Product = {};
        var productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?ProductId=" + productId).success(function (response) {
            $scope.Product = response.product;

        });
    }
    $scope.Rate = function () {
        var data = {
            productId: $scope.Product.ProductId,
            rating: $scope.Product.AverageRate
        };
        $http.post("/Rating/AddRating", data).success(function (response, status) {
            if (status == 200)
                $scope.Product.AverageRate = response;
            else alert("Error");
        });
    }
    $scope.addToWishlist = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/WishList/AddToWishList", data).success(function (response) {
            $scope.Message = response;
            alert(response);
        }).error(function () {
            $location.path("login");
        });
    }

    $scope.addToCart = function (id) {

        var cartItem = {
            ProductId: id,
            Quantity: 1
        };
        $http.post("/Cart/AddToCart/", cartItem).success(function (response) {
            $scope.Message = response;
            alert(response);
        }).error(function () {
            $location.path("login");
        });
    }

});