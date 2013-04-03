var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var ExampleProject;
(function (ExampleProject) {
    (function (Tests) {
        (function (Controller) {
            (function (Tests) {
                (function (Data) {
                    /// <reference path="..\..\..\ExampleProject\Controller\Data\ItemController.ts" />
                    /// <reference path="..\..\..\tsUnit.ts" />
                    (function (Tests) {
                        var ItemControllerTests = (function (_super) {
                            __extends(ItemControllerTests, _super);
                            function ItemControllerTests() {
                                _super.apply(this, arguments);

                            }
                            return ItemControllerTests;
                        })(tsUnit.TestClass);
                        Tests.ItemControllerTests = ItemControllerTests;                        
                    })(Data.Tests || (Data.Tests = {}));
                    var Tests = Data.Tests;
                })(Tests.Data || (Tests.Data = {}));
                var Data = Tests.Data;
            })(Controller.Tests || (Controller.Tests = {}));
            var Tests = Controller.Tests;
        })(Tests.Controller || (Tests.Controller = {}));
        var Controller = Tests.Controller;
    })(ExampleProject.Tests || (ExampleProject.Tests = {}));
    var Tests = ExampleProject.Tests;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=ItemControllerTests.js.map
