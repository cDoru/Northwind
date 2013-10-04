/**
    @class      CustomersController
    @namespace  Northwind
    @extends    Northwind.ArrayController
**/
Northwind.CustomersController = Northwind.Common.Components.Grid.GridController.extend({

	/**
		queryServer
	**/
	queryServer: function (offset, limit) {

		//this.set('content', this.get('store').findQuery('customer', { offset: offset, limit: limit }));
		var result = this.get('store').findQuery('customer', { offset: offset, limit: limit });		

		result.then(
			function(data) {
				this.set('content.[]', result);
			}
		);
	},

	/**
		getPaginationLink
	**/
	getPaginationLink: function (rel) {
		var metadata = this.get('metadata');		

		return metadata.links.findBy('rel', rel);		
	},

	/**
		nextPage
	**/
	nextPage: function () {
		var link = this.getPaginationLink('next');
		
		if (link) {			
			this.queryServer(link.get('offset'), link.get('limit'));
		}

		this._super();
	},

	/**
		didContentChange
	**/
	contentDidChange: function () {

		console.log('contentDidChange');

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
            this.set('offset', metadata.offset);
            this.set('limit', metadata.limit);
            this.set('totalCount', metadata.totalCount);
        }

	}.observes('content'),

    columns: [
		Northwind.Common.Components.Grid.column('id', { formatter: '{{#link-to \'customer\' view.content}}{{view.content.id}}{{/link-to}}' }),
		Northwind.Common.Components.Grid.column('contactName'),
		Northwind.Common.Components.Grid.column('companyName'),
		Northwind.Common.Components.Grid.column('contactTitle')
	]	

});