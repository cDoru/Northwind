/**
    CustomersRoute
**/
Northwind.CustomersRoute = Ember.Route.extend({
    /**
        model
    **/
    model: function () {

        var offset;
        var limit;

        var controller = this.controllerFor('customer');

        if (controller.metadata) {
            limit = controller.metadata.limit;
            offset = controller.metadata.offset + limit;
        }

        return this.get('store').findQuery('customer', { offset: offset, limit: limit });

    },

    /**
        setupController
    **/
    setupController: function (controller, model) {

        var meta = this.get('store').metadataFor(model.type);

        controller.set('model', model);

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

            controller.set('metadata', metadata);
            controller.set('offset', metadata.offset);
            controller.set('limit', metadata.limit);
            controller.set('totalCount', metadata.totalCount);
        }

    }

});