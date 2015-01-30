app.controller("ProductListController", function ($scope, $http, $location) {

    $scope.size = 6;
    $scope.index = 0;
    $scope.selectedCategory = "";
    $scope.selectedSubCategory = "";
    $scope.categories = [{ "id": 1, "name": "Apparels", "subcategories": ["Male", "Female", "Child"] },
        { "id": 2, "name": "Electronics", "subcategories": ["Computers", "Mobiles", "Accessories"] },
        { "id": 3, "name": "Foods", "subcategories": ["Fresh", "Dry", "Others"] },
        { "id": 4, "name": "Accessories", "subcategories": ["Sunglasses", "Watches", "Jewelleries"] },
        { "id": 5, "name": "Miscellaneous", "subcategories": ["Gift Items", "Perfume", "Others"] }];

    init();

    function init() {
        //$http.get("/Product/GetAllProducts").success(function (response) {
        //    $scope.Products = response;
        //});
        $http.get("/Product/GetProducts?index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
            $scope.Products = response;
            $scope.index++;
        });

    }

    $scope.onScroll = function () {
        if ($scope.selectedCategory == "" && $scope.selectedSubCategory == "") {
            $http.get("/Product/GetProducts?index=" + $scope.index + "&size=" + $scope.size)
                .success(function (response) {
                    for (var i = 0; i < response.length; i++) {
                        $scope.Products.push(response[i]);
                    }
                    $scope.index++;
                });
        }
        else if ($scope.selectedSubCategory == "") {
            $http.get("/Product/GetCategoryProducts?category=" + $scope.selectedCategory
                + "&index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
                    for (var i = 0; i < response.length; i++) {
                        $scope.Products.push(response[i]);
                    }
                    $scope.index++;
                });

        }
        else {
            $http.get("/Product/GetSubCategoryProducts?category=" + $scope.selectedCategory + "subCategory="
                + $scope.selectedSubCategory + "&index=" + $scope.index + "&size=" + $scope.size)
                .success(function (response) {
                    for (var i = 0; i < response.length; i++) {
                        $scope.Products.push(response[i]);
                    }
                    $scope.index++;
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

    $scope.GetSubCategoryProducts = function (categoryName, subCategoryName) {
        console.log(categoryName + "-" + subCategoryName);
        $scope.index = 0;
        $scope.selectedCategory = categoryName;
        $scope.selectedSubCategory = subCategoryName;
        $http.get("/Product/GetSubCategoryProducts?category=" + categoryName + "subCategory=" + subCategoryName + "&index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
            $scope.Products = response;
            $scope.index++;
        });
    }

    $scope.GetCategoryProducts = function (categoryName) {
        console.log(categoryName);
        $scope.index = 0;
        $scope.selectedCategory = categoryName;
        $http.get("/Product/GetCategoryProducts?category=" + categoryName + "&index=" + $scope.index + "&size=" + $scope.size).success(function (response) {
            $scope.Products = response;
            $scope.index++;
        });
    }
});