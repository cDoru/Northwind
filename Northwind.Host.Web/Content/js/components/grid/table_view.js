﻿/**
	`TableView` 

	@class 		TableView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.TableView = Ember.View.extend({

 	tagName: 'table',

 	classNames: ['table', 'table-striped', 'table-condensed'],

 	defaultTemplate: Ember.Handlebars.compile('<thead>{{view Northwind.Common.Components.Grid.HeaderView}}</thead>{{view Northwind.Common.Components.Grid.BodyView}}')

 });