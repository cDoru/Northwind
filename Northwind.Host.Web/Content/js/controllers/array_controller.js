/**
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