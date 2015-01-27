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
    $scope.Total = parseInt(0);
    $scope.CartItems = [];
    $http.get("/Cart/GetAllCartItems").success(function (response) {
        $scope.CartItems = response;
        for (var i = 0; i < $scope.CartItems.length; i++)
            $scope.Total += $scope.CartItems[i].Price;
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
    $scope.CheckOut = function() {
        $http.post("/Cart/CheckOut").success(function(response) {
            $scope.Message = response;
            $scope.CartItems = [];
            $scope.Show = true;
            $scope.Total = 0;
        });
    }
})