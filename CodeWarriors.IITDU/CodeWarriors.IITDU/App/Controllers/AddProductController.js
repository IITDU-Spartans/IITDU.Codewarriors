
app.controller("AddProductController", function ($, $scope, $rootScope, $http, $location, addProductFactory, uploadManager) {
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
            $scope.NewProduct.Dynamic[i] = '';
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
    $scope.$on('fileAdded', function (e, call) {
        $scope.files.push(call);
        $scope.$apply();
    });

    $scope.AddProduct = function () {
        uploadManager.uploadProduct();
    }
    $scope.$on('uploadProductDone', function (e, call) {
        $scope.NewProduct.ImageUrl = uploadManager.files()[0];
        var data = [];
        for (var i = 0; i < $scope.SortedFormList.length; i++) {
            if(i==1) {
                data.push($scope.NewProduct.Dynamic[i].category);
            }
            else data.push($scope.NewProduct.Dynamic[i]);
        }
        var imageFiles = uploadManager.files();
        for (var i = 0; i < imageFiles.length; i++) {
            data.push(imageFiles[i]);
        }
        $http.post("/Product/AddProduct", data).success(function (response) {
            $scope.ResetInfo();
            $scope.Message = response.Message;
            $location.path("product/" + response.ProductId);
        });

    });
});