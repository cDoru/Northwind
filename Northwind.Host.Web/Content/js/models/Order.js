/**
	@class		Order
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Order = Northwind.Model.extend({
	id: DS.attrib('long'),
	employeeId: DS.attrib('long'),
	orderDate: DS.attrib('string'),
	requiredDate: DS.attrib('string'),
	shippedDate: DS.attrib('string'),
	freight: DS.attrib('decimal'),
	shipName: DS.attrib('string'),
	shipAddress: DS.attrib('string'),
	shipCity: DS.attrib('string'),
	shipRegion: DS.attrib('string'),
	shipPostalCode: DS.attrib('string'),
	shipCountry: DS.attrib('string')
	
});
