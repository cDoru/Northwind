/**
	`PageView` 

	@class 		PageView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.PageView = Ember.View.extend({

	classNames: ['pull-left', 'table-page'],

	defaultTemplate: Ember.Handlebars.compile('Showing {{view.first}} - {{view.last}} from {{filteredContent.length}}'),

	/**
		didPageChange
	**/
	didPageChange: function () {

		var page = this.get('controller.page');
		var limit = this.get('controller.limit');
		var length = this.get('controller.filteredContent.length');

		this.set('first', page * limit + 1);
		this.set('last', Math.min(length, page * limit + limit))

	}.observes('controller.page', 'controller.filteredContent.length')

});