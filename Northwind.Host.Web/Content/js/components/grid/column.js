/**
    `TableView` 

    @class 		Column
    @namespace 	Northwind.Common.Components.Grid
    @extends 	Ember.Object

*/

Northwind.Common.Components.Grid.Column = Ember.Object.extend({

    property: null,

    display: true,

    formatter: '{{view.content.%@}}',

    /**
        header
    **/
    header: function () {

        if (!this.get('property')) return '';

        return this.get('property').capitalize();

    } .property('property'),

    /**
        visible
    **/
    visible: function () {

        return this.get('display') != false;

    }.property('display'),

    /**
        always
    **/
    always: function () {

        return this.get('display') === 'always';

    }.property('display'),    

    /**
        viewClass
    **/
    viewClass: function () {
        var formatter = this.get('formatter');

        if (Northwind.Common.Components.Grid.CellView.detect(formatter)) {
            return formatter;
        } else {
            Ember.assert('Formatter has to extend CellView or Handlebar template', Ember.typeOf(formatter) === 'string');

            var property = this.get('property');

            if (!property) {
                property = 'constructor';
            }

            var template = this.get('formatter').fmt(property);

            return Northwind.Common.Components.Grid.CellView.extend({
                template: Ember.Handlebars.compile(template)
            });
        };
    }.property()

});

/**
    `TableView` 

    @class      Column
    @namespace  Northwind.Common.Components.Grid
    @extends    Ember.Object

*/

Northwind.Common.Components.Grid.column = function (property, options) {

    if (Ember.typeOf(property) === 'object') {
        options = property;
        property = null;
    }

    var column = Northwind.Common.Components.Grid.Column.create({
        property: property
    });

    if (options) {
        for (var key in options) {
            column.set(key, options[key]);
        };
    }

    return column;
};