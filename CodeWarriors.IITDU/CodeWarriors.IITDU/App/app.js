var app = app || angular.module('CodeWarriorsApp', ["ui.router", "ui.bootstrap"]);
app.config(function ($stateProvider, $urlRouterProvider, $httpProvider) {

    $urlRouterProvider.otherwise("/home");

    $stateProvider
            .state("home", { url: "/home", templateUrl: "/App/Views/home.html", controller: "HomeController" })
            .state("products", { url: "/products", templateUrl: "/App/Views/products.html", controller: "ProductListController" })
            .state("login", { url: "/login", templateUrl: "/Account/Login" })
            .state("register", { url: "/register", templateUrl: "/Account/Register" })
            .state("profile", { url: "/profile", templateUrl: "/App/Views/profile.html", controller: "ProfileController" })
            .state("userProfile", { url: "/profile/:userId", templateUrl: "/App/Views/userProfile.html", controller: "UserProfileController" })
            .state("password", { url: "/password", templateUrl: "/Account/ChangePassword" })
            .state("addProduct", { url: "/addProduct", templateUrl: "/App/Views/addProduct.html", controller: "AddProductController" })
            .state('product', { url: '/product/:productId', templateUrl: "/App/Views/product.html", controller: "ProductController" })
            .state('search', { url: '/search/:searchText', templateUrl: "/App/Views/search.html", controller: "SearchController" })
            .state('editProduct', { url: '/editProduct/:productId', templateUrl: "/App/Views/editProduct.html", controller: "EditProductController" })
            .state("cart", { url: "/cart", templateUrl: "/App/Views/cart.html", controller: "CartController" })
            .state("wishList", { url: "/wishList", templateUrl: "/App/Views/wishlist.html", controller: "WishListController" })
            .state("myProducts", { url: "/myProducts", templateUrl: "/App/Views/myProducts.html", controller: "MyProductController" })
            .state("error", { url: "/error", templateUrl: "/App/Views/error.html" });

    if (!$httpProvider.defaults.headers.get) {
        $httpProvider.defaults.headers.get = {};
    }
    $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
    $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';;
});
