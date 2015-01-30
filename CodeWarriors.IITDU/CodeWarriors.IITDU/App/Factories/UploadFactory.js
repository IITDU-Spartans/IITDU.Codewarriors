app.factory('uploadManager', function ($rootScope) {
    var _files = [];

    return {
        add: function (file) {
            //this.clear();
            _files.push(file);
            $rootScope.$broadcast('fileAdded', file.files[0].name);
        },
        clear: function () {
            _files = [];
        },
        files: function () {
            var fileNames = [];
            $.each(_files, function (index, file) {
                fileNames.push(file.files[0].name);
            });
            return fileNames;
        },
        uploadProduct: function () {
            $.each(_files, function (index, file) {
                file.submit();
            });
            $rootScope.$broadcast('uploadProductDone', "productDone");
            this.clear();
        },
        uploadProfile: function () {
            $.each(_files, function (index, file) {
                file.submit();
            });
            $rootScope.$broadcast('uploadProfileDone', "profileDone");
            this.clear();
        },
        setProgress: function (percentage) {
            $rootScope.$broadcast('uploadProgress', percentage);
        }
    };
});
