/**
    @class      CustomersController
    @namespace  Northwind
    @extends    Northwind.ArrayController
**/
Northwind.CustomersController = Northwind.Common.Components.Grid.GridController.extend({

    columns: [
		Northwind.Common.Components.Grid.column('id', { formatter: '{{#link-to \'customer\' view.content}}{{view.content.id}}{{/link-to}}' }),
		Northwind.Common.Components.Grid.column('contactName'),
		Northwind.Common.Components.Grid.column('companyName'),
		Northwind.Common.Components.Grid.column('contactTitle')
	]

});