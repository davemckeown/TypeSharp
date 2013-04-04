var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var BusinessObjects;
(function (BusinessObjects) {
    /// <reference path="..\BusinessObjects\Product.ts" />
    /// <reference path="..\tsUnit.ts" />
    (function (Tests) {
        var ProductTests = (function (_super) {
            __extends(ProductTests, _super);
            function ProductTests() {
                _super.apply(this, arguments);

            }
            return ProductTests;
        })(tsUnit.TestClass);
        Tests.ProductTests = ProductTests;        
    })(BusinessObjects.Tests || (BusinessObjects.Tests = {}));
    var Tests = BusinessObjects.Tests;
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=ProductTests.js.map
