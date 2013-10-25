/**
    `ApplicationAdapter` 

    @class 		ApplicationAdapter
    @namespace 	Northwind
    @extends 	DS.RESTAdapter
    @see 		http://stackoverflow.com/questions/16037175/ember-data-serializer-data-mapping/16042261#16042261

*/
Northwind.ApplicationAdapter = DS.RESTAdapter.extend({

	/**
		host
	**/
    host: 'http://localhost:2828',

    /**
    	serializer
    **/
    serializer: Northwind.ApplicationSerializer


});