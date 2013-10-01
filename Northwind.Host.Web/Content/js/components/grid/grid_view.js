/**
	`TableView` 

	@class 		GridView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.GridView = Ember.View.extend({

    classNames: ['grid'],

    defaultTemplate: Ember.Handlebars.compile('{{view Northwind.Common.Components.Grid.TableView}}{{view Northwind.Common.Components.Grid.FooterView}}')    

});