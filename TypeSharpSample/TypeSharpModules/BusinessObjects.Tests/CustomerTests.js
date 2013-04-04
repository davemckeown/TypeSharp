var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var BusinessObjects;
(function (BusinessObjects) {
    /// <reference path="..\BusinessObjects\Customer.ts" />
    /// <reference path="..\tsUnit.ts" />
    (function (Tests) {
        var CustomerTests = (function (_super) {
            __extends(CustomerTests, _super);
            function CustomerTests() {
                _super.apply(this, arguments);

            }
            return CustomerTests;
        })(tsUnit.TestClass);
        Tests.CustomerTests = CustomerTests;        
    })(BusinessObjects.Tests || (BusinessObjects.Tests = {}));
    var Tests = BusinessObjects.Tests;
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=CustomerTests.js.map
