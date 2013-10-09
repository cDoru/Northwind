/**
    CustomersRoute
**/
Northwind.CustomersRoute = Ember.Route.extend({    

    /**
        model
    **/
    model: function () {

        var controller = this.controllerFor('customer');        

        return this.get('store').findQuery('customer', { offset: controller.offset, limit: controller.limit });

    },   

    /**
        setupController
    **/
    setupController: function (controller, model) {
        controller.set('content', model);
        controller.set('contentLoaded', true);
    }

});