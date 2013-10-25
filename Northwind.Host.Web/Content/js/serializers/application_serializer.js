/**
    `ApplicationSerializer` 

    @class      ApplicationSerializer
    @namespace  Northwind
    @extends    DS.RESTSerializer

*/

Northwind.ApplicationSerializer = DS.RESTSerializer.extend({

    /**
        `extractArray`

        Eliminamos del JSON aquellos elementos que no son compatibles con un JSON de Ember.
        En este caso, `count` no es necesario ya que tenemos el mismo valor en los metadatos
    **/
    extractArray: function (store, primaryType, payload) {
        
        delete payload.count;
        
        this.extractMeta(store, primaryType, payload);

        return this._super(store, primaryType, payload);

    },    

    /**
        `extractMeta`

        Extraemos los metadatos. Ember busca los metadatos en un objeto `meta`, pero Northwind
        los envía en el objeto `metadata`
    **/
    extractMeta: function (store, type, payload) {

        if (payload && payload.metadata) {
            store.metaForType(type, payload.metadata);
            delete payload.metadata;
        }

    },

    /**
        `serializeIntoHash`

        El JSON que necesita Northwind en las operaciones POST/PUT no tiene
        elemento raíz, así que sobreescribimos este método para eliminarlo
    **/
    serializeIntoHash: function(hash, type, record, options) {

        Ember.merge(hash, this.serialize(record, options));

    }

});

