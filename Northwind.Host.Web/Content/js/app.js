;Ember.FEATURES["query-params"] = true;

/**
**/
Northwind = Ember.Application.create();

/**
**/
Northwind.store = DS.Store.extend();



;/**
    @extends	Ember.Namespace
    @namespace	Northwind
    @module		@Northwind
**/

Northwind.Common = Ember.Namespace.create({
    Utils: Ember.Namespace.create()
});

/**
    @class      UriUtis
    @extends	Ember.Namespace
    @namespace	Northwind.Common.Utils
    @module		Northwind
**/
Northwind.Common.Utils.UriUtils = Ember.Object.extend({

    /**
    Extrae los argumentos de una Url en forma de objeto

    @see https://gist.github.com/simonsmith/5152680
    **/
    parseQueryParams: function (uri) {
        var queryParams = {};

        if (uri) {
            var reg = /\\?([^?=&]+)(=([^&#]*))?/g;

            uri.replace(reg, function ($0, $1, $2, $3) {
                if (typeof $3 == 'string') {
                    queryParams[$1] = decodeURIComponent($3);
                }
            });
        }

        return queryParams;

    }
});

Northwind.uriUtils = Northwind.Common.Utils.UriUtils.create();

/**
**/
Northwind.Common.PaginationMetadata = Ember.Object.extend({
    rel: null,
    offset: 0,
    limit: 0    
});

;/**
    @class      ArrayController
    @namespace  Northwind
    @extends    Ember.ArrayController
**/
Northwind.ArrayController = Ember.ArrayController.extend({
    /**
        offset
    **/
    offset: 0,

    /**
        limit
    **/
    limit: 0,    

    /**
    metadata
    **/
    metadata: null

});
;/**
**/
Northwind.Common.Components = Ember.Namespace.create({
    Grid: Ember.Namespace.create()
});
;/**
	PaginationMixin
 **/
Northwind.Common.Components.Grid.PaginationMixin = Ember.Mixin.create({
    /**
        totalCount
    **/
    totalCount: 0,

    /**
        offset
    **/
    offset: 0,

    /**
        limit
    **/
    limit: 10,

    /**
        limit
    **/
    page: 0,

    /**
        metadata
    **/
    metadata: null,

    /**
        paginableContentBinding
    **/
    paginableContentBinding: 'content',


    /**
        paginatedContent
    **/
    paginatedContent: Ember.computed(function () {

        if (this.get('page') >= this.get('pages')) {
            this.set('page', 0);
        }

        //return this.get('paginableContent').slice(this.get('offset'), this.get('offset') + this.get('limit'));
        return this.get('paginableContent');

    }).property('@each', 'page', 'limit', 'paginableContent'),


    /**
        pages
    **/
    pages: Ember.computed(function () {        

        return Math.ceil(this.get('totalCount') / this.get('limit'));

    }).property('totalCount', 'limit'),

    /**
        firstPage
    **/
    firstPage: function () {

        this.set('page', 0);

    },

    /**
        previousPage
    **/
    previousPage: function () {

        this.set('page', Math.max(this.get('page') - 1, 0));

    },

    /**
        nextPage
    **/
    nextPage: function () {

        this.set('page', Math.min(this.get('page') + 1, this.get('pages') - 1));

    },

    /**
        lastPage
    **/
    lastPage: function () {

        this.set('page', this.get('pages') - 1);

    }

});
;/**
    `TableView` 

    @class 		Column
    @namespace 	Northwind.Common.Components.Grid
    @extends 	Ember.Object

*/

Northwind.Common.Components.Grid.Column = Ember.Object.extend({

    property: null,

    display: true,

    formatter: '{{view.content.%@}}',

    /**
        header
    **/
    header: function () {

        if (!this.get('property')) return '';

        return this.get('property').capitalize();

    } .property('property'),

    /**
        visible
    **/
    visible: function () {

        return this.get('display') != false;

    }.property('display'),

    /**
        always
    **/
    always: function () {

        return this.get('display') === 'always';

    }.property('display'),    

    /**
        viewClass
    **/
    viewClass: function () {
        var formatter = this.get('formatter');

        if (Northwind.Common.Components.Grid.CellView.detect(formatter)) {
            return formatter;
        } else {
            Ember.assert('Formatter has to extend CellView or Handlebar template', Ember.typeOf(formatter) === 'string');

            var property = this.get('property');

            if (!property) {
                property = 'constructor';
            }

            var template = this.get('formatter').fmt(property);

            return Northwind.Common.Components.Grid.CellView.extend({
                template: Ember.Handlebars.compile(template)
            });
        };
    }.property()

});

/**
    `TableView` 

    @class      Column
    @namespace  Northwind.Common.Components.Grid
    @extends    Ember.Object

*/

Northwind.Common.Components.Grid.column = function (property, options) {

    if (Ember.typeOf(property) === 'object') {
        options = property;
        property = null;
    }

    var column = Northwind.Common.Components.Grid.Column.create({
        property: property
    });

    if (options) {
        for (var key in options) {
            column.set(key, options[key]);
        };
    }

    return column;
};
;/**
	`GridController` ofrece una manera de listar elementos de una colección de 
  	objetos con la posibilidad de mostrar los datos mediante lista paginada

	@class 		GridController
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ArrayController
	@uses		Northwind.Common.Components.Grid.PaginationMixin		
 */

Northwind.Common.Components.Grid.GridController = Ember.ArrayController.extend(Northwind.Common.Components.Grid.PaginationMixin, {

    columns: [],

    paginableContentBinding: 'content',

    rowsBinding: 'paginatedContent',

    visibleColumns: function () {

        return this.get('columns').filterProperty('visible', true);

    }.property('columns.@each.visible')

});
;/**
	`TableView` 

	@class 		GridView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.GridView = Ember.View.extend({

    classNames: ['grid'],

    defaultTemplate: Ember.Handlebars.compile('{{view Northwind.Common.Components.Grid.TableView}}')

});
;/**
	`PageListView` 

	@class 		PageListView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ContainerView

 */

Northwind.Common.Components.Grid.PageListView = Ember.ContainerView.extend({

	tagName: 'ul',

	pages: [],

	visiblePages: 3, 

	/**
		firstPageView
	 **/
	firstPageView: Ember.View.extend({

		tagName: 'li',
		classNames: ['parent.hasFirstPage::disabled'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action firstPage target="view.parentView"}}>&laquo;</a>')

	 }),

	/**
		prevPageView
	 **/
	prevPageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['parent.hasPreviousPage::disabled'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action prevPage target="view.parentView"}}>&lsquo;</a>')
	}),

	/**
		pageView
	 **/
	pageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['content.isActive:active'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action setPage view.content target="view.parentView"}}>{{view.content.page}}</a>')
	}),

	/**
		nextPageView
	**/
	nextPageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['parentView.hasNextPage::disabled'],
		templage: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action nextPage target="view.parentView"}}>&rsquo;</a>')
	}),

	/**
		nextPageView
	**/
	lastPageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['parentView.hasLastPage::disabled'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action lastPage target="view.parentView"}}>&raquo;</a>')
	}),

	/**
		refreshPageListItems
	**/
	refreshPageListItems: function () {
		var pages =  this.get('pages');
		if (!pages.get('length')) return;

		this.clear();
		this.pushObject(this.get('firstPageView').create());
		this.pushObject(this.get('prevPageView').create());

		var self = this;

		this.get('pages').forEach(function (page) {
			var pageView = self.get('pageView').create({
				content: page
			});
		});

		this.pushObject(this.get('nextPageView').create());
		this.pushObject(this.get('lastPageView').create());
	}.observes('pages'),

	/**
		createPages
	**/
	createPages: function () {
		if (!this.get('controller')) return [];

		var page = this.get('controller.page');
		var pages = this.get('controller.pages');
		var pagesFrom = Math.max(0, page - this.visiblePages);
		var pagesTo = Math.min(pages, page + this.visiblePages + 1);
		var limit = this.get('controller.limit');

		var pages = [];

		for (var i = pagesFrom; i < pagesTo; i++) {
			pages.push({
				index: i, 
				page: i + 1,
				isActive: (i == page)
			});
		}

		this.set('pages', pages);
	},

	/**
		didControllerContentChanged
	**/
	didControllerContentChanged: function () {
		this.createPages();

		var pages = this.get('controller.pages');
		var page = this.get('controller.page');

		this.set('pagesCount', pages);
		this.set('hasNextPage', page + 1 < pages);
		this.set('hasPrevPage', page > 0);
		this.set('hasFirstPage', page > 0);
		this.set('hasLastPage', page + 1 < pages);
	}.observes('controller', 'controller.pages', 'controller.page'),

	/**
		setPage
	**/
	setPage: function () {
		this.get('controller').set('page', context.index);
	},

	/**
		firstPage
	**/		
	firstPage: function () {
		if (!this.get('hasFirstPage')) return;

		this.get('controller').firstPage();
	},

	/**
		lastPage
	**/
	lastPage: function () {
		if (!this.get('hasLastPage')) return;

		this.get('controller').lastPage();
	},

	/**
		lastPage
	**/
	prevPage: function () {
		if (!this.get('hasPrevPage')) return;

		this.get('controller').previousPage();
	},

	/**
		nextPage
	**/
	nextPage: function () {
		if (!this.get('hasNextPage')) return;

		this.get('controller').nextPage();
	},

	/**
		init
	**/
	init: function () {
		this._super();
		this.refreshPageListItems();
	}

 });
