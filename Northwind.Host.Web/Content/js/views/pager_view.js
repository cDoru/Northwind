/**
    @class PagerView
**/

Northwind.PagerView = Ember.View.extend({
    templateName: 'pager',
    tagName: 'ul',
    classNames: ['pager'],

    /**
        self
    **/
    self: function () {

        return this.get('controller.metadata.links.self');

    }.property(),

    /**
        previous
    **/
    previous: function () {

        return this.get('controller.metadata.links.previous');

    }.property(),

    /**
        next
    **/
    next: function () {        

        return this.get('controller.metadata.links.next');

    }.property(),

    /**
        first
    **/
    first: function () {

        return this.get('controller.metadata.links.first');

    }.property(),

    /**
        last
    **/
    last: function () {

        return this.get('controller.metadata.links.last');

    }.property()
});