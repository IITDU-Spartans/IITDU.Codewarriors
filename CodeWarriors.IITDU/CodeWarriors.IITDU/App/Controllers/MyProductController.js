app.controller("MyProductController", function ($http, $scope) {
    $scope.Products = [];
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
        $http.get("/Product/GetAllProductByUser").success(function (response) {
            $scope.Products = response;
        });
    }

    $scope.RemoveProduct = function (id) {
        if (confirm("Are you sure to remove product?")) {
            var data = { productId: parseInt(id) };
            $http.post("/Product/RemoveProduct", data).success(function (response) {
                for (var i = $scope.Products.length - 1; i >= 0; i--) {
                    if ($scope.Products[i].ProductId == id) {
                        $scope.Products.splice(i, 1);
                    }
                }
                alert(response);
            });
        }
    }

})