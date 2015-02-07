app.controller("MyProductController", function ($http, $scope) {
    $scope.Products = [];
    $scope.ProductCoverPic = [];
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
        $http.get("/Product/GetAllProductByUser").success(function (response) {
            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ product: response.products[i], coverPic: response.images[i][0] });
            }
        });
        $scope.data = {};
    }

    $scope.RemoveProduct = function (id) {
        if (confirm("Are you sure to remove product?")) {
            var data = { productId: parseInt(id) };
            $http.post("/Product/RemoveProduct", data).success(function (response) {
                for (var i = $scope.Products.length - 1; i >= 0; i--) {
                    if ($scope.Products[i].ProductId == id) {
                        $scope.Products.splice(i, 1);
                    }
                }
                alert(response);
            });
        }
    }

    $scope.Added = function () {
        $http.get("/Product/GetAllProductByUser").success(function (response) {
            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ product: response.products[i], coverPic: response.images[i][0] });
            }
        });
    }

    $scope.Purchased = function () {
        $http.get("/Product/GetPurchasedProducts").success(function (response) {
            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ product: response.products[i], coverPic: response.images[i][0] });
            }
        });
    }

    $scope.Sold = function () {
        //$http.get("/Product/GetSoldProducts").success(function (response) {
        //    $scope.Products = [];
        //    for (var i = 0; i < response.products.length; i++) {
        //        $scope.Products.push({ product: response.products[i], coverPic: response.images[i][0] });
        //    }
        //});
        $http.get("/Product/GetGroupedProductsByBuyers").success(function (response) {
            $scope.Products = [];
            for (var i = 0; i < response.length; i++) {
                $scope.Products.push(response[i]);
            }
        });
    }

    $scope.Queued = function () {
        $http.get("/Cart/GetWaitingForDeliveryProducts").success(function (response) {
            $scope.Products = [];
            $scope.orderIds = response.orderIds;
            $scope.Products = [];
            for (var i = 0; i < response.products.length; i++) {
                $scope.Products.push({ product: response.products[i], coverPic: response.images[i][0], buyer: 0 });
            }
            var insideLoop = 0;
            for (var i = 0; i < $scope.orderIds.length; i++) {
                $http.get("/Product/GetBuyerId?orderId=" + $scope.orderIds[i]).success(function (response) {
                    $scope.Products[insideLoop].buyer = response;
                    insideLoop++;
                });
            }

        });
    }

    $scope.ApproveDelivery = function (id) {
        $scope.data.orderId = $scope.orderIds[id];
        $scope.data.deliveryStatus = true;
        $http.post("/Cart/ChangeOrderStatus", $scope.data).success(function (response) {
            $scope.Products.splice(id, 1);
        });
    }
    $scope.CancelDelivery = function (id) {
        if (confirm("Are you sure to cancel delivery?")) {
            $scope.data.orderId = $scope.orderIds[id];
            $scope.data.deliveryStatus = false;
            $http.post("/Cart/ChangeOrderStatus", $scope.data).success(function (response) {
                $scope.Products.splice(id, 1);
            });
        }
    }
})