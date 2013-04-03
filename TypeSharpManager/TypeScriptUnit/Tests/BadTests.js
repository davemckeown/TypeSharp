var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
/// <reference path="../tsUnit.ts" />
var BadTests;
(function (BadTests) {
    var Composer = (function () {
        function Composer() { }
        Composer.compose = function compose(test) {
            test.addTestClass(new DeliberateFailures(), 'DeliberateFailures');
        };
        return Composer;
    })();
    BadTests.Composer = Composer;    
    var DeliberateFailures = (function (_super) {
        __extends(DeliberateFailures, _super);
        function DeliberateFailures() {
            _super.apply(this, arguments);

        }
        DeliberateFailures.prototype.deliberateBadTest = function () {
            this.areIdentical('1', 1);
        };
        return DeliberateFailures;
    })(tsUnit.TestClass);    
})(BadTests || (BadTests = {}));
//@ sourceMappingURL=BadTests.js.map