;/**
	`PageView` 

	@class 		PageView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.PageView = Ember.View.extend({

    classNames: ['pull-left', 'table-page'],

    first: 0,

    last: 0,

    defaultTemplate: Ember.Handlebars.compile('Showing {{view.first}} - {{view.last}} from {{controller.totalCount}}'),

    /**
    didPageChange
    **/
    didPageChange: function () {

        console.log('PageView.didPageChange');

        var limit = this.get('controller.limit');
        var length = this.get('controller.totalCount');
        var offset = this.get('controller.offset');

        this.set('first', offset);
        this.set('last', Math.min(length, offset + limit));

    }.observes('controller.page', 'controller.totalCount')

});
;/**
	`PaginationView` 

	@class 		PageListView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ContainerView

 */

Northwind.Common.Components.Grid.PaginationView = Ember.ContainerView.extend({

	tagName: 'div',

	classNames: ['pagination', 'pagination-small', 'pagination-right', 'table-pagination'],

	childViews: ['pageList'],

	/**
		pageList
	**/
	pageList: Northwind.Common.Components.Grid.PageListView.create()

});
;/**
	`TableView` 

	@class 		TableView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.TableView = Ember.View.extend({

    tagName: 'table',

    classNames: ['table', 'table-striped', 'table-condensed'],

    defaultTemplate: function () {        

        var headerView = '<thead>{{view Northwind.Common.Components.Grid.HeaderView}}</thead>';
        var bodyView = '{{view Northwind.Common.Components.Grid.BodyView}}';
        var footerView = '{{view Northwind.Common.Components.Grid.FooterView}}';

        return Ember.Handlebars.compile(headerView + bodyView + footerView);

    }.property()

});

;/**
	`HeaderView` 

	@class 		HeaderView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.HeaderView = Ember.CollectionView.extend({

 	tagName: 'tr',

    contentBinding: 'controller.visibleColumns',

 	itemViewClass: Ember.View.extend({
 	    tagName: 'th',
        classNames: ['table-header-cell'],
 		template: Ember.Handlebars.compile('{{view.content.header}}')        
 	})

 });
;/**
	`BodyView` 

	@class 		BodyView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.BodyView = Ember.CollectionView.extend({

 	tagName: 'tbody',

 	contentBinding: 'controller.rows',

 	classNames: ['table-body'],

 	itemViewClass: 'Northwind.Common.Components.Grid.RowView',

 	/**
        emptyView
 	**/
 	emptyView: Ember.View.extend({
 		tagName: 'tr',
 		template: Ember.Handlebars.compile('<td {{bindAttr colspan="controller.columns.length"}} class="muted">No hay elementos</td>')
 	})

 });
