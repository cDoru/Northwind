Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        return this.store.find('customer', params.customer_id);
    }
});