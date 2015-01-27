app.value('$', $);

app.factory('addProductFactory', ['$', '$rootScope', function ($, $rootScope) {
    function addNewProductOperations() {

        var connection;
        var proxy;

        var showNewProduct;

        var initializeClient = function () {
            connection = $.hubConnection();
            proxy = connection.createHubProxy('productHub');
            configureProxyClientFunction();
            start();
        };

        var setCallbacks = function (product) {
            showNewProduct = product;
        };

        var configureProxyClientFunction = function () {
            proxy.on('updateAboutNewProduct', function (lastProductData) {
                $rootScope.$apply(showNewProduct(lastProductData));
            });
        };

        var start = function () {
            connection.start().done();
        };

        var updateClientsAboutLastProduct = function (newProduct) {
            proxy.invoke('updateClientsAboutLastProductHub', newProduct);
        };


        return {
            initializeClient: initializeClient,
            setCallbacks: setCallbacks,
            updateClientsAboutLastProduct: updateClientsAboutLastProduct
        }
    };
    return addNewProductOperations;
    
}]);
