;Northwind = Ember.Application.create();

Northwind.store = DS.Store.extend();



;var get = Ember.get;
var set = Ember.set;
var forEach = Ember.EnumerableUtils.forEach;

/**
    
    @class Pagination
    @namespace Ember
    @extends Ember.Mixin

        Implementa paginación para ArrayController

**/

Ember.Pagination = Ember.Mixin.create({

    /**
        pages
    **/
    pages: function () {

        var pages = [];
        var page = 0;
        var totalPages = this.get('totalPages');

        for (i = 0; i < totalPages; i++) {
            page = i + 1;
            pages.push({ page_id: page.toString() });
        }

        return pages;

    }.property('totalPages'),


    /**
        currentPage
    **/
    currentPage: function () {

        return parseInt(this.get('selectedPage'), 10) || 1;

    }.property('selectedPage'),

    /**
        nextPage
    **/
    nextPage: function () {

        var nextPage = this.get('currentPage') + 1;
        var totalPages = this.get('totalPages');

        if (nextPage <= totalPages) {
            return Ember.Object.create({ id: nextPage });
        } else {
            return Ember.Object.create({ id: this.get('currentPage') });
        }

    }.property('currentPage', 'totalPages'),

    /**
        previousPage
    **/
    previousPage: function () {

        var prevPage = this.get('currentPage') - 1;

        if (prevPage > 0) {
            return Ember.Object.create({ id: prevPage });
        } else {
            return Ember.Object.create({ id: this.get('currentPage') });
        }

    }.property('currentPage'),

    /**
        totalPages
    **/
    totalPages: function () {

        return Math.ceil((this.get('content.length') / this.get('limit')) || 1);

    } .property('content.length'),

    /**
        paginatedContent
    **/
    paginatedContent: function () {

        var selectedPage = this.get('selectedPage') || 1;
        var upperBound = (selectedPage * this.get('limit'));
        var lowerBound = (selectedPage * this.get('limit') - this.get('limit'));
        var models = this.get('content');

        return models.slice(lowerBound, upperBound);

    }.property('selectedPage', 'content.@each')

});
;// http://stackoverflow.com/questions/16037175/ember-data-serializer-data-mapping/16042261#16042261
Northwind.ApplicationAdapter = DS.RESTAdapter.extend({
    host: 'http://localhost:2828',
    serializer: Northwind.ApplicationSerializer
});
;/**

Serializador personalizado

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

Ember Data espera un JSON cuyo elemento raíz sea el nombre del modelo en plural. 
Por ejemplo, para el caso anterior, Ember Data espera esto: 

{       
    customers:[
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
    ]       
}

**/

Northwind.ApplicationSerializer = DS.RESTSerializer.extend({
    // Reestructuramos el nivel superior para organizarlo de la manera que espera Ember. 
    // Crearemos un nuevo objeto payload cuyo nivel superior sea el nombre del modelo "primaryType" en plural
    extractArray: function (store, primaryType, payload) {

        var result = payload.result;
        var metadata = payload.metadata;

        // Obtenemos el nombre que tiene que tener el elemento raíz
        var root = Ember.String.pluralize(primaryType.typeKey);

        // Creamos un nuevo objeto como lo quiere Ember Data
        var newPayload = {};

        newPayload[root] = result;

        return this._super(store, primaryType, newPayload);

    },

    // Hacemos lo mismo que en extractArray
    extractSingle: function (store, primaryType, payload, recordId, requestType) {

        var typeName = primaryType.typeName;
        var data = {}
        data[typeName] = payload.result;

        return this._super(store, primaryType, data, recordId, requestType);

    },

    // Extracción de los metadatos de la respuesta
    extractMeta: function (store, type, payload) {

        if (payload && payload.metadata) {
            store.metaForType(type, payload.metadata);
        }

        this._super(store, type, payload);

    }

});

