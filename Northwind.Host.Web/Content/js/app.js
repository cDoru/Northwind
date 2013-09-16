﻿Northwind = Ember.Application.create();

// http://stackoverflow.com/questions/16037175/ember-data-serializer-data-mapping/16042261#16042261
Northwind.ApplicationAdapter = DS.RESTAdapter.extend({
    host: 'http://localhost:2828',
    defaultSerializer: 'Northwind.ApplicationSerializer'    
});

/**

    Custom serializer. 
    El JSON de Northwind no sigue el estándar de Ember, así que tenemos que personalizarlo
    para que coja el elemento raíz correcto.

    El JSON de Northwind tiene un aspecto como este:     

    {
       count:2,
       result:[
          {
             id:"ALFKI",
             companyName:"Alfreds Futterkiste",
             contactName:"Maria Anders",
             contactTitle:"Sales Representative",
             address:"Obere Str. 57",
             city:"Berlin",
             postalCode:"12209",
             country:"Germany",
             phone:"030-0074321",
             fax:"030-0076545",
             link:"http://localhost:2828/customers/ALFKI"
          },
          {
             id:"ANATR",
             companyName:"Ana Trujillo Emparedados y helados",
             contactName:"Ana Trujillo",
             contactTitle:"Owner",
             address:"Avda. de la Constitución 2222",
             city:"México D.F.",
             postalCode:"05021",
             country:"Mexico",
             phone:"(5) 555-4729",
             fax:"(5) 555-3745",
             link:"http://localhost:2828/customers/ANATR"
          }
       ],
       metadata:{
          offset:1,
          limit:10,
          totalCount:92,
          links:{
             self:"http://localhost:2828/customers?format=json&offset=1&limit=10",
             last:"http://localhost:2828/customers?format=json&offset=82&limit=10",
             next:"http://localhost:2828/customers?format=json&offset=11&limit=10"
          }
       }
    }

**/

Northwind.ApplicationSerializer = DS.RESTSerializer.extend({
    // Reestructuramos el nivel superior para organizarlo de la manera que espera Ember    
    extractArray: function (store, primaryType, payload) {
        var result = payload.result;
        var metadata = payload.metadata;
        var root = Ember.String.pluralize(primaryType.typeKey);

        var newPayload = { };

        newPayload[root] = result;

        return this._super(store, primaryType, newPayload);
    }

});


Northwind.store = DS.Store.extend({
});

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
        //return $.getJSON('http://localhost:2828/customers').then(function(data) {
        //	return data.result;        
        //});        
        return this.store.find('customer');
    }
});

Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        //return Ember.$.getJSON('http://localhost:2828/customers/' + params.customer_id).then(function(data) {
        //    return data.result;
        //});
        return this.store.find('customer', params.customer_id);
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

// Customer
Northwind.Customer = DS.Model.extend({
//    id: DS.attr('string'),
    companyName: DS.attr('string'),
    contactName: DS.attr('string'),
    contactTitle: DS.attr('string'),
    address: DS.attr('string'),
    city: DS.attr('string'),
    region: DS.attr('string'),
    postalCode: DS.attr('string'),
    country: DS.attr('string'),
    phone: DS.attr('string'),
    fax: DS.attr('string')
});
