app.controller("EditProductController", function ($scope, $http, $stateParams) {
    init();
    var Product = {};
    function init() {
        var productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?productId=" + productId).success(function (response) {
            $scope.NewProduct = response.product;
            $scope.NewProduct.CategoryName = response.CategoryName;
            $scope.CopyObject(Product, $scope.NewProduct);
        });
    }

    $scope.UpdateProduct = function () {
        var data = {
            ProductName: $scope.NewProduct.ProductName,
            Price: $scope.NewProduct.Price,
            CategoryName: $scope.NewProduct.CategoryName,
            Description: $scope.NewProduct.Description,
            AvailableCount: $scope.NewProduct.AvailableCount,
            ImageUrl: $scope.NewProduct.ImageUrl,
            productId: $scope.NewProduct.ProductId
        };
        $http.post("/Product/UpdateProduct", data).success(function (response) {
            $scope.Message = response;
        });
    }
    $scope.ResetInfo = function () {
        $scope.CopyObject($scope.NewProduct, Product);
    }

    $scope.CopyObject = function (a, b) {
        a.ProductName = b.ProductName;
        a.Price = b.Price;
        a.CategoryName = b.CategoryName;
        a.Description = b.Description;
        a.AvailableCount = b.AvailableCount;
        a.ImageUrl = b.ImageUrl;
    }
})