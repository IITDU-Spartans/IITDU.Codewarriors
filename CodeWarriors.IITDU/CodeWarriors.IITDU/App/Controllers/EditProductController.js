app.controller("EditProductController", function ($scope, $http, $stateParams, uploadManager, $rootScope) {
    $scope.files = [];
    init();
    $scope.SortedFormList = [];
    $scope.DynamicFormList = [];

    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
        var productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?productId=" + productId).success(function (response) {
            //$scope.NewProduct = response.product;
            //$scope.NewProduct.CategoryName = response.CategoryName;
            for (var i = 0; i < response.product.length; i++) {
                $scope.NewProduct.Dynamic[i] = response.product[i];
            }
        });
    }

    $scope.subcategories = [];
    $scope.categories = [{ category: "Apparels", subcategories: ["Male", "Female", "Child"] },
        { category: "Electronics", subcategories: ["Computers", "Mobiles", "Accessories"] },
        { category: "Foods", subcategories: ["Fresh", "Dry", "Others"] },
        { category: "Miscellaneous", subcategories: ["Gift Items", "Perfume", "Others"] }];
    $scope.NewProduct = {};
    $scope.Message = "";
    $scope.ResetInfo = function () {
        for (var i = 0; i < $scope.SortedFormList.length; i++) {
            $scope.NewProduct.Dynamic[i] = $scope.Product.Dynamic[i];
        }
        $scope.files = [];
    }

    $scope.UpdateSubcategory = function () {
        $scope.subcategories = $scope.NewProduct.Dynamic[1].subcategories;
    }

    $scope.UpdateFormFields = function () {
        $http.get("/Product/GetFormElements?category=" + $scope.NewProduct.Dynamic[1].category).success(function (response) {
            $scope.SortedFormList = response;
            $scope.DynamicFormList = [];
            for (var i = 6; i < response.length; i++) {
                $scope.DynamicFormList.push(response[i]);
            }
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