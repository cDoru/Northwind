/**
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