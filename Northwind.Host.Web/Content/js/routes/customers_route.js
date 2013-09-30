/**
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

        //return this.get('store').find('customer');
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