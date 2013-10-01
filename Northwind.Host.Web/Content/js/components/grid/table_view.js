/**
	`TableView` 

	@class 		TableView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.View

 */

Northwind.Common.Components.Grid.TableView = Ember.View.extend({

    tagName: 'table',

    classNames: ['table', 'table-striped', 'table-condensed'],

    defaultTemplate: function () {

        var headerView = '<thead>{{view Northwind.Common.Components.Grid.HeaderView}}</thead>';
        var bodyView = '{{view Northwind.Common.Components.Grid.BodyView}}';
        //var footerView = '{{view Northwind.Common.Components.Grid.FooterView}}';

        return Ember.Handlebars.compile(headerView + bodyView/* + footerView*/);

    }.property()

});
