var BusinessObjects;
(function (BusinessObjects) {
    /// <reference path="BusinessObjects.DataStore.d.ts" />
    (function (DataStore) {
        /**
        * @classdesc DataServices Class
        */
        /**
        * @classdesc BusinessObjects.DataStore
        */
        var DataService = (function () {
            function DataService() { }
            DataService.prototype.LoadProducts = /**
            * Load the Products
            * @return All Products
            */
            function () {
                /** @todo Implement LoadProducts */
                return null;
            };
            DataService.prototype.LoadCustomers = /**
            * Load the Customers
            * @return Loads all Customers
            */
            function () {
                /** @todo Implement LoadCustomers */
                return null;
            };
            DataService.prototype.LoadOrders = /**
            * Load all Orders
            * @return Loads all Orders
            */
            function () {
                /** @todo Implement LoadOrders */
                return null;
            };
            return DataService;
        })();
        DataStore.DataService = DataService;        
    })(BusinessObjects.DataStore || (BusinessObjects.DataStore = {}));
    var DataStore = BusinessObjects.DataStore;
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=DataService.js.map
