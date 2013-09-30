/**
	`HeaderView` 

	@class 		HeaderView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.HeaderView = Ember.CollectionView.extend({

 	tagName: 'tr',

    contentBinding: 'controller.visibleColumns',

 	itemViewClass: Ember.View.extend({
 	    tagName: 'th',
        classNames: ['table-header-cell'],
 		template: Ember.Handlebars.compile('{{view.content.header}}')        
 	})

 });