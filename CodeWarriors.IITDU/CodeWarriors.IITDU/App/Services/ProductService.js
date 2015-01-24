app.service('ProductService', function () {
    this.getProducts = function () {
        return products;
    };

    this.insertProduct = function (title, description, price) {
        var topID = products.length + 1;
        products.push({
            id: topID,
            title: title,
            description: description,
            price: price
        });
    };

    this.deleteProduct = function (id) {
        for (var i = products.length - 1; i >= 0; i--) {
            if (products[i].id === id) {
                products.splice(i, 1);
                break;
            }
        }
    };

    this.getProduct = function (id) {
        for (var i = 0; i < products.length; i++) {
            if (products[i].id === id) {
                return products[i];
            }
        }
        return null;
    };

    var products = [
        {
            id: 1, title: 'Mobile', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 2, title: 'MobileA', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 3, title: 'MobileB', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 4, title: 'MobileC', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 5, title: 'MobileD', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 6, title: 'MobileE', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 7, title: 'MobileF', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 8, title: 'MobileG', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 9, title: 'MobileH', description: 'Sony Ericson', price: '1234'
        },
        {
            id: 10, title: 'MobileI', description: 'Sony Ericson', price: '1234'
        }
    ];

});