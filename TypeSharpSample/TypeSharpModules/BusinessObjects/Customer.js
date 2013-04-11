/// <reference path="BusinessObjects.d.ts" />
var BusinessObjects;
(function (BusinessObjects) {
    /**
    * @classdesc Testing comment
    */
    /**
    * @classdesc BusinessObjects
    */
    var Customer = (function () {
        function Customer() { }
        Customer.prototype.AssignOrder = /**
        * Assign the Order to the Customer
        * @param order
        * @return
        */
        function (order) {
            /** @todo Implement AssignOrder */
            return null;
        };
        return Customer;
    })();
    BusinessObjects.Customer = Customer;    
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=Customer.js.map
