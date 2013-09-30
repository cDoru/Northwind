/**
	`FooterView` 

	@class 		FooterView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.FooterView = Ember.CollectionView.extend({

 	tagName: 'tfoot',

 	classNames: ['table-footer'],

 	defaultTemplate: function () {
 		
 		return Ember.Handlebars.compile('<tr><td {{bindAttr colspan="controller.columns.length"}}>{{view Northwind.Common.Components.Grid.PaginationView</td></tr>');

 	}.property()

 });