app.controller("UserProfileController", function ($scope, $http, uploadManager, $rootScope, $stateParams, $location) {
    init();

    $scope.ProfileInfo = {};
    $scope.MapSource = "";

    function init() {
        var userId = parseInt($stateParams.userId);
        $http.get("/Profile/GetProfileInformationByUserId?userId=" + userId).success(function (response) {
            $scope.ProfileInfo = response;
            if ($scope.ProfileInfo.ImageUrl == null) {
                $scope.ProfileInfo.ImageUrl = "Upload//default.jpg";
            }
            $scope.MapSource = "/Map/ShowMap?location=" + $scope.ProfileInfo.Location;
        });
    }
});