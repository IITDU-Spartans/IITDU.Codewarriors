app.controller("SearchController", function ($scope, $http, $location, $stateParams) {

    $scope.size = 6;
    $scope.index = 0;
    $scope.range = {
        minPrice: 0,
        maxPrice: 5000000
    };
    $scope.sortparam = '-Info.PurchaseCount';
    $scope.CalledMoreProduct = false;

    init();

    function init() {
        $scope.SearchText = $stateParams.searchText;
        $http.get("/Search/Search?query=" + $scope.SearchText + "&index=" + $scope.index + "&size=" + $scope.size)
            .success(function (response) {
                $scope.Products = [];
                for (var i = 0; i < response.products.length; i++) {
                    $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
                }
                $scope.index++;
            });

    }
    $scope.FilterByPriceRange = function (p) {
        return p.Info.Price > $scope.range.minPrice && p.Info.Price <= $scope.range.maxPrice;
    };
    $scope.onScroll = function () {
        if ($scope.CalledMoreProduct == false) {
            $scope.CalledMoreProduct = true;
            $http.get("/Search/Search?query=" + $scope.SearchText + "&index=" + $scope.index + "&size=" + $scope.size)
            .success(function (response) {
                for (var i = 0; i < response.products.length; i++) {
                    $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
                }
                $scope.index++;
                $scope.CalledMoreProduct = false;
            });
        }

    }

    $scope.addToWishlist = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/WishList/AddToWishList", data).success(function (response, status) {
            if (status == 200)
                $scope.Message = response;
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
            if (status == 200)
                $scope.Message = response;
            else $location.path("login");
        }).error(function () {
            $location.path("login");
        });
    }
    $scope.Rate = function (ratedValue, id) {
        var data = {
            productId: id,
            rating: ratedValue
        };
        $http.post("/Rating/AddRating", data).success(function (response) {
            alert(response);
        });
    }
});