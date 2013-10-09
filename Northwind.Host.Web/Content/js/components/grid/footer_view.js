﻿/**
	`FooterView` 

	@class 		FooterView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.CollectionView

 */

Northwind.Common.Components.Grid.FooterView = Ember.ContainerView.extend({

    tagName: 'tfoot',

    classNames: ['table-footer', 'text-muted'],

    childViews: ['gridFooter'],

    gridFooter: Ember.View.create({

        tagName: 'tr',

        template: Ember.Handlebars.compile(
            '<td {{bindAttr colspan="controller.columns.length"}}>' + 
                '{{view Northwind.Common.Components.Grid.PageView}}' + 
                '{{view Northwind.Common.Components.Grid.PaginationView}}' + 
            '</td>'
        )

    })

});