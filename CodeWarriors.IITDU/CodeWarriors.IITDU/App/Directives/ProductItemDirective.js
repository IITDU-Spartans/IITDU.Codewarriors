app.directive("productItem", function () {
    return {
        restrict: 'EA',
        template: '<div class="col-md-4">\
					<div class="thumbnail">\
						<img data-ng-src="Upload/{{imageUrl[0]}}" height="400" width="380"\
    alt="Product Image"/>\
<div class="caption">\
    <a class="img-heading" ui-sref="product({productId:productInfo.ProductId})"><div style="font-size:24px;">{{productInfo.ProductName}}</div></a>\
    <p>\
        <b>Price</b>: {{productInfo.Price}}<br /> <b>Category</b>:\
    {{productInfo.CatagoryName}}\
    </p>\
        <a class="btn btn-primary" title="Add to Cart" data-ng-click="addToCart({productId:productInfo.ProductId})">\
                                    <span class="fa fa-shopping-cart"></span> Add to Cart\
                                </a>\
                                <a class="btn btn-info" title="Add to Wishlist" data-ng-click="addToWishlist({productId:productInfo.ProductId})">\
                                    <i class="fa fa-heart"></i> Add to Wishlist\
                                </a>\
						</div>\
					</div>\
				</div>',
        scope: {
            productInfo: '=info',
            imageUrl: '=image',
            addToWishlist: '&',
            addToCart: '&',
            rate: '&'
        },
        link: function (scope, element, attr) {
        }
    };

});