app.controller("EditProductController", function ($scope, $http, $stateParams, uploadManager, $rootScope) {
    init();
    $scope.Product = {};
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
        var productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?productId=" + productId).success(function (response) {
            $scope.NewProduct = response.product;
            $scope.NewProduct.CategoryName = response.CategoryName;
            $scope.CopyObject($scope.Product, $scope.NewProduct);
        });
    }

    $scope.UpdateProduct = function () {
        uploadManager.upload();
    }
    $scope.$on('uploadDone', function (e, call) {
        $scope.NewProduct.ImageUrl = uploadManager.files()[0];

        $http.post("/Product/UpdateProduct", $scope.NewProduct).success(function (response) {
            $scope.CopyObject($scope.Product, $scope.NewProduct);
            $scope.EditMode = false;
            $scope.Message = response;
        });

    });


    $scope.$on('fileAdded', function (e, call) {
        $scope.files = [];
        $scope.files.push(call);
        $scope.$apply();
    });

    $scope.ResetInfo = function () {
        $scope.CopyObject($scope.NewProduct, $scope.Product);
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