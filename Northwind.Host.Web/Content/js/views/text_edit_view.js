/**
    `TextEditView` 

    @class 		TextEditView
    @namespace 	Northwind
    @extends 	Ember.TextField

*/
Northwind.TextEditView = Ember.TextField.extend({

	didInsertElement: function () {

		this.$().focus();
		
	}

});

Ember.Handlebars.helper('text-edit', Northwind.TextEditView);