;/**
	`RowView` 

	@class 		RowView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.RowView = Ember.ContainerView.extend({

    tagName: 'tr',

    classNames: ['table-row'],

    rowBinding: 'content',

    columnsBinding: 'controller.visibleColumns',

    /**
        columnsDidChange
    **/
    columnsDidChange: function () {

        if (this.get('columns')) {
            this.clear();
            this.get('columns').forEach(function (column) {
                var cell = column.get('viewClass').create({
                    column: column,
                    content: this.get('row')
                });

                this.pushObject(cell);

            }, this);
        }

    }.observes('columns.@each'),

    /**
    init
    **/
    init: function () {
        this._super();
        this.columnsDidChange();
    }

});
;/**
	`TableView` 

	@class 		CellView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.CellView = Ember.View.extend({

 	tagName: 'td'

 });
;/**
	`FooterView` 

	@class 		FooterView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.FooterView = Ember.ContainerView.extend({

    tagName: 'tfoot',

    classNames: ['table-footer'],

    childViews: ['gridFooter'],

    gridFooter: Ember.View.create({

        tagName: 'tr',

        template: Ember.Handlebars.compile(
            '<td {{bindAttr colspan="controller.columns.length"}}>' + 
                '{{view Northwind.Common.Components.Grid.PageView}}' + 
                '{{view Northwind.Common.Components.Grid.PaginationView}}' + 
            '</td>'
        )

    })

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
    this.resource('customers', { queryParams: ['offset', 'limit'] }, function () {
        this.resource('customer', { path: ':customer_id' });
    });
    this.resource('about');
});

/**
**/
Northwind.Router.reopen({
    location: 'history'
});
;/**
    CustomersRoute
**/
Northwind.CustomersRoute = Ember.Route.extend({
    /**
        model
    **/
    model: function () {

        var queryParams = this.get('queryParams');
        var offset;
        var limit;

        if (queryParams) {
            limit = queryParams.limit;
            offset = queryParams.offset + limit;
        }

        return this.get('store').findQuery('customer', { offset: offset, limit: limit });

    },

    /**
        setupController
    **/
    setupController: function (controller, model, queryParams) {

        controller.set('model', model);
        controller.set('offset', queryParams.offset);
        controller.set('limit', queryParams.limit);

        var meta = this.get('store').metadataFor(model.type);

        if (meta) {
            var metadata = Ember.Object.create({
                offset: meta.offset,
                limit: meta.limit,
                totalCount: meta.totalCount,
                links: Ember.makeArray([
                        Northwind.Common.PaginationMetadata.create({ rel: "previous", href: Northwind.uriUtils.parseQueryParams(meta.links.previous) }),
                        Northwind.Common.PaginationMetadata.create({ rel: "next", href: Northwind.uriUtils.parseQueryParams(meta.links.next) }),
                        Northwind.Common.PaginationMetadata.create({ rel: "fist", href: Northwind.uriUtils.parseQueryParams(meta.links.first) }),
                        Northwind.Common.PaginationMetadata.create({ rel: "last", href: Northwind.uriUtils.parseQueryParams(meta.links.last) })
                ])
            });

            controller.set('metadata', metadata);
            controller.set('totalCount', metadata.totalCount);
        }

    }

});
;Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        return this.get('store').find('customer', params.customer_id);
    }
});
;/**
    `CustomerController` 

    @class 		CustomerController
    @namespace 	Northwind
    @extends 	Ember.Object

*/
Northwind.CustomerController = Ember.ObjectController.extend({

});
;/**
    @class      CustomersController
    @namespace  Northwind
    @extends    Northwind.ArrayController
**/
Northwind.CustomersController = Northwind.Common.Components.Grid.GridController.extend({

    columns: [
		Northwind.Common.Components.Grid.column('id', { formatter: '{{#link-to \'customer\' view.content}}{{view.content.id}}{{/link-to}}' }),
		Northwind.Common.Components.Grid.column('contactName'),
		Northwind.Common.Components.Grid.column('companyName'),
		Northwind.Common.Components.Grid.column('contactTitle')
	]

});
;/**
    @class PagerView
**/

Northwind.PagerView = Ember.View.extend({
    templateName: 'pager',
    tagName: 'ul',
    classNames: ['pager']   
});
;/**
    `CustomersView` 

    @class 		CustomersView
    @namespace 	Northwind
    @extends 	Northwind.Common.Components.Grid.GridView

*/
/*
Northwind.CustomersView = Northwind.Common.Components.Grid.GridView.extend({
    templateName: 'customers'
});
*/
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

