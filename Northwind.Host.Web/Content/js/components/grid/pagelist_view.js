/**
	`PageListView` 

	@class 		PageListView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ContainerView

 */

Northwind.Common.Components.Grid.PageListView = Ember.ContainerView.extend({

	tagName: 'ul',

	pages: [],

	visiblePages: 3, 

	/**
		firstPageView
	 **/
	firstPageView: Ember.View.extend({

		tagName: 'li',
		classNames: ['parent.hasFirstPage::disabled'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action firstPage target="view.parentView"}}>&laquo;</a>')

	 }),

	/**
		prevPageView
	 **/
	prevPageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['parent.hasPreviousPage::disabled'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action prevPage target="view.parentView"}}>&lsquo;</a>')
	}),

	/**
		pageView
	 **/
	pageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['content.isActive:active'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action setPage view.content target="view.parentView"}}>{{view.content.page}}</a>')
	}),

	/**
		nextPageView
	**/
	nextPageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['parentView.hasNextPage::disabled'],
		templage: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action nextPage target="view.parentView"}}>&rsquo;</a>')
	}),

	/**
		nextPageView
	**/
	lastPageView: Ember.View.extend({
		tagName: 'li',
		classNameBindings: ['parentView.hasLastPage::disabled'],
		template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action lastPage target="view.parentView"}}>&raquo;</a>')
	}),

	/**
		refreshPageListItems
	**/
	refreshPageListItems: function () {
		var pages =  this.get('pages');
		if (!pages.get('length')) return;

		this.clear();
		this.pushObject(this.get('firstPageView').create());
		this.pushObject(this.get('prevPageView').create());

		var self = this;

		this.get('pages').forEach(function (page) {
			var pageView = self.get('pageView').create({
				content: page
			});
		});

		this.pushObject(this.get('nextPageView').create());
		this.pushObject(this.get('lastPageView').create());
	}.observes('pages'),

	/**
		createPages
	**/
	createPages: function () {
		if (!this.get('controller')) return [];

		var page = this.get('controller.page');
		var pages = this.get('controller.pages');
		var pagesFrom = Math.max(0, page - this.visiblePages);
		var pagesTo = Math.min(pages, page + this.visiblePages + 1);
		var limit = this.get('controller.limit');

		var pages = [];

		for (var i = pagesFrom; i < pagesTo; i++) {
			pages.push({
				index: i, 
				page: i + 1,
				isActive: (i == page)
			});
		}

		this.set('pages', pages);
	},

	/**
		didControllerContentChanged
	**/
	didControllerContentChanged: function () {
		this.createPages();

		var pages = this.get('controller.pages');
		var page = this.get('controller.page');

		this.set('pagesCount', pages);
		this.set('hasNextPage', page + 1 < pages);
		this.set('hasPrevPage', page > 0);
		this.set('hasFirstPage', page > 0);
		this.set('hasLastPage', page + 1 < pages);
	}.observes('controller', 'controller.pages', 'controller.page'),

	/**
		setPage
	**/
	setPage: function () {
		this.get('controller').set('page', context.index);
	},

	/**
		firstPage
	**/		
	firstPage: function () {
		if (!this.get('hasFirstPage')) return;

		this.get('controller').firstPage();
	},

	/**
		lastPage
	**/
	lastPage: function () {
		if (!this.get('hasLastPage')) return;

		this.get('controller').lastPage();
	},

	/**
		lastPage
	**/
	prevPage: function () {
		if (!this.get('hasPrevPage')) return;

		this.get('controller').previousPage();
	},

	/**
		nextPage
	**/
	nextPage: function () {
		if (!this.get('hasNextPage')) return;

		this.get('controller').nextPage();
	},

	/**
		init
	**/
	init: function () {
		this._super();
		this.refreshPageListItems();
	}

 });