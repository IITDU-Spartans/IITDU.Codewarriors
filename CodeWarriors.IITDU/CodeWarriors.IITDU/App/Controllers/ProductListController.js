app.controller("ProductListController", function ($scope, $http, $location) {

    $scope.size = 6;
    $scope.index = 0;
    $scope.selectedCategory = "";
    $scope.selectedSubCategory = "";
    $scope.CalledMoreProduct = false;
    $scope.categories = [{ "id": 1, "name": "Apparels", "subcategories": ["Male", "Female", "Child"] },
        { "id": 2, "name": "Electronics", "subcategories": ["Computers", "Mobiles", "Accessories"] },
        { "id": 3, "name": "Foods", "subcategories": ["Fresh", "Dry", "Others"] },
        { "id": 4, "name": "Accessories", "subcategories": ["Sunglasses", "Watches", "Jewelleries"] },
        { "id": 5, "name": "Miscellaneous", "subcategories": ["Gift Items", "Perfume", "Others"] }];
    $scope.range = {
        minPrice: 0,
        maxPrice: 5000000
    };
    $scope.sortparam = '-Info.PurchaseCount';

    init();

    function init() {
        //$http.get("/Product/GetAllProducts").success(function (response) {
        //    $scope.Products = response;
        //});
        $http.get("/Product/GetProducts?index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
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
            if ($scope.selectedCategory == "" && $scope.selectedSubCategory == "") {
                $http.get("/Product/GetProducts?index=" + $scope.index + "&size=" + $scope.size)
                    .success(function (response) {
                        for (var i = 0; i < response.products.length; i++) {
                            $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
                        }
                        $scope.index++;
                        $scope.CalledMoreProduct = false;
                    });
            }
            else if ($scope.selectedSubCategory == "") {
                $http.get("/Product/GetProductByCategory?category=" + $scope.selectedCategory
                    + "&index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
                        for (var i = 0; i < response.products.length; i++) {
                            $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
                        }
                        $scope.index++;
                        $scope.CalledMoreProduct = false;
                    });
            }
            else {
                $http.get("/Product/GetProductBySubCategory?category=" + $scope.selectedCategory + "subCategory="
                    + $scope.selectedSubCategory + "&index=" + $scope.index + "&size=" + $scope.size)
                    .success(function (response) {
                        for (var i = 0; i < response.length; i++) {
                            $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
                        }
                        $scope.index++;
                        $scope.CalledMoreProduct = false;
                    });
            }
        }
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
    $scope.Rate = function (ratedValue, id) {
        var data = {
            productId: id,
            rating: ratedValue
        };
        $http.post("/Rating/AddRating", data).success(function (response) {
            alert(response);
        });
    }

    $scope.GetSubCategoryProducts = function (categoryName, subCategoryName) {
        console.log(categoryName + "-" + subCategoryName);
        $scope.index = 0;
        $scope.selectedCategory = categoryName;
        $scope.selectedSubCategory = subCategoryName;
        $http.get("/Product/GetProductBySubCategory?category=" + categoryName + "&subCategory=" + subCategoryName + "&index=" + $scope.index + "&size=" + $scope.size).success(function (response) {

            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
            }
            $scope.index++;
        });
    }

    $scope.GetCategoryProducts = function (categoryName) {
        console.log(categoryName);
        $scope.index = 0;
        $scope.selectedCategory = categoryName;
        $http.get("/Product/GetProductByCategory?category=" + categoryName + "&index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
            }
            $scope.index++;
        });
    }


    $scope.GetAllProducts = function () {
        $scope.index = 0;
        $scope.selectedCategory = "";
        $scope.selectedSubCategory = "";
        $http.get("/Product/GetProducts?index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ Info: response.products[i], ImageUrl: response.images[i] });
            }
            $scope.index++;
        });
    }
});