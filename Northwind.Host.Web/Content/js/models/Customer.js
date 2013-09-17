/**
	@class		Customer
	@extends	Em.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Customer = DS.Model.extend({
	//id: DS.attr('string'),
	companyName: DS.attr('string'),
	contactName: DS.attr('string'),
	contactTitle: DS.attr('string'),
	address: DS.attr('string'),
	city: DS.attr('string'),
	region: DS.attr('string'),
	postalCode: DS.attr('string'),
	country: DS.attr('string'),
	phone: DS.attr('string'),
	fax: DS.attr('string'),
	    
    orders: DS.hasMany('order')
});
