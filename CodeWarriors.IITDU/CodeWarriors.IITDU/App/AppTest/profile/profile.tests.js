describe('ProfileController', function () {
    var scope;
    beforeEach(angular.mock.module('CodeWarriorsApp'));

    beforeEach(angular.mock.inject(function ($rootScope, $controller, _$httpBackend_) {
        $httpBackend = _$httpBackend_;
        $httpBackend.when('GET', '/Profile/GetProfileInformation').
            respond({ FirstName: "Sponge", LastName: 'Bob' });
        $httpBackend.when('GET', '/Account/IsAuthenticated').
            respond(true);
        scope = $rootScope.$new();
        $controller('ProfileController', { $scope: scope });
    }));

    it('should have EditMode = "false"', function () {
        expect(scope.EditMode).toBe(false);
    });

    it('should fetch profile info of user', function () {
        $httpBackend.flush();
        expect(scope.ProfileInfo.FirstName).toBe('Sponge');
        expect(scope.ProfileInfo.LastName).toBe('Bob');
    });
});