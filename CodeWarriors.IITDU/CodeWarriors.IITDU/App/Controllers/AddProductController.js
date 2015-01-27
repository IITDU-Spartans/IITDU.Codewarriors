app.controller("AddProductController", function ($scope, $http) {
    $scope.EditMode = true;
    $scope.Product = {};
    $scope.NewProduct = {};
    $scope.Message = "";
    $scope.ResetInfo = function () {
        $scope.CopyProfileObject($scope.NewProduct, $scope.Product);
    }

    $scope.AddProduct = function () {
        $http.post("/Product/AddProduct", $scope.NewProduct).success(function (response) {
            $scope.CopyProfileObject($scope.Product, $scope.NewProduct);
            $scope.EditMode = false;
            $scope.Message = response;
        });
    }

    $scope.CopyProfileObject = function (a, b) {
        a.ProductName = b.ProductName;
        a.CategoryName = b.CategoryName;
        a.Price = b.Price;
        a.ImageUrl = b.ImageUrl;
        a.Description = b.Description;
    }
});