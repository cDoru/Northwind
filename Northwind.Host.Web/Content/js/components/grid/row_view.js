/**
	`RowView` 

	@class 		RowView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.RowView = Ember.ContainerView.extend({

    tagName: 'tr',

    classNames: ['table-row'],

    rowBinding: 'content',

    columnsBinding: 'controller.visibleColumns',

    /**
        columnsDidChange
    **/
    columnsDidChange: function () {

        if (this.get('columns')) {
            this.clear();
            this.get('columns').forEach(function (column) {
                var cell = column.get('viewClass').create({
                    column: column,
                    content: this.get('row')
                });

                this.pushObject(cell);

            }, this);
        }

    }.observes('columns.@each'),

    /**
    init
    **/
    init: function () {
        this._super();
        this.columnsDidChange();
    }

});