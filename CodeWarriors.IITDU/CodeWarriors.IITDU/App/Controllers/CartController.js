app.controller("CartController", function ($http, $scope, $location) {
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
    $scope.CartItems = [];
    $http.get("/Cart/GetAllCartItems").success(function (response) {
        $scope.CartItems = response;
    });
    $scope.RemoveFromCart = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/Cart/RemoveFromCart", data).success(function (response) {
            for (var i = $scope.CartItems.length-1; i >=0; i--) {
                if ($scope.CartItems[i].ProductId == id) {
                    $scope.CartItems.splice(i, 1);
                }

            }
            $scope.Show = true;
            $scope.Message = response;
        });
    }
})