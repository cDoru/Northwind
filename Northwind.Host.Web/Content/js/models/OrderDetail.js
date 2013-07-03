/**
	@class		OrderDetail
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.OrderDetail = Northwind.Model.extend({
	id: DS.attrib('string'),
	productId: DS.attrib('long'),
	unitPrice: DS.attrib('decimal'),
	quantity: DS.attrib('long'),
	discount: DS.attrib('double')
	
});
