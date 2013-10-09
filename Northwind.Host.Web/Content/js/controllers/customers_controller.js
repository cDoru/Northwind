/**
    @class      CustomersController
    @namespace  Northwind
    @extends    Northwind.ArrayController
**/
Northwind.CustomersController = Northwind.Common.Components.Grid.GridController.extend({		

	contentLoaded: false,
	
	/**
		contentDidChange
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
                    var lnkObj = Ember.Object.create(Northwind.uriUtils.parseQueryParams(meta.links[link]));
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
		refresh
	**/
	refresh: function () {

		var offset = this.get('offset');
		var limit = this.get('limit');
		var self = this;

		self.set('contentLoaded', false);

		this.get('store').findQuery('customer', { offset: offset, limit: limit }).then(function (result) {			
			self.set('content', result);
			self.set('contentLoaded', true);
		});

	}.observes('page'),
	
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