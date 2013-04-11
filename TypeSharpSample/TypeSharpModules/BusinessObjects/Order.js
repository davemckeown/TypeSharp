/// <reference path="BusinessObjects.d.ts" />
var BusinessObjects;
(function (BusinessObjects) {
    /**
    * @classdesc Testing Comment
    */
    /**
    * @classdesc BusinessObjects
    */
    var Order = (function () {
        function Order() { }
        Order.prototype.AddProducts = /**
        * Add products to the order
        * @param products The products to add
        * @param group The group name
        */
        function (products, group) {
            /** @todo Implement AddProducts */
            return;
        };
        return Order;
    })();
    BusinessObjects.Order = Order;    
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=Order.js.map
