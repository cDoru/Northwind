/**
	`PageListView` 

	@class 		PageListView
	@namespace 	Northwind.Common.Components.Grid
	@extends 	Ember.ContainerView

 */

Northwind.Common.Components.Grid.PageListView = Ember.ContainerView.extend({

    tagName: 'ul',

    classNames: ['pagination', 'pagination-sm'],

    pages: [],

    visiblePages: 3,

    /**
        firstPageView
    **/
    firstPageView: Ember.View.extend({
        tagName: 'li',
        classNameBindings: ['parentView.hasFirstPage::disabled'],
        template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action firstPage target="view.parentView"}}>&laquo;</a>')

    }),

    /**
        prevPageView
    **/
    prevPageView: Ember.View.extend({
        tagName: 'li',
        classNameBindings: ['parentView.hasPreviousPage::disabled'],
        template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action prevPage target="view.parentView"}}>&lsaquo;</a>')
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
        template: Ember.Handlebars.compile('<a href="javascript:void(0);" {{action nextPage target="view.parentView"}}>&rsaquo;</a>')
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
        var pages = this.get('pages');

        if (!pages.get('length')) return;

        this.clear();
        this.pushObject(this.get('firstPageView').create());
        this.pushObject(this.get('prevPageView').create());

        var self = this;


        this.get('pages').forEach(function (page) {            
            var pageView = self.get('pageView').create({
                content: page
            });

            self.pushObject(pageView);
        });

        this.pushObject(this.get('nextPageView').create());
        this.pushObject(this.get('lastPageView').create());
    }.observes('pages'),

    /**
        createPages
    **/
    createPages: function () {

        if (!this.get('controller')) return [];

        var currentPage = this.get('controller.page');
        var pages = this.get('controller.pages');
        var pagesFrom = Math.max(0, currentPage - this.visiblePages);
        var pagesTo = Math.min(pages, currentPage + this.visiblePages + 1);
        var limit = this.get('controller.limit');
        
        var pages = [];

        for (var i = pagesFrom; i < pagesTo; i++) {
            pages.push({
                index: i,
                page: i + 1,
                isActive: (i == currentPage)
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

    }.observes('controller.offset', 'controller.pages', 'controller.page').on('init'),

    /**
        actions
    **/
    actions: {
        /**
            setPage
        **/
        setPage: function (context) {

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

        }
    },

    /**
        init
    **/
    init: function () {
        this._super();
        this.refreshPageListItems();
    }

});
