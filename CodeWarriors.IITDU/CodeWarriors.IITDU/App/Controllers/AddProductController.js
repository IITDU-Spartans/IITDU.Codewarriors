
app.controller("AddProductController", function ($, $scope, $rootScope, $http, $location, addProductFactory, uploadManager) {
    $scope.files = [];
    init();
    $scope.SortedFormList = [];
    $scope.DynamicFormList = [];
    $scope.NewProduct = {};
    $scope.NewProduct.Dynamic = {};
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

    $scope.AddProduct = function () {
        if (typeof $scope.NewProduct.Dynamic === 'undefined') {
            $scope.Message = 'Set Product Title';
            return;
        }
        if (typeof $scope.NewProduct.Dynamic[0] === 'undefined' || $scope.NewProduct.Dynamic[0] == '') {
            $scope.Message = 'Set Product Title';
            return;
        }
        if (typeof $scope.NewProduct.Dynamic[1] === 'undefined' || $scope.NewProduct.Dynamic[1] == '') {
            $scope.Message = 'Set Product Category';
            return;
        }
        if (typeof $scope.NewProduct.Dynamic[2] === 'undefined' || $scope.NewProduct.Dynamic[2] == '') {
            $scope.Message = 'Set Product Sub Category';
            return;
        }
        if ($scope.files.length < 3) {
            $scope.Message = 'Upload at Least 3 Images';
            return;
        }
        for (var i = 0; i < $scope.SortedFormList.length; i++) {
            if (typeof $scope.NewProduct.Dynamic[i] === 'undefined' || $scope.NewProduct.Dynamic[i] == '') {
                $scope.Message = 'Complete the Form Fields';
                return;
            }
        }
        uploadManager.uploadProduct();
    }

    $scope.RemoveImage = function (fileName) {
        uploadManager.remove(fileName);
    }

    $scope.$on('fileAdded', function (e, call) {
        $scope.files.push(call);
        $scope.$apply();
    });

    $scope.$on('fileRemoved', function (e, call) {
        $scope.files.splice(call, 1);
        //$scope.$apply();
    });

    $scope.$on('uploadProductDone', function (e, call) {
        var data = [];
        for (var i = 0; i < $scope.SortedFormList.length; i++) {
            if (i == 1) {
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