app.controller("CartController",function($http, $scope) {
    $scope.CartItems = [];
    $http.get("/Cart/GetAllCartItems").success(function (response) {
        $scope.CartItems = response;
    });
})