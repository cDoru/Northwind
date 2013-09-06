Northwind = Ember.Application.create();

/**

    Routes

**/

Northwind.Router.map(function () {
    this.resource('customers', function () {
        this.resource('customer', { path: ':customer_id' }); 
    });    
    this.resource('about');
});

Northwind.CustomersRoute = Ember.Route.extend({
    model: function () {
        return Ember.$.getJSON('http://localhost:2828/customers').property('results');
    }
});

Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        return Ember.$.getJSON('http://localhost:2828/customers/' + params.customer_id);
    }
});

/**

    Controller

**/

Northwind.CustomerController = Ember.ObjectController.extend({
    isEditing: false,

    actions: {
        edit: function () {
            this.set('isEditing', true);
        },

        doneEditing: function () {
            this.set('isEditing', false);
        }
    }
});

/**

    Helpers

**/

Ember.Handlebars.helper('format-date', function(date) {
    return moment(date).fromNow();
});

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