/**
	`BodyView` 

	@class 		BodyView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.BodyView = Ember.CollectionView.extend({

 	tagName: 'tbody',

 	contentBinding: 'controller.rows',

 	classNames: ['table-body'],

 	itemViewClass: 'Northwind.Common.Components.Grid.RowView',

 	/**
        emptyView
 	**/
 	emptyView: Ember.View.extend({
 		tagName: 'tr',
 		template: Ember.Handlebars.compile('<td {{bindAttr colspan="controller.columns.length"}} class="muted">No hay elementos</td>')
 	})

 });