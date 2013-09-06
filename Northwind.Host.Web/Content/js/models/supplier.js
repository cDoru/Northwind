/**
	@class		Supplier
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Supplier = Northwind.Model.extend({
	id: DS.attrib('long'),
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
	homePage: DS.attrib('string')
	
});
