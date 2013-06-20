/**
	@class		Customer
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Customer = DS.Model.extend({
	id: DS.attrib('string'),
	companyName: DS.attrib('string'),
	contactName: DS.attrib('string'),
	contactTitle: DS.attrib('string'),
	address: DS.attrib('string'),
	city: DS.attrib('string'),
	region: DS.attrib('string'),
	postalCode: DS.attrib('string'),
	country: DS.attrib('string'),
	phone: DS.attrib('string'),
	fax: DS.attrib('string'),
	orders: DS.attrib('System.Collections.Generic.List<Northwind.ServiceModel.Dto.Order>')
});
