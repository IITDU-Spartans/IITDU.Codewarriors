app.controller("HomeController", function ($rootScope, $scope, $state, $location, $http) {

    $scope.RecommendedProducts = [];
    $scope.Images = [];
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (response) {
                $http.get("/Product/GetProductGallaryImages").success(function (response) {
                    $scope.Images = response;
                });

                $http.get("/Product/GetRecommendedProducts").success(function (response) {
                    for (var i = 0; i < response.products.length; i++) {
                        $scope.RecommendedProducts.push({ Info: response.products[i], ImageUrl: response.images[i] });
                    }
                });
            }
        });
    }

    $scope.addToWishlist = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/WishList/AddToWishList", data).success(function (response, status) {
            if (status == 200) {
                $scope.Message = response;
                alert(response);
            }
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
            if (status == 200) {
                $scope.Message = response;
                alert(response);
            }
            else $location.path("login");
        }).error(function () {
            $location.path("login");
        });
    }
});