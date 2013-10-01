/**
	PaginationMixin
 **/
Northwind.Common.Components.Grid.PaginationMixin = Ember.Mixin.create({
    /**
        totalCount
    **/
    totalCount: 0,

    /**
        offset
    **/
    offset: 0,

    /**
        limit
    **/
    limit: 10,

    /**
        limit
    **/
    page: 0,

    /**
        metadata
    **/
    metadata: null,

    /**
        paginableContentBinding
    **/
    paginableContentBinding: 'content',


    /**
        paginatedContent
    **/
    paginatedContent: Ember.computed(function () {

        if (this.get('page') >= this.get('pages')) {
            this.set('page', 0);
        }

        //return this.get('paginableContent').slice(this.get('offset'), this.get('offset') + this.get('limit'));
        return this.get('paginableContent');

    }).property('@each', 'page', 'limit', 'paginableContent'),


    /**
        pages
    **/
    pages: Ember.computed(function () {        

        return Math.ceil(this.get('totalCount') / this.get('limit'));

    }).property('totalCount', 'limit'),

    /**
        firstPage
    **/
    firstPage: function () {

        this.set('page', 0);

    },

    /**
        previousPage
    **/
    previousPage: function () {

        this.set('page', Math.max(this.get('page') - 1, 0));

    },

    /**
        nextPage
    **/
    nextPage: function () {

        this.set('page', Math.min(this.get('page') + 1, this.get('pages') - 1));

    },

    /**
        lastPage
    **/
    lastPage: function () {

        this.set('page', this.get('pages') - 1);

    }

});