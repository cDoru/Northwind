/**
	Clase base para todos los modelos definidos en Northwind

	@class		Model
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Model = DS.Model.extend();

Northwind.Model.reopenClass({

	/**
		Define las propiedades obligatorias del modelo

		@property	required
		@type		{Array}
	**/
	required: Ember.computed(function () {		

		var required = Ember.makeArray();

		this.eachComputedProperty(function (name, meta) {
			if (meta.isAttribute && meta.options.required) {
				required.push(name);
			}
		});

		console.log(required);

		return required;

	}),

	/**
		Comprueba si todas las propiedades obligatorias del modelo tienen valor

		@property	isSaveable
		@type		{Boolean}
	**/
	isSaveable: Ember.computed(function () {

		var required = this.get('required');

		for (var prop in required) {
			if(!this.get(prop)) {
				return false;
			}
		}

		return true;

	})

});