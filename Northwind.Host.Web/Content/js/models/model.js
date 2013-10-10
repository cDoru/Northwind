/**
	Clase base para todos los modelos definidos en Northwind

	@class		Model
	@extends	DS.Model
	@namespace	Northwind
	@module		@Northwind
**/

Northwind.Model = DS.Model.extend({

	required: [],

	/**
		Comprueba si todas las propiedades obligatorias del modelo tienen valor

		@property	isSaveable
		@type		{Boolean}
	**/
	isSaveable: function () {

		var required = this.get('required');
		var self = this;
		
		required.forEach(function (value) {			
			if(!self.get(value)) {
				return false;
			}
		});

		return true;

	}.property('required')

});
