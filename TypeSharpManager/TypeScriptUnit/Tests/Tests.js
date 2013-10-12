/// <reference path="../tsUnit.ts" />
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Tests;
(function (Tests) {
    var Composer = (function () {
        function Composer() {
        }
        Composer.compose = function (test) {
            // Using a composer means we don't need to export the test classes
            // and it means changes are isolated to this file
            test.addTestClass(new SetUpTests(), 'SetUpTests');
            test.addTestClass(new TearDownTests(), 'TearDownTests');
            test.addTestClass(new FakesTests(), 'FakesTests');
            test.addTestClass(new AssertAreIdenticalTests(), 'AssertAreIdenticalTests');
            test.addTestClass(new AssertAreNotIdenticalTests(), 'AssertAreNotIdenticalTests');
            test.addTestClass(new IsTrueTests(), 'IsTrueTests');
            test.addTestClass(new IsFalseTests(), 'IsFalseTests');
            test.addTestClass(new IsTruthyTests(), 'IsTruthyTests');
            test.addTestClass(new IsFalseyTests(), 'IsFalseyTests');
            test.addTestClass(new FailTests(), 'FailTests');
        };
        return Composer;
    })();
    Tests.Composer = Composer;

    var RealClass = (function () {
        function RealClass() {
            this.name = 'Real';
        }
        RealClass.prototype.run = function () {
            return false;
        };
        RealClass.prototype.returnValue = function () {
            return this;
        };
        return RealClass;
    })();

    var SetUpTests = (function (_super) {
        __extends(SetUpTests, _super);
        function SetUpTests() {
            _super.apply(this, arguments);
        }
        SetUpTests.prototype.setUp = function () {
            this.testProperty = 'SETUP';
        };

        SetUpTests.prototype.testAfterInitialSetUp = function () {
            this.areIdentical('SETUP', this.testProperty);
            this.testProperty = 'OVERWRITE';
        };

        SetUpTests.prototype.testAfterInitialOverwritten = function () {
            this.areIdentical('SETUP', this.testProperty);
            this.testProperty = 'OVERWRITE';
        };
        return SetUpTests;
    })(tsUnit.TestClass);

    var TearDownTests = (function (_super) {
        __extends(TearDownTests, _super);
        function TearDownTests() {
            _super.apply(this, arguments);
            this.testProperty = '';
        }
        TearDownTests.prototype.tearDown = function () {
            this.testProperty = 'TEARDOWN';
        };

        TearDownTests.prototype.testAfterInitialSetUp = function () {
            this.areIdentical('', this.testProperty);
            this.testProperty = 'OVERWRITE';
        };

        TearDownTests.prototype.testAfterInitialOverwritten = function () {
            this.areIdentical('TEARDOWN', this.testProperty);
            this.testProperty = 'OVERWRITE';
        };
        return TearDownTests;
    })(tsUnit.TestClass);

    var FakesTests = (function (_super) {
        __extends(FakesTests, _super);
        function FakesTests() {
            _super.apply(this, arguments);
        }
        FakesTests.prototype.callDefaultFunctionOnFake = function () {
            var fakeObject = new tsUnit.Fake(new RealClass());
            var target = fakeObject.create();

            var result = target.run();

            this.areIdentical(undefined, result);
        };

        FakesTests.prototype.callSubstituteFunctionOnFake = function () {
            var fakeObject = new tsUnit.Fake(new RealClass());
            fakeObject.addFunction('run', function () {
                return true;
            });
            var target = fakeObject.create();

            var result = target.run();

            this.isTrue(result);
        };

        FakesTests.prototype.callSubstituteFunctionToObtainSecondFake = function () {
            var innerFake = new tsUnit.Fake(new RealClass());
            innerFake.addFunction('run', function () {
                return true;
            });
            var outerFake = new tsUnit.Fake(new RealClass());
            outerFake.addFunction('returnValue', function () {
                return innerFake.create();
            });
            var target = outerFake.create();

            var interimResult = target.returnValue();
            var result = interimResult.run();

            this.isTrue(result);
        };

        FakesTests.prototype.callDefaultPropertyOnFake = function () {
            var fakeObject = new tsUnit.Fake(new RealClass());
            var target = fakeObject.create();

            var result = target.name;

            this.areIdentical(null, result);
        };

        FakesTests.prototype.callSubstitutePropertyOnFake = function () {
            var fakeObject = new tsUnit.Fake(new RealClass());
            fakeObject.addProperty('name', 'Test');
            var target = fakeObject.create();

            var result = target.name;

            this.areIdentical('Test', result);
        };
        return FakesTests;
    })(tsUnit.TestClass);

    var AssertAreIdenticalTests = (function (_super) {
        __extends(AssertAreIdenticalTests, _super);
        function AssertAreIdenticalTests() {
            _super.apply(this, arguments);
        }
        AssertAreIdenticalTests.prototype.withIdenticalNumbers = function () {
            this.areIdentical(5, 5);
        };

        AssertAreIdenticalTests.prototype.withDifferentNumbers = function () {
            this.throws(function () {
                this.areIdentical(5, 4);
            });
        };

        AssertAreIdenticalTests.prototype.withIdenticalStings = function () {
            this.areIdentical('Hello', 'Hello');
        };

        AssertAreIdenticalTests.prototype.withDifferentStrings = function () {
            this.throws(function () {
                this.areIdentical('Hello', 'Jello');
            });
        };

        AssertAreIdenticalTests.prototype.withSameInstance = function () {
            var x = { test: 'Object' };
            var y = x;
            this.areIdentical(x, y);
        };

        AssertAreIdenticalTests.prototype.withDifferentInstance = function () {
            this.throws(function () {
                var x = { test: 'Object' };
                var y = { test: 'Object' };
                this.areIdentical(x, y);
            });
        };

        AssertAreIdenticalTests.prototype.withDifferentTypes = function () {
            this.throws(function () {
                this.areIdentical('1', 1);
            });
        };
        return AssertAreIdenticalTests;
    })(tsUnit.TestClass);

    var AssertAreNotIdenticalTests = (function (_super) {
        __extends(AssertAreNotIdenticalTests, _super);
        function AssertAreNotIdenticalTests() {
            _super.apply(this, arguments);
        }
        AssertAreNotIdenticalTests.prototype.withIdenticalNumbers = function () {
            this.throws(function () {
                this.areNotIdentical(4, 4);
            });
        };

        AssertAreNotIdenticalTests.prototype.withDifferentNumbers = function () {
            this.areNotIdentical(4, -4);
        };

        AssertAreNotIdenticalTests.prototype.withIdenticalStrings = function () {
            this.throws(function () {
                this.areNotIdentical('Hello', 'Hello');
            });
        };

        AssertAreNotIdenticalTests.prototype.withDifferentStrings = function () {
            this.areNotIdentical('Hello', 'Hella');
        };

        AssertAreNotIdenticalTests.prototype.withSameInstance = function () {
            this.throws(function () {
                var x = { test: 'Object' };
                var y = x;
                this.areNotIdentical(x, y);
            });
        };

        AssertAreNotIdenticalTests.prototype.withDifferentInstance = function () {
            var x = { test: 'Object' };
            var y = { test: 'Object' };
            this.areNotIdentical(x, y);
        };

        AssertAreNotIdenticalTests.prototype.withDifferentTypes = function () {
            this.areNotIdentical('1', 1);
        };
        return AssertAreNotIdenticalTests;
    })(tsUnit.TestClass);

    var IsTrueTests = (function (_super) {
        __extends(IsTrueTests, _super);
        function IsTrueTests() {
            _super.apply(this, arguments);
        }
        IsTrueTests.prototype.withBoolTrue = function () {
            this.isTrue(true);
        };

        IsTrueTests.prototype.withBoolFalse = function () {
            this.throws(function () {
                this.isTrue(false);
            });
        };
        return IsTrueTests;
    })(tsUnit.TestClass);

    var IsFalseTests = (function (_super) {
        __extends(IsFalseTests, _super);
        function IsFalseTests() {
            _super.apply(this, arguments);
        }
        IsFalseTests.prototype.withBoolFalse = function () {
            this.isFalse(false);
        };

        IsFalseTests.prototype.withBoolTrue = function () {
            this.throws(function () {
                this.isFalse(true);
            });
        };
        return IsFalseTests;
    })(tsUnit.TestClass);

    var IsTruthyTests = (function (_super) {
        __extends(IsTruthyTests, _super);
        function IsTruthyTests() {
            _super.apply(this, arguments);
        }
        IsTruthyTests.prototype.withBoolTrue = function () {
            this.isTruthy(true);
        };

        IsTruthyTests.prototype.withNonEmptyString = function () {
            this.isTruthy('Hello');
        };

        IsTruthyTests.prototype.withTrueString = function () {
            this.isTruthy('True');
        };

        IsTruthyTests.prototype.with1 = function () {
            this.isTruthy(1);
        };

        IsTruthyTests.prototype.withBoolFalse = function () {
            this.throws(function () {
                this.isTruthy(false);
            });
        };

        IsTruthyTests.prototype.withEmptyString = function () {
            this.throws(function () {
                this.isTruthy('');
            });
        };

        IsTruthyTests.prototype.withZero = function () {
            this.throws(function () {
                this.isTruthy(0);
            });
        };

        IsTruthyTests.prototype.withNull = function () {
            this.throws(function () {
                this.isTruthy(null);
            });
        };

        IsTruthyTests.prototype.withUndefined = function () {
            this.throws(function () {
                this.isTruthy(undefined);
            });
        };
        return IsTruthyTests;
    })(tsUnit.TestClass);

    var IsFalseyTests = (function (_super) {
        __extends(IsFalseyTests, _super);
        function IsFalseyTests() {
            _super.apply(this, arguments);
        }
        IsFalseyTests.prototype.withBoolFalse = function () {
            this.isFalsey(false);
        };

        IsFalseyTests.prototype.withEmptyString = function () {
            this.isFalsey('');
        };

        IsFalseyTests.prototype.withZero = function () {
            this.isFalsey(0);
        };

        IsFalseyTests.prototype.withNull = function () {
            this.isFalsey(null);
        };

        IsFalseyTests.prototype.withUndefined = function () {
            this.isFalsey(undefined);
        };

        IsFalseyTests.prototype.withBoolTrue = function () {
            this.throws(function () {
                this.isFalsey(true);
            });
        };

        IsFalseyTests.prototype.withNonEmptyString = function () {
            this.throws(function () {
                this.isFalsey('Hello');
            });
        };

        IsFalseyTests.prototype.withTrueString = function () {
            this.throws(function () {
                this.isFalsey('True');
            });
        };

        IsFalseyTests.prototype.with1 = function () {
            this.throws(function () {
                this.isFalsey(1);
            });
        };
        return IsFalseyTests;
    })(tsUnit.TestClass);

    var FailTests = (function (_super) {
        __extends(FailTests, _super);
        function FailTests() {
            _super.apply(this, arguments);
        }
        FailTests.prototype.expectFails = function () {
            this.throws(function () {
                this.fail();
            });
        };
        return FailTests;
    })(tsUnit.TestClass);
})(Tests || (Tests = {}));
