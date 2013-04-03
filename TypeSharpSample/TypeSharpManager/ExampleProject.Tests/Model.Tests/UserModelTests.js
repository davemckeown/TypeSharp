var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var ExampleProject;
(function (ExampleProject) {
    (function (Tests) {
        (function (Model) {
            /// <reference path="..\..\ExampleProject\Model\UserModel.ts" />
            /// <reference path="..\..\tsUnit.ts" />
            (function (Tests) {
                var UserModelTests = (function (_super) {
                    __extends(UserModelTests, _super);
                    function UserModelTests() {
                        _super.apply(this, arguments);

                    }
                    return UserModelTests;
                })(tsUnit.TestClass);
                Tests.UserModelTests = UserModelTests;                
            })(Model.Tests || (Model.Tests = {}));
            var Tests = Model.Tests;
        })(Tests.Model || (Tests.Model = {}));
        var Model = Tests.Model;
    })(ExampleProject.Tests || (ExampleProject.Tests = {}));
    var Tests = ExampleProject.Tests;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=UserModelTests.js.map
