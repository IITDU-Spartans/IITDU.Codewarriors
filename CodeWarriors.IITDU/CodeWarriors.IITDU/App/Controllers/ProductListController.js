app.controller("ProductListController", function ($scope, $http, $location) {
    init();

    function init() {
        $http.get("/Product/GetAllProducts").success(function (response) {
            $scope.Products = response;
        });
        
    }

    $scope.addToWishlist = function () {
        alert("Added to wishlist");
    }

    $scope.addToCart = function (id) {
        alert(id);
        var cartItem = {
            ProductId:id,
            Quantity:1 
        };
        $http.post("/Cart/AddToCart/",cartItem).success(function(response) {
            $scope.Message = response;
            alert(response);
        }).error(function() {
            $location.path("login");
        });
    }
});