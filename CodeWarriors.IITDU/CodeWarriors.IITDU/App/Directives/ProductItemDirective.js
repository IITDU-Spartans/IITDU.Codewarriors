app.directive("productItem", function () {
    return {
        restrict: 'EA',
        template: '<div class="col-md-4">\
					<div class="thumbnail">\
						<img data-ng-src="/{{productInfo.ImageUrl}}" height="400" width="380"\
    alt="Product Image"/>\
<div class="caption">\
    <a class="img-heading" ui-sref="product({productId:productInfo.ProductId})"><div style="font-size:24px;">{{productInfo.ProductName}}</div></a>\
    <p>\
        <b>Price</b>: {{productInfo.Price}}<br /> <b>Category</b>:\
    {{productInfo.CategoryName}}\
    </p>\
        <a class="btn btn-primary" title="Add to Cart" data-ng-click="addToCart(productInfo.ProductId)">\
                                    <span class="fa fa-shopping-cart"></span> Add to Cart\
                                </a>\
                                <a class="btn btn-info" title="Add to Wishlist" data-ng-click="addToWishlist(productInfo.ProductId)">\
                                    <i class="fa fa-heart"></i> Add to Wishlist\
                                </a>\
						</div>\
					</div>\
				</div>',
        scope: {
            productInfo: '=info',
            addToWishlist: '&',
            addToCart: '&',
            rate: '&'
        },
        link: function (scope, element, attr) {
        }
    };

});