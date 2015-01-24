app.directive("productItem", function () {
    return {
        restrict: 'EA',
        template: '<div class="well">' +
                    '<a href=""><h3 ui-sref="product({productId:productInfo.ProductId})">{{productInfo.ProductName}}</h3></a>' +
                    ' {{productInfo.Description}}<br/>{{productInfo.Price}}' +
                    '<span class="pull-right"><img src="../../Content/images/wish_list.png" height=20px" width="20px" data-ng-click="addToWishlist()" title="Add to Wishlist" />' +
                    '&nbsp;&nbsp;&nbsp;<img src="../../Content/images/shoping_cart.png" height=20px" width="20px" data-ng-click="addToCart(productInfo.ProductId)" title="Add to Cart" /></span>' +
                  '</div>',
        scope: {
            productInfo: '=info',
            addToWishlist: '&',
            addToCart: '&'
        },
        link: function (scope, element, attr) {
            
        }
    };

});