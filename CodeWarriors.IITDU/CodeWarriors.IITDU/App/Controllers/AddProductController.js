
app.controller("AddProductController", function ($, $scope, $rootScope, $http, $location, addProductFactory, uploadManager) {
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
    }
    var addProductFactoryOperations = addProductFactory();
    $scope.EditMode = true;
    $scope.Product = {};
    $scope.NewProduct = {};
    $scope.Message = "";
    $scope.ResetInfo = function () {
        $scope.CopyProfileObject($scope.NewProduct, $scope.Product);
    }

    $scope.AddProduct = function () {
        uploadManager.upload();
    }
    $rootScope.$on('uploadDone', function (e, call) {
        $scope.NewProduct.ImageUrl = uploadManager.files()[0];
        
        $http.post("/Product/AddProduct", $scope.NewProduct).success(function (response) {
            $scope.CopyProfileObject($scope.Product, $scope.NewProduct);
            $scope.EditMode = false;
            $scope.Message = response;

            addProductFactoryOperations.updateClientsAboutLastProduct($scope.NewProduct);
        });

    });


    $rootScope.$on('fileAdded', function (e, call) {
        $scope.files = [];
        $scope.files.push(call);
        $scope.$apply();
    });

    var showNewProductFunc = function (productData) {
        alert(productData);
    };

    addProductFactoryOperations.setCallbacks($rootScope.getAddedProduct);
    //addProductFactoryOperations.setCallbacks(showNewProductFunc);
    addProductFactoryOperations.initializeClient();

    $scope.CopyProfileObject = function (a, b) {
        a.ProductName = b.ProductName;
        a.CategoryName = b.CategoryName;
        a.Price = b.Price;
        a.ImageUrl = b.ImageUrl;
        a.Description = b.Description;
    }
});