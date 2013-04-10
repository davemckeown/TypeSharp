/// <reference path="BusinessObjects.d.ts" />
var BusinessObjects;
(function (BusinessObjects) {
    /**
    * Testing Comment
    */
    var Order = (function () {
        function Order() { }
        Order.prototype.AddProducts = /**
        * Add products to the order
        */
        function (products, group) {
        };
        return Order;
    })();
    BusinessObjects.Order = Order;    
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=Order.js.map
