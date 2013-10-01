/**
	`FooterView` 

	@class 		FooterView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.FooterView = Ember.CollectionView.extend({

    //tagName: 'tfoot',

    classNames: ['table-footer'],

    defaultTemplate: function () {

        var pageView = '{{view Northwind.Common.Components.Grid.PageView}}';
        var paginationView = '{{view Northwind.Common.Components.Grid.PaginationView}}';

        //return Ember.Handlebars.compile('<td {{bindAttr colspan="controller.columns.length"}}>' + pageView + paginationView + '</td>');
        return Ember.Handlebars.compile(pageView + paginationView);
    }          

});