/**
	@class		OrderDetail
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.OrderDetail = DS.Model.extend({
//	id: DS.attr('string'),
	productId: DS.attr('long'),
	unitPrice: DS.attr('decimal'),
	quantity: DS.attr('long'),
	discount: DS.attr('double'),

    order: DS.belongsTo('order')
	
});
