var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var ExampleProject;
(function (ExampleProject) {
    (function (Tests) {
        (function (Model) {
            (function (Tests) {
                (function (Data) {
                    /// <reference path="..\..\..\ExampleProject\Model\Data\DataRepository.ts" />
                    /// <reference path="..\..\..\tsUnit.ts" />
                    (function (Tests) {
                        var DataRepositoryTests = (function (_super) {
                            __extends(DataRepositoryTests, _super);
                            function DataRepositoryTests() {
                                _super.apply(this, arguments);

                            }
                            return DataRepositoryTests;
                        })(tsUnit.TestClass);
                        Tests.DataRepositoryTests = DataRepositoryTests;                        
                    })(Data.Tests || (Data.Tests = {}));
                    var Tests = Data.Tests;
                })(Tests.Data || (Tests.Data = {}));
                var Data = Tests.Data;
            })(Model.Tests || (Model.Tests = {}));
            var Tests = Model.Tests;
        })(Tests.Model || (Tests.Model = {}));
        var Model = Tests.Model;
    })(ExampleProject.Tests || (ExampleProject.Tests = {}));
    var Tests = ExampleProject.Tests;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=DataRepositoryTests.js.map