;/**
**/
Northwind.Router.map(function () {
    this.resource('customers', function () {
        this.resource('customer', { path: ':customer_id' });
    });
    this.resource('about');
});

/**
**/
Northwind.Router.reopen({
    location: 'history'
});
;Northwind.CustomersRoute = Ember.Route.extend({
    model: function () {
        return this.store.find('customer');
    }    
});
;Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        return this.store.find('customer', params.customer_id);
    }
});
;/**
    @class  ArrayController
**/
Northwind.ArrayController = Ember.ArrayController.extend(Ember.Pagination, {
    offset: 1,
    limit: 10,

    metadata: function () {

        if (this.get('model.isLoaded')) {
            var modelType = this.get('model.type');
            //var meta = this.get('store').typeMapFor(modelType).metadata;           
            var meta = this.get('store').metadataFor(modelType);

            for (var prop in meta) {
                console.log(prop + ': ' + meta[prop]);
            }

            return meta;
        }

    }.property('model.isLoaded')
});
;Northwind.CustomerController = Ember.ObjectController.extend({
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
;/**
**/
Northwind.CustomersController = Northwind.ArrayController.extend({
    limit: 20
});
;/**
    @class PagerView
**/

Northwind.PagerView = Ember.View.extend({
    templateName: 'pager',
    tagName: 'ul',
    classNames: ['pager'],

    /**
        self
    **/
    self: function () {

        return this.get('controller.metadata.links.self');

    }.property(),

    /**
        previous
    **/
    previous: function () {

        return this.get('controller.metadata.links.previous');

    }.property(),

    /**
        next
    **/
    next: function () {        

        return this.get('controller.metadata.links.next');

    }.property(),

    /**
        first
    **/
    first: function () {

        return this.get('controller.metadata.links.first');

    }.property(),

    /**
        last
    **/
    last: function () {

        return this.get('controller.metadata.links.last');

    }.property()
});
;/**
	@class		Customer
	@extends	Em.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Customer = DS.Model.extend({
	//id: DS.attr('string'),
	companyName: DS.attr('string'),
	contactName: DS.attr('string'),
	contactTitle: DS.attr('string'),
	address: DS.attr('string'),
	city: DS.attr('string'),
	region: DS.attr('string'),
	postalCode: DS.attr('string'),
	country: DS.attr('string'),
	phone: DS.attr('string'),
	fax: DS.attr('string'),
	    
    orders: DS.hasMany('order')
});

;/**
	@class		Order
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Order = DS.Model.extend({
//	id: DS.attr('long'),
	employeeId: DS.attr('long'),
	orderDate: DS.attr('string'),
	requiredDate: DS.attr('string'),
	shippedDate: DS.attr('string'),
	freight: DS.attr('decimal'),
	shipName: DS.attr('string'),
	shipAddress: DS.attr('string'),
	shipCity: DS.attr('string'),
	shipRegion: DS.attr('string'),
	shipPostalCode: DS.attr('string'),
	shipCountry: DS.attr('string'),
	
    customer: DS.belongsTo('customer'),

    details: DS.hasMany('orderdetails')
});

;/**
	@class		OrderDetail
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.OrderDetail = DS.Model.extend({
//	id: DS.attr('string'),
	productId: DS.attr('long'),
	unitPrice: DS.attr('decimal'),
	quantity: DS.attr('long'),
	discount: DS.attr('double'),

    order: DS.belongsTo('order')
	
});

;/**
	@class		Supplier
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Supplier = DS.Model.extend({
//	id: DS.attr('long'),
	companyName: DS.attr('string'),
	contactName: DS.attr('string'),
	contactTitle: DS.attr('string'),
	address: DS.attr('string'),
	city: DS.attr('string'),
	region: DS.attr('string'),
	postalCode: DS.attr('string'),
	country: DS.attr('string'),
	phone: DS.attr('string'),
	fax: DS.attr('string'),
	homePage: DS.attr('string')
	
});

