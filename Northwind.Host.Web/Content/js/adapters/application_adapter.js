// http://stackoverflow.com/questions/16037175/ember-data-serializer-data-mapping/16042261#16042261
Northwind.ApplicationAdapter = DS.RESTAdapter.extend({
    host: 'http://localhost:2828',
    defaultSerializer: 'Northwind.ApplicationSerializer'
});