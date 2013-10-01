﻿/**
	`PaginationView` 

	@class 		PageListView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ContainerView

 */

Northwind.Common.Components.Grid.PaginationView = Ember.ContainerView.extend({

	tagName: 'div',

	classNames: ['pagination', 'pagination-small', 'pagination-right', 'table-pagination'],

	childViews: ['pageList'],

	/**
		pageList
	**/
	pageList: Northwind.Common.Components.Grid.PageListView.create()

});