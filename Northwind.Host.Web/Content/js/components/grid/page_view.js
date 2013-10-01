/**
	`PageView` 

	@class 		PageView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.PageView = Ember.View.extend({

    classNames: ['pull-left', 'table-page'],

    first: 0,

    last: 0,

    defaultTemplate: Ember.Handlebars.compile('Showing {{view.first}} - {{view.last}} from {{controller.totalCount}}'),

    /**
    didPageChange
    **/
    didPageChange: function () {

        console.log('PageView.didPageChange');

        var limit = this.get('controller.limit');
        var length = this.get('controller.totalCount');
        var offset = this.get('controller.offset');

        this.set('first', offset);
        this.set('last', Math.min(length, offset + limit));

    }.observes('controller.page', 'controller.totalCount')

});