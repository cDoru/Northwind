/**
	@class		Order
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Order = DS.Model.extend({
	id: DS.attrib('long'),
	customerId: DS.attrib('string'),
	employeeId: DS.attrib('long'),
	orderDate: DS.attrib('string'),
	requiredDate: DS.attrib('string'),
	shippedDate: DS.attrib('string'),
	shipVia: DS.attrib('long?'),
	freight: DS.attrib('decimal'),
	shipName: DS.attrib('string'),
	shipAddress: DS.attrib('string'),
	shipCity: DS.attrib('string'),
	shipRegion: DS.attrib('string'),
	shipPostalCode: DS.attrib('string'),
	shipCountry: DS.attrib('string'),
	detail: DS.attrib('System.Collections.Generic.List<Northwind.ServiceModel.Dto.OrderDetail>')
});
