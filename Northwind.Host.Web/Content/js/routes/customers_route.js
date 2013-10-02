/**
    CustomersRoute
**/
Northwind.CustomersRoute = Ember.Route.extend({
    /**
        model
    **/
    model: function () {

        //var queryParams = this.get('queryParams');
        var offset;
        var limit;

        var controller = this.controllerFor('customer');

        //if (queryParams) {
        if (controller.metadata) {
            limit = controller.metadata.limit;
            offset = controller.metadata.offset + limit;
        }

        return this.get('store').findQuery('customer', { offset: offset, limit: limit });

    },

    /**
        setupController
    **/
    //setupController: function (controller, model, queryParams) {
    setupController: function (controller, model) {

        var meta = this.get('store').metadataFor(model.type);

        controller.set('model', model);

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
            controller.set('offset', metadata.offset);
            controller.set('limit', metadata.limit);
            controller.set('totalCount', metadata.totalCount);
        }

    }

});