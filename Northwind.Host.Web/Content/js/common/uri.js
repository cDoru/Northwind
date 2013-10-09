/**
	Funciones relacionadas con Url

	@class		Uri
	@namespace	Northwind
	@module		Northwind
	@extends	Ember.Object
**/
Northwind.Common.Uri = Ember.Object.create({
	
	/**
		Extrae los argumentos de una Url en forma de objeto

		@method	queryParams
		@param  {String} Uri de donde se extraerán sus parámetros
		@see 	https://gist.github.com/simonsmith/5152680
	**/
	queryParams: function (uri) {

		var queryParams = {};

        if (uri) {
            var reg = /\\?([^?=&]+)(=([^&#]*))?/g;

            uri.replace(reg, function ($0, $1, $2, $3) {
                if (typeof $3 == 'string') {
                    queryParams[$1] = decodeURIComponent($3);
                }
            });
        }

        return queryParams;

	}

});