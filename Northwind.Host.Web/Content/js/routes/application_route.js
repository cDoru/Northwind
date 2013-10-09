﻿/**
**/
Northwind.Router.map(function () {
    this.resource('customers', function () {
        this.resource('customer', { path: ':customer_id' }, function () {
        	this.route('orders');
        });
    });
    this.resource('about');
});

/**
**/
Northwind.Router.reopen({
    location: 'history'
});