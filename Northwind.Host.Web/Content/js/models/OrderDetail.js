/**
	@class		OrderDetail
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.OrderDetail = Northwind.Model.extend({
	productId: DS.attr('long'),
	unitPrice: DS.attr('decimal'),
	quantity: DS.attr('long'),
	discount: DS.attr('double'),

    order: DS.belongsTo('order')
	
});
