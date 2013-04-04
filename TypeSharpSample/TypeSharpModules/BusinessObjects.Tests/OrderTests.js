var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var BusinessObjects;
(function (BusinessObjects) {
    /// <reference path="..\BusinessObjects\Order.ts" />
    /// <reference path="..\tsUnit.ts" />
    (function (Tests) {
        var OrderTests = (function (_super) {
            __extends(OrderTests, _super);
            function OrderTests() {
                _super.apply(this, arguments);

            }
            return OrderTests;
        })(tsUnit.TestClass);
        Tests.OrderTests = OrderTests;        
    })(BusinessObjects.Tests || (BusinessObjects.Tests = {}));
    var Tests = BusinessObjects.Tests;
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=OrderTests.js.map
