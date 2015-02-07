app.controller("ProductController", function ($scope, $http, ProductService, $stateParams, $location, $rootScope) {

    init();
    function init() {
        $scope.Product = {};
        $scope.ReleventProducts = [];
        $scope.ReleventProductImages = [];
        $scope.Reviews = [];
        $scope.slides = [];
        var productId = parseInt($stateParams.productId);
        $http.get("/Product/GetProduct?ProductId=" + productId).success(function (response) {
            $scope.Product = response.product;
            $scope.IsOwner = response.IsOwner;
            $scope.images = response.images;
            for (var i = 0; i < $scope.images.length; i++)
                $scope.slides.push({ image: $scope.images[i] });
        });

        $http.get("/Review/GetReviews?ProductId=" + productId).success(function (response) {
            $scope.Reviews = response;
            $scope.Reviews.Time = angular.toJson($scope.Reviews.Time);
        });


        $http.get("/Product/GetRelevantProducts?productId=" + productId).success(function (response) {
            for (var i = 0; i < response.products.length; i++) {
                $scope.ReleventProducts.push({ Info: response.products[i], ImageUrl: response.images[i] });
                for (var j = 0; j < response.images[i].length; j++) {
                    $scope.ReleventProductImages.push({ 'text': response.products[i].ProductName, 'image': "Upload/" + response.images[i][j] });
                }
                $rootScope.$broadcast('ImagesPushed', "hehe");
            }
        });
        $scope.statusMessage = "Show Reviews";
        $scope.show = false;
    }
    $scope.Rate = function () {
        var data = {
            productId: $scope.Product.ProductId,
            rating: $scope.Product.AverageRate
        };
        $http.post("/Rating/AddRating", data).success(function (response, status) {
            if (status == 200)
                $scope.Product.AverageRate = response;
            else $location.path("login");
        });
    }
    $scope.addToWishlist = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/WishList/AddToWishList", data).success(function (response, status) {
            if (status == 200) {
                $scope.Message = response;
                alert(response);
            }
            else $location.path("login");
        }).error(function () {
            $location.path("login");
        });
    }

    $scope.addToCart = function (id) {

        var cartItem = {
            ProductId: id,
            Quantity: 1
        };
        $http.post("/Cart/AddToCart/", cartItem).success(function (response, status) {
            if (status == 200) {
                $scope.Message = response;
                alert(response);
            }
            else $location.path("login");
        }).error(function () {
            $location.path("login");
        });
    }
});