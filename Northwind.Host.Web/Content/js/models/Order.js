/**
	@class		Order
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Order = Northwind.Model.extend({
	employeeId: DS.attr('long'),
	orderDate: DS.attr('string'),
	requiredDate: DS.attr('string'),
	shippedDate: DS.attr('string'),
	freight: DS.attr('decimal'),
	shipName: DS.attr('string'),
	shipAddress: DS.attr('string'),
	shipCity: DS.attr('string'),
	shipRegion: DS.attr('string'),
	shipPostalCode: DS.attr('string'),
	shipCountry: DS.attr('string'),
	
    customer: DS.belongsTo('customer'),

    details: DS.hasMany('orderDetail')
});
