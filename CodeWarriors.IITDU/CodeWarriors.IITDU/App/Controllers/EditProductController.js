app.controller("EditProductController", function ($scope, $http, $stateParams, uploadManager, $rootScope, $location) {

    init();
    $scope.NewProduct = {};
    $scope.Product = {};
    var productId;

    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
        productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?productId=" + productId).success(function (response) {
            $scope.NewProduct = response.product;
            $scope.NewProduct.ProductId = productId;
            $scope.CopyObject($scope.Product, $scope.NewProduct);
        });
    }
    $scope.Message = "";

    $scope.ResetInfo = function () {
        $scope.CopyObject($scope.NewProduct, $scope.Product);
    }

    $scope.UpdateProduct = function () {
        $http.post("/Product/UpdateProduct", $scope.NewProduct).success(function (response) {
            $scope.CopyObject($scope.Product, $scope.NewProduct);
            $scope.Message = response;
            $location.path("product/" + productId);
        });
    }

    $scope.CopyObject = function (a, b) {
        a.ProductName = b.ProductName;
        a.Price = b.Price;
        a.CategoryName = b.CategoryName;
        a.SubCatagoryName = b.SubCatagoryName;
        a.Description = b.Description;
        a.AvailableModels = b.AvailableModels;
        a.AvailableSizes = b.AvailableSizes;
        a.AvailableCount = b.AvailableCount;
        a.Manufacturer = b.Manufacturer;
    }
})