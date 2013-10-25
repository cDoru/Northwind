/**
    `ObjectController` 

    @class 		ObjectController
    @namespace 	Northwind
    @extends 	Ember.ObjectController

*/
Northwind.ObjectController = Ember.ObjectController.extend({

	/**
		Indica si se está editando un registro

		@property	isEditing
		@type		{Boolean}

	**/
	isEditing: false,

	actions: {

		/**
			Modificación de un registro

			@action 	edit
		**/
		edit: function () {

			this.set('isEditing', true);

		},

		/**
			Se guardan los datos del registro

			@action 	acceptChanges
		**/
		acceptChanges: function () {

			this.set('isEditing', false);

			// Comprobamos que los campos obligatorios tienen datos
			if (this.get('model.isSaveable'))
			{
				this.send('save');
			} else {
				this.send('remove');
			}

		},

		/**
			Se guardan los datos del registro en la base de datos

			@action 	save
		**/
		save: function () {

			this.get('model').save();

		},

		/**
			Se elimina el registro

			@action 	remove
		**/
		remove: function () {

			this.get('model').deleteRecord();
			this.send('save');

		}

	}

});