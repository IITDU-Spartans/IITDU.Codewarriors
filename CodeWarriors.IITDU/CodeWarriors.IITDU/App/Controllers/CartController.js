app.controller("CartController", function ($http, $scope, $location) {
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
    }
    $scope.Message = "";
    $scope.proceeded = false;
    $scope.Show = false;
    $scope.Total = parseInt(0);
    $scope.CartItems = [];
    $scope.dynamic = [];
    $http.get("/Cart/GetAllCartItems").success(function (response) {
        $scope.CartItems = response;
        for (var i = 0; i < $scope.CartItems.length; i++)
            $scope.Total += $scope.CartItems[i].Price;
    });
    $scope.RemoveFromCart = function (id) {
        var data = { productId: parseInt(id) };
        $http.post("/Cart/RemoveFromCart", data).success(function (response) {
            for (var i = $scope.CartItems.length - 1; i >= 0; i--) {
                if ($scope.CartItems[i].ProductId == id) {
                    $scope.CartItems.splice(i, 1);
                }

            }
            $scope.Show = true;
            $scope.Message = response;
        });
    }
    $scope.CheckOut = function () {
        $scope.Show = true;
        $scope.CartModels = [];
        $http.get("/Cart/GetCartItemsBySeller").success(function (response) {
            $scope.CartModels = response;
            for (var i = 0; i < $scope.CartModels.length; i++) {
                $scope.CartModels[i].Reviews = [];
            }
        });
    }
    $scope.SubmitReview = function (index) {
        //alert(index);
        if ($scope.CartModels[index].Reviews != null)
            for (var i = 0; i < $scope.CartModels[index].Reviews.length; i++) {
                if (typeof $scope.CartModels[index].Reviews[i] === "undefined" || $scope.CartModels[index].Reviews[i] == "") {
                    //alert(i);
                }
                else {
                    $scope.data = {};
                    $scope.data.productId = $scope.CartModels[index].ProductIds[i];
                    $scope.data.review = $scope.CartModels[index].Reviews[i];

                    //alert($scope.data.review + " " + $scope.data.id);
                    $http.post("/Review/AddReview", $scope.data).success(function (response) {

                    });
                }
            }
        $http.post("/Cart/CheckOut").success(function (response) {
            $scope.Message = response;
            $scope.CartItems = [];
            $scope.Show = true;
            $scope.Total = 0;
            $scope.Message = "Your request for checkout is processing...Thank you.";
            $scope.proceeded = true;

            $location.path('myProducts');
        });
    }
    $scope.RateSeller = function (id, rate) {
        $scope.data = {};
        $scope.data.sellerId = id;
        $scope.data.rating = rate;
        //alert(id + " " + rate);
        $http.post("/Rating/AddRating", $scope.data).success(function (response) {

        });
    }
    $scope.Proceed = function () {
        $http.post("/Cart/CheckOut").success(function (response) {
            $scope.Message = response;
            $scope.CartItems = [];
            $scope.Show = true;
            $scope.Total = 0;
            $scope.Message = "Your request for checkout is processing...Thank you.";
            $scope.proceeded = true;

            $location.path('myProducts');
        });
    }
});