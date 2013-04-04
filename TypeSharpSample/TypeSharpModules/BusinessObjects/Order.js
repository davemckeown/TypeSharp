/// <reference path="BusinessObjects.d.ts" />
var BusinessObjects;
(function (BusinessObjects) {
    var Order = (function () {
        function Order() { }
        Order.prototype.AddProducts = function (products, group) {
        };
        return Order;
    })();
    BusinessObjects.Order = Order;    
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=Order.js.map
