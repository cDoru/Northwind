Northwind.Router.map(function () {
    this.resource('customers', function () {
        this.resource('customer', { path: ':customer_id' });
    });
    this.resource('about');
});