app.controller("ProfileController", function ($scope, $http, uploadManager, $rootScope, $location) {
    init();
    function init() {
        $http.get("/Account/IsAuthenticated").success(function (response) {
            if (!response) {
                $location.path("home");
            }
        });
    }
    $scope.EditMode = false;
    $scope.ProfileInfo = {};
    $scope.NewProfileInfo = {};
    $scope.Message = "";
    $http.get("/Profile/GetProfileInformation").success(function (response) {
        $scope.ProfileInfo = response;
        if ($scope.ProfileInfo.ImageUrl == null) {
            $scope.ProfileInfo.ImageUrl = "Upload//default.jpg";
        }
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


    $scope.files = [];

    $scope.percentage = 0;

    $scope.upload = function () {
        uploadManager.upload();
    };

    $rootScope.$on('uploadDone', function (e, call) {
        $scope.CopyProfileObject($scope.NewProfileInfo, $scope.ProfileInfo);
        $scope.NewProfileInfo.ImageUrl = uploadManager.files()[0];
        $http.post("/Profile/UpdateProfile", $scope.NewProfileInfo).success(function(response) {
            alert('done');
        });
        
    });


    $rootScope.$on('fileAdded', function (e, call) {
        $scope.files = [];
        $scope.files.push(call);
        $scope.$apply();
    });

});