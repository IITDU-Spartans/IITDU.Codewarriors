app.controller("ProductListController", function ($scope, $http, $location) {
    init();

    function init() {
        $http.get("/Product/GetAllProducts").success(function (response) {
            $scope.Products = response;
        });

    }

    $scope.addToWishlist = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/WishList/AddToWishList", data).success(function (response, status) {
            if (status == 200)
                $scope.Message = response;
            else $location.path("login");
        }).error(function () {
            $location.path("login");
        });
    }

    $scope.addToCart = function (id) {

        var cartItem = {
            ProductId: id,
            Quantity: 1
        };
        $http.post("/Cart/AddToCart/", cartItem).success(function (response, status) {
            if (status == 200)
                $scope.Message = response;
            else $location.path("login");
        }).error(function () {
            $location.path("login");
        });
    }
    $scope.Rate = function (ratedValue, id) {
        var data = {
            productId: id,
            rating: ratedValue
        };
        $http.post("/Rating/AddRating", data).success(function (response) {
            alert(response);
        });
    }
});