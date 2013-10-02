/**
	`GridController` ofrece una manera de listar elementos de una colección de 
  	objetos con la posibilidad de mostrar los datos mediante lista paginada

	@class 		GridController
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ArrayController
	@uses		Northwind.Common.Components.Grid.PaginationMixin		
 */

Northwind.Common.Components.Grid.GridController = Ember.ArrayController.extend(Northwind.Common.Components.Grid.Pagination, {

    columns: [],

    paginableContentBinding: 'content',

    rowsBinding: 'paginatedContent',

    visibleColumns: function () {

        return this.get('columns').filterProperty('visible', true);

    }.property('columns.@each.visible')

});