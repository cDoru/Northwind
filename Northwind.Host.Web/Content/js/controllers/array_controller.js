/**
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