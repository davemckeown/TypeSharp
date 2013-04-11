var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var BusinessObjects;
(function (BusinessObjects) {
    (function (Tests) {
        (function (DataStore) {
            /// <reference path="..\..\BusinessObjects\DataStore\DataService.ts" />
            /// <reference path="..\..\tsUnit.ts" />
            (function (Tests) {
                var DataServiceTests = (function (_super) {
                    __extends(DataServiceTests, _super);
                    function DataServiceTests() {
                        _super.apply(this, arguments);

                    }
                    return DataServiceTests;
                })(tsUnit.TestClass);
                Tests.DataServiceTests = DataServiceTests;                
            })(DataStore.Tests || (DataStore.Tests = {}));
            var Tests = DataStore.Tests;
        })(Tests.DataStore || (Tests.DataStore = {}));
        var DataStore = Tests.DataStore;
    })(BusinessObjects.Tests || (BusinessObjects.Tests = {}));
    var Tests = BusinessObjects.Tests;
})(BusinessObjects || (BusinessObjects = {}));
//@ sourceMappingURL=DataServiceTests.js.map
