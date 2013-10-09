/**
	PaginationMixin
 **/
Northwind.Common.Components.Grid.Pagination = Ember.Mixin.create({
    /**
        totalCount
    **/
    totalCount: 0,    

    /**
        limit
    **/
    limit: 0,

    /**
        page
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
        offset
    **/
    offset: function () {

        var page = this.get('page');
        var limit = this.get('limit');

        return (page * limit) + 1;

    }.property('page'),


    /**
        paginatedContent
    **/
    paginatedContent: function () {

        if (this.get('page') >= this.get('pages')) {
            this.set('page', 0);
        }
        
        return this.get('content');

    }.property('@each', 'page', 'limit'),


    /**
        pages
    **/
    pages: function () {        

        return Math.ceil(this.get('totalCount') / this.get('limit'));

    }.property('totalCount', 'limit'),

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