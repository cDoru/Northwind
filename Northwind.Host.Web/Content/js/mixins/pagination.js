var get = Ember.get;
var set = Ember.set;
var forEach = Ember.EnumerableUtils.forEach;

/**
    
    @class Pagination
    @namespace Ember
    @extends Ember.Mixin

        Implementa paginación para ArrayController

**/

Ember.Pagination = Ember.Mixin.create({

    /**
        pages
    **/
    pages: function () {

        var pages = [];
        var page = 0;
        var totalPages = this.get('totalPages');

        for (i = 0; i < totalPages; i++) {
            page = i + 1;
            pages.push({ page_id: page.toString() });
        }

        return pages;

    }.property('totalPages'),


    /**
        currentPage
    **/
    currentPage: function () {

        return parseInt(this.get('selectedPage'), 10) || 1;

    }.property('selectedPage'),

    /**
        nextPage
    **/
    nextPage: function () {

        var nextPage = this.get('currentPage') + 1;
        var totalPages = this.get('totalPages');

        if (nextPage <= totalPages) {
            return Ember.Object.create({ id: nextPage });
        } else {
            return Ember.Object.create({ id: this.get('currentPage') });
        }

    }.property('currentPage', 'totalPages'),

    /**
        previousPage
    **/
    previousPage: function () {

        var prevPage = this.get('currentPage') - 1;

        if (prevPage > 0) {
            return Ember.Object.create({ id: prevPage });
        } else {
            return Ember.Object.create({ id: this.get('currentPage') });
        }

    }.property('currentPage'),

    /**
        totalPages
    **/
    totalPages: function () {

        return Math.ceil((this.get('content.length') / this.get('limit')) || 1);

    }.property('content.length'),

    /**
        paginatedContent
    **/
    paginatedContent: function () {

        var selectedPage = this.get('selectedPage') || 1;
        var upperBound = (selectedPage * this.get('limit'));
        var lowerBound = (selectedPage * this.get('limit') - this.get('limit'));
        var models = this.get('content');

        return models.slice(lowerBound, upperBound);

    }.property('selectedPage', 'content.@each')

});