/**

    Northwind main application

**/

Northwind = Ember.Application.create();

/**

    Model

**/

Northwind.Model = DS.Model.extend();

// Customer
Northwind.Customer = Northwind.Model.extend({
    id: DS.attrib('string'),
    companyName: DS.attrib('string'),
    contactName: DS.attrib('string'),
    contactTitle: DS.attrib('string'),
    address: DS.attrib('string'),
    city: DS.attrib('string'),
    region: DS.attrib('string'),
    postalCode: DS.attrib('string'),
    country: DS.attrib('string'),
    phone: DS.attrib('string'),
    fax: DS.attrib('string')
});

/**

    Routes

**/

Northwind.Routes.Map(function () {
    this.resource('customers', { path: '/customers' });
    this.resource('customer', { path: '/customers/:id' }, function () {
        this.resource('orders', { path: '/orders' });
    });

    this.resource('orders', { path: '/orders' });
    this.resource('order', { path: '/orders/:id' });
});

// CustomersRoute
Northwind.CustomersRoute = Ember.Route.extend({
    model: function () {
        return Ember.$.getJSON('http://localhost:2828/customers');
    }
});

// CustomerRoute
Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        return Ember.$.getJSON('http://localhost:2828/customers/' + params.id);
    }
});

/**

    Controllers

**/



