/// <reference path="Object.ts"/>
var TestCompileProject;
(function (TestCompileProject) {
    var TestSyntaxGenClass = (function () {
        function TestSyntaxGenClass() {
        }
        Object.defineProperty(TestSyntaxGenClass.prototype, "NumberProp", {
            get: function () {
                return this._NumberProp;
            },
            set: function (value) {
                this._NumberProp = value;
            },
            enumerable: true,
            configurable: true
        });


        Object.defineProperty(TestSyntaxGenClass.prototype, "GenericProp", {
            get: function () {
                return this._GenericProp;
            },
            set: function (value) {
                this._GenericProp = value;
            },
            enumerable: true,
            configurable: true
        });


        Object.defineProperty(TestSyntaxGenClass.prototype, "StringProp", {
            get: function () {
                return this._StringProp;
            },
            enumerable: true,
            configurable: true
        });

        /**
        * Void method as an example of a method accepting generics
        * @param key The key
        * @param value The value
        */
        TestSyntaxGenClass.prototype.VoidMethod = function (key, value) {
            /** @todo Implement VoidMethod */
            return;
        };
        return TestSyntaxGenClass;
    })();
    TestCompileProject.TestSyntaxGenClass = TestSyntaxGenClass;
})(TestCompileProject || (TestCompileProject = {}));

var testing = new TestCompileProject.TestSyntaxGenClass();

testing.GenericProp = "hello";

var testing2 = new TestCompileProject.TestSyntaxGenClass();

testing2.GenericProp = new System.Object();

testing.GenericProp = testing2.GenericProp.ToString();

testing.VoidMethod("hello", 123);
//# sourceMappingURL=app.js.map
