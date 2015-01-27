app.controller("WishListController", function ($http, $scope) {
    $scope.Message = "";
    $scope.Show = false;
    $scope.WishList = [];
    $http.get("/WishList/GetWishList").success(function (response) {
        $scope.WishList = response;
    });
    $scope.RemoveFromWishList = function (id) {
        var data = { wishId: parseInt(id) };
        $http.post("/WishList/RemoveFromWishList", data).success(function (response) {
            for (var i = $scope.WishList.length-1; i >=0; i--) {
                if ($scope.WishList[i].WishedProductId == id) {
                    $scope.WishList.splice(i, 1);
                }
            }
            $scope.Show = true;
            $scope.Message = response;
        });
    }
})