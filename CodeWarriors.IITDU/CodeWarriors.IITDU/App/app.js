var app = app || angular.module('CodeWarriorsApp', ["ui.router", "ui.bootstrap"]);
app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

    $urlRouterProvider.otherwise("/home");

    $stateProvider
            .state("home", { url: "/home", templateUrl: "/App/Views/home.html" })
            .state("products", { url: "/products", templateUrl: "/App/Views/products.html", controller: "ProductListController" })
            .state("login", { url: "/login", templateUrl: "/Account/Login" })
            .state("register", { url: "/register", templateUrl: "/Account/Register" })
            .state("profile", { url: "/profile", templateUrl: "/App/Views/profile.html", controller: "ProfileController" })
            .state("password", { url: "/password", templateUrl: "/Account/Edit" })
            .state("addProduct", { url: "/addProduct", templateUrl: "/App/Views/addProduct.html", controller: "AddProductController" })
            .state('product', { url: '/product/:productId', templateUrl: "/App/Views/product.html", controller: "ProductController" })
            .state("cart",{url:"/cart", templateUrl:"/App/Views/cart.html", controller:"CartController"});

    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';;
});
