/**
**/
Northwind.Router.map(function () {
    this.resource('customers', /*{ queryParams: ['offset', 'limit'] },*/ function () {
        this.resource('customer', { path: ':customer_id' });
    });
    this.resource('about');
});

/**
**/
Northwind.Router.reopen({
    location: 'history'
});