app.controller("ProfileController", function ($scope, $http) {
    $scope.EditMode = false;
    $scope.ProfileInfo = {};
    $scope.NewProfileInfo = {};
    $scope.Message = "";
    $http.get("/Profile/GetProfileInformation").success(function (response) {
        $scope.ProfileInfo = response;
    });

    $scope.EditProfile = function () {
        $scope.EditMode = true;
        $scope.CopyProfileObject($scope.NewProfileInfo, $scope.ProfileInfo);
        $scope.Message = "";
    }

    $scope.CancelEdit = function () {
        $scope.EditMode = false;
    }

    $scope.ResetInfo = function () {
        $scope.CopyProfileObject($scope.NewProfileInfo, $scope.ProfileInfo);
    }

    $scope.UpdateProfile = function () {
        $http.post("/Profile/UpdateProfile", $scope.NewProfileInfo).success(function (response) {
            $scope.CopyProfileObject($scope.ProfileInfo, $scope.NewProfileInfo);
            $scope.EditMode = false;
            $scope.Message = response;
        });
    }

    $scope.CopyProfileObject = function (a, b) {
        a.FirstName = b.FirstName;
        a.LastName = b.LastName;
        a.Age = b.Age;
        a.About = b.About;
        a.Country = b.Country;
        a.Email = b.Email;
        a.Gender = b.Gender;
    }

});