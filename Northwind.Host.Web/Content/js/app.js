/**

    Northwind main application

**/

Northwind = Ember.Application.create();

Northwind.Router.map(function () {
  // put your routes here
});

Northwind.IndexRoute = Ember.Route.extend({
  model: function() {
    return ['red', 'yellow', 'blue'];
  }
});
