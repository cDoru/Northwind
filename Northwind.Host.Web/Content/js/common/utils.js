/**
    @extends	Ember.Namespace
    @namespace	Northwind
    @module		@Northwind
**/

Northwind.Common = Ember.Namespace.create({
    Utils: Ember.Namespace.create()
});

/**
    @class      UriUtis
    @extends	Ember.Namespace
    @namespace	Northwind.Common.Utils
    @module		Northwind
**/
Northwind.Common.Utils.UriUtils = Ember.Object.extend({

    /**
    Extrae los argumentos de una Url en forma de objeto

    @see https://gist.github.com/simonsmith/5152680
    **/
    parseQueryParams: function (uri) {
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

Northwind.uriUtils = Northwind.Common.Utils.UriUtils.create();

/**
**/
Northwind.Common.PaginationMetadata = Ember.Object.extend({
    rel: null,
    offset: 0,
    limit: 0    
});
