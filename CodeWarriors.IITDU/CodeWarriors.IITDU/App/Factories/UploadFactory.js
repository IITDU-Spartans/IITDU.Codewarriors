app.factory('uploadManager', function ($rootScope) {
    var _files = [];

    return {
        add: function (file) {
            //this.clear();
            var fileExists = false;
            for (var i = 0; i < _files.length; i++) {
                if (_files[i].files[0].name == file.files[0].name) {
                    fileExists = true;
                    break;
                }
            }
            if (!fileExists) {
                _files.push(file);
                $rootScope.$broadcast('fileAdded', file.files[0].name);
            }
        },
        remove: function (fileName) {
            for (var i = 0; i < _files.length; i++) {
                if (_files[i].files[0].name == fileName) {
                    _files.splice(i, 1);
                    break;
                }
            }
            $rootScope.$broadcast('fileRemoved', i);
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
