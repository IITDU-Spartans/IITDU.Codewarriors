var app = app || angular.module('CodeWarriorsApp', ["ui.router", "ui.bootstrap"]);
app.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise("/home");

    $stateProvider
            .state("home", { url: "/home", templateUrl: "/App/Views/home.html" })
            .state("products", { url: "/products", templateUrl: "/App/Views/products.html" })
            .state("login", { url: "/login", templateUrl: "/Account/Login" })
            .state("register", { url: "/register", templateUrl: "/Account/Register" })
            .state("profile", { url: "/profile", templateUrl: "/App/Views/profile.html", controller:"ProfileController" })
            .state("password", { url: "/password", templateUrl: "/Account/Edit" });
});
