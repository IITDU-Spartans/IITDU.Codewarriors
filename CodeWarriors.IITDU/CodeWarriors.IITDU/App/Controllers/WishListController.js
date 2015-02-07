app.controller("WishListController", function ($http, $scope) {
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
    }
    $scope.Message = "";
    $scope.Show = false;
    $scope.WishList = [];
    $http.get("/WishList/GetWishList").success(function (response) {
        //$scope.WishList = response;
        $scope.WishList = [];
        for (var i = 0; i < response.wishItems.length; i++) {
            $scope.WishList.push({ productInfo: response.wishItems[i].product, ImageUrl: response.images[i], wishId: response.wishItems[i].wishId });
        }
    });
    $scope.RemoveFromWishList = function (id) {
        var data = { wishId: parseInt(id) };
        $http.post("/WishList/RemoveFromWishList", data).success(function (response) {
            for (var i = $scope.WishList.length - 1; i >= 0; i--) {
                if ($scope.WishList[i].wishId == id) {
                    $scope.WishList.splice(i, 1);
                }
            }
            $scope.Show = true;
            $scope.Message = response;
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
})