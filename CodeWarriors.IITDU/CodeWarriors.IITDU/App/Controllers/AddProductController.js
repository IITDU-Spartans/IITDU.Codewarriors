
app.controller("AddProductController", function ($, $scope, $rootScope, $http, $location, addProductFactory, uploadManager) {
    $scope.files = [];
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
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
        $scope.NewProduct.ProductName = "";
        $scope.NewProduct.CategoryName = "";
        $scope.NewProduct.AvailableCount = "";
        $scope.NewProduct.Price = "";
        $scope.NewProduct.ImageUrl = "";
        $scope.NewProduct.Description = "";
    }

    $scope.AddProduct = function () {
        uploadManager.uploadProduct();
    }
    $scope.$on('uploadProductDone', function (e, call) {
        $scope.NewProduct.ImageUrl = uploadManager.files()[0];

        $http.post("/Product/AddProduct", $scope.NewProduct).success(function (response) {
            $scope.ResetInfo();
            $scope.Message = response.Message;
            $location.path("product/" + response.ProductId);
        });

    });
    $scope.UpdateSubcategory = function () {
        $scope.subcategories = $scope.NewProduct.Category.subcategories;
    }
    $scope.$on('fileAdded', function (e, call) {
        $scope.files.push(call);
        $scope.$apply();
    });

});