/**
	`PageView` 

	@class 		PageView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.PageView = Ember.View.extend({

    classNames: ['pull-left', 'table-page'],

    defaultTemplate: Ember.Handlebars.compile('Showing {{controller.offset}} - {{view.last}} from {{controller.totalCount}}'),

    /**
        didPageChange
    **/
    didPageChange: function () {        

        var limit = this.get('controller.limit');
        var length = this.get('controller.totalCount');
        var offset = this.get('controller.offset');        

        this.set('first', offset);
        this.set('last', Math.min(length, (offset - 1) + limit));

    }.observes('controller.offset', 'controller.limit', 'controller.totalCount').on('init')

});