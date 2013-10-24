;/**
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
    
});


;/**
	Funciones relacionadas con Url

	@class		Uri
	@namespace	Northwind
	@module		Northwind
	@extends	Ember.Object
**/
Northwind.Common.Uri = Ember.Object.create({
	
	/**
		Extrae los argumentos de una Url en forma de objeto

		@method	queryParams
		@param  {String} Uri de donde se extraerán sus parámetros
		@see 	https://gist.github.com/simonsmith/5152680
	**/
	queryParams: function (uri) {

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
;/**
**/
Northwind.Common.Components = Ember.Namespace.create({
    Grid: Ember.Namespace.create()
});
;/**
	PaginationMixin
 **/
Northwind.Common.Components.Grid.Pagination = Ember.Mixin.create({
    /**
        totalCount
    **/
    totalCount: 0,    

    /**
        limit
    **/
    limit: 0,

    /**
        page
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
        offset
    **/
    offset: function () {

        var page = this.get('page');
        var limit = this.get('limit');

        return (page * limit) + 1;

    }.property('page'),


    /**
        paginatedContent
    **/
    paginatedContent: function () {

        if (this.get('page') >= this.get('pages')) {
            this.set('page', 0);
        }
        
        return this.get('content');

    }.property('@each', 'page', 'limit'),


    /**
        pages
    **/
    pages: function () {        

        return Math.ceil(this.get('totalCount') / this.get('limit'));

    }.property('totalCount', 'limit'),

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

Northwind.Common.Components.Grid.GridController = Ember.ArrayController.extend(Northwind.Common.Components.Grid.Pagination, {

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

    classNames: ['pagination', 'pagination-sm'],

    pages: [],

    visiblePages: 3,

    /**
        firstPageView
    **/
    firstPageView: Ember.View.extend({
        tagName: 'li',
        classNameBindings: ['parentView.hasFirstPage::disabled'],
        template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action firstPage target="view.parentView"}}>&laquo;</a>')

    }),

    /**
        prevPageView
    **/
    prevPageView: Ember.View.extend({
        tagName: 'li',
        classNameBindings: ['parentView.hasPreviousPage::disabled'],
        template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action prevPage target="view.parentView"}}>&lsaquo;</a>')
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
        template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action nextPage target="view.parentView"}}>&rsaquo;</a>')
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

        var pages = this.get('pages');

        if (!pages.get('length')) return;

        this.clear();
        this.pushObject(this.get('firstPageView').create());
        this.pushObject(this.get('prevPageView').create());

        var self = this;


        this.get('pages').forEach(function (page) {            
            var pageView = self.get('pageView').create({
                content: page
            });

            self.pushObject(pageView);
        });

        this.pushObject(this.get('nextPageView').create());
        this.pushObject(this.get('lastPageView').create());
        
    }.observes('pages'),

    /**
        createPages
    **/
    createPages: function () {

        if (!this.get('controller')) return [];

        var currentPage = this.get('controller.page');
        var pages = this.get('controller.pages');
        var pagesFrom = Math.max(0, currentPage - this.visiblePages);
        var pagesTo = Math.min(pages, currentPage + this.visiblePages + 1);
        var limit = this.get('controller.limit');
        
        var pages = [];

        for (var i = pagesFrom; i < pagesTo; i++) {
            pages.push({
                index: i,
                page: i + 1,
                isActive: (i == currentPage)
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
        this.set('hasPreviousPage', page > 0);
        this.set('hasFirstPage', page > 0);
        this.set('hasLastPage', page + 1 < pages);

    }.observes('controller.offset', 'controller.pages', 'controller.page').on('init'),

    /**
        actions
    **/
    actions: {
        /**
            setPage
        **/
        setPage: function (context) {

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

            if (!this.get('hasPreviousPage')) return;

            this.get('controller').previousPage();

        },

        /**
            nextPage
        **/
        nextPage: function () {

            if (!this.get('hasNextPage')) return;

            this.get('controller').nextPage();

        }
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

    defaultTemplate: Ember.Handlebars.compile('Showing {{controller.offset}} - {{view.last}} from {{controller.totalCount}}'),

    /**
        didPageChange
    **/
    didPageChange: function () {        

        var limit = this.get('controller.limit');
        var length = this.get('controller.totalCount');
        var offset = this.get('controller.offset');        

        this.set('first', offset);
        this.set('last', Math.min(length, (offset - 1) + limit));

    }.observes('controller.offset', 'controller.limit', 'controller.totalCount').on('init')

});
;/**
	`PaginationView` 

	@class 		PageListView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ContainerView

 */

Northwind.Common.Components.Grid.PaginationView = Ember.ContainerView.extend({

	tagName: 'div',

	classNames: ['pull-right', 'table-pagination'],

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

    classNames: ['table-bordered', 'table-striped', 'table-condensed'],

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

    classNames: ['table-footer', 'text-muted'],

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
        
        delete payload.count;
        
        this.extractMeta(store, primaryType, payload);

        return this._super(store, primaryType, payload);

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
            delete payload.metadata;
        }

    }

});


;/**
**/
Northwind.Router.map(function () {
    this.resource('customers', function () {
        this.resource('customer', { path: ':customer_id' }, function () {
        	this.route('orders');
        });
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

        var controller = this.controllerFor('customer');        

        return this.get('store').findQuery('customer', { offset: controller.offset, limit: controller.limit });

    },   

    /**
        setupController
    **/
    setupController: function (controller, model) {
        controller.set('content', model);
        controller.set('contentLoaded', true);
    }

});
;Northwind.CustomerRoute = Ember.Route.extend({
    model: function (params) {
        return this.get('store').find('customer', params.customer_id);
    }
});
;/**
    `ObjectController` 

    @class 		ObjectController
    @namespace 	Northwind
    @extends 	Ember.ObjectController

*/
Northwind.ObjectController = Ember.ObjectController.extend({

	/**
		Indica si se está editando un registro

		@property	isEditing
		@type		{Boolean}

	**/
	isEditing: false,

	actions: {

		/**
			Modificación de un registro

			@action 	edit
		**/
		edit: function () {

			this.set('isEditing', true);

		},

		/**
			Se guardan los datos del registro

			@action 	acceptChanges
		**/
		acceptChanges: function () {

			this.set('isEditing', false);			

			// Comprobamos que los campos obligatorios tienen datos
			if (this.get('model.isSaveable'))
			{
				console.log('isSaveable');
				//this.send('save');
			} else {
				console.log('NOT isSaveable');
				//this.send('remove');
			}

		},

		/**
			Se guardan los datos del registro en la base de datos

			@action 	save
		**/
		save: function () {

			this.get('model').save();

		},

		/**
			Se elimina el registro

			@action 	remove
		**/
		remove: function () {

			this.get('model').deleteRecord();
			this.send('save');

		}

	}

});
;/**
    @class      ArrayController
    @namespace  Northwind
    @extends    Northwind.Common.Components.Grid.GridController
**/
Northwind.ArrayController = Northwind.Common.Components.Grid.GridController.extend({
    
    contentLoaded: false,

    modelType: Ember.computed(function () {

        var model = this.get('model');

        return model.type;

    }),
    
    /**
        @method loadMetadata

        Carga los metadatos.

        Este método se ejecuta una vez que todos los datos se han cargado.
    **/
    loadMetadata: function () { 

        if (!this.get('contentLoaded')) return;

        var model = this.get('model');
        var meta = this.get('store').metadataFor(model.type);

        if (meta) {
            // Creamos el objeto de metadatos
            var metadata = Ember.Object.create({
                offset: meta.offset,
                limit: meta.limit,
                totalCount: meta.totalCount,
                links: Ember.makeArray()
            });

            // Se añaden los enlaces de paginación
            if (meta.links) {
                for (var link in meta.links) {
                    var lnkObj = Ember.Object.create(Northwind.Common.Uri.queryParams(meta.links[link]));
                    lnkObj.set('rel', link);
                    metadata.links.pushObject(lnkObj);
                }
            }

            this.set('metadata', metadata);
            this.set('limit', metadata.limit);
            this.set('totalCount', metadata.totalCount);
        }

    }.observes('contentLoaded'),

    /**
        @method refresh

        Actualiza la información del controlador a partir de los datos de paginación actuales.

        Este método se dispara cuando se cambia de página de resultados
    **/
    refresh: function () {

        var offset = this.get('offset');
        var limit = this.get('limit');
        var modelType = this.get('modelType');
        var self = this;

        self.set('contentLoaded', false);

        this.get('store').findQuery(modelType, { offset: offset, limit: limit }).then(function (result) {          
            self.set('model', result);
            self.set('contentLoaded', true);
        });

    }.observes('page')

});
;/**
    `CustomerController` 

    @class 		CustomerController
    @namespace 	Northwind
    @extends 	Northwind.ObjectController

*/
Northwind.CustomerController = Northwind.ObjectController.extend({
		

});
;/**
    @class      CustomersController
    @namespace  Northwind
    @extends    Northwind.ArrayController
**/
Northwind.CustomersController = Northwind.ArrayController.extend({		

	itemController: 'customer',

	/**
		columns
	**/
    columns: [
		Northwind.Common.Components.Grid.column('id', { formatter: '{{#link-to \'customer\' view.content}}{{view.content.id}}{{/link-to}}' }),
		Northwind.Common.Components.Grid.column('contactName'),
		Northwind.Common.Components.Grid.column('companyName'),
		Northwind.Common.Components.Grid.column('contactTitle')
	]	

});
;/**
    `TextEditView` 

    @class 		TextEditView
    @namespace 	Northwind
    @extends 	Ember.TextField

*/
Northwind.TextEditView = Ember.TextField.extend({

	didInsertElement: function () {

		this.$().focus();
		
	}

});

Ember.Handlebars.helper('text-edit', Northwind.TextEditView);

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
	Clase base para todos los modelos definidos en Northwind

	@class		Model
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Model = DS.Model.extend({

	required: [],

	/**
		Comprueba si todas las propiedades obligatorias del modelo tienen valor

		@property	isSaveable
		@type		{Boolean}
	**/
	isSaveable: function () {

		var required = this.get('required');
		var self = this;
		
		required.forEach(function (value) {			
			if(!self.get(value)) {
				return false;
			}
		});

		return true;

	}.property('required')

});

;/**
	Modelo que representa un Customer

	@class		Customer
	@extends	Northwind.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Customer = Northwind.Model.extend({
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

Northwind.Customer.reopen({

	required: ['contactName', 'companyName']

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

