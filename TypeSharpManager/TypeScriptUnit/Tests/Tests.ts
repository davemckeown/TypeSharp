/// <reference path="../tsUnit.ts" />

module Tests {
    export class Composer {
        static compose(test: tsUnit.Test) {
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
        }
    }

    class RealClass {
        public name = 'Real';
        run() {
            return false;
        }
        returnValue(): RealClass {
            return this;
        }
    }

    class SetUpTests extends tsUnit.TestClass {
        private testProperty: string;

        setUp() {
            this.testProperty = 'SETUP';
        }

        testAfterInitialSetUp() {
            this.areIdentical('SETUP', this.testProperty);
            this.testProperty = 'OVERWRITE';
        }

        testAfterInitialOverwritten() {
            this.areIdentical('SETUP', this.testProperty);
            this.testProperty = 'OVERWRITE';
        }
    }


    class TearDownTests extends tsUnit.TestClass {
        private testProperty: string = '';

        tearDown() {
            this.testProperty = 'TEARDOWN';
        }

        testAfterInitialSetUp() {
            this.areIdentical('', this.testProperty);
            this.testProperty = 'OVERWRITE';
        }

        testAfterInitialOverwritten() {
            this.areIdentical('TEARDOWN', this.testProperty);
            this.testProperty = 'OVERWRITE';
        }
    }


    class FakesTests extends tsUnit.TestClass {

        callDefaultFunctionOnFake() {
            var fakeObject = new tsUnit.Fake(new RealClass());
            var target = <RealClass> fakeObject.create();

            var result = target.run();

            this.areIdentical(undefined, result);
        }

        callSubstituteFunctionOnFake() {
            var fakeObject = new tsUnit.Fake(new RealClass());
            fakeObject.addFunction('run', function () { return true; });
            var target = <RealClass> fakeObject.create();

            var result = target.run();

            this.isTrue(result);
        }

        callSubstituteFunctionToObtainSecondFake() {
            var innerFake = new tsUnit.Fake(new RealClass());
            innerFake.addFunction('run', function () { return true; });
            var outerFake = new tsUnit.Fake(new RealClass());
            outerFake.addFunction('returnValue', function () { return <RealClass> innerFake.create(); });
            var target = <RealClass> outerFake.create();

            var interimResult = target.returnValue();
            var result = interimResult.run();

            this.isTrue(result);
        }

        callDefaultPropertyOnFake() {
            var fakeObject = new tsUnit.Fake(new RealClass());
            var target = <RealClass> fakeObject.create();

            var result = target.name;

            this.areIdentical(null, result);
        }

        callSubstitutePropertyOnFake() {
            var fakeObject = new tsUnit.Fake(new RealClass());
            fakeObject.addProperty('name', 'Test');
            var target = <RealClass> fakeObject.create();

            var result = target.name;

            this.areIdentical('Test', result);
        }
    }

    class AssertAreIdenticalTests extends tsUnit.TestClass {
        withIdenticalNumbers() {
            this.areIdentical(5, 5);
        }

        withDifferentNumbers() {
            this.throws(function () {
                this.areIdentical(5, 4);
            });
        }

        withIdenticalStings() {
            this.areIdentical('Hello', 'Hello');
        }

        withDifferentStrings() {
            this.throws(function () {
                this.areIdentical('Hello', 'Jello');
            });
        }

        withSameInstance() {
            var x = { test: 'Object' };
            var y = x;
            this.areIdentical(x, y);
        }

        withDifferentInstance() {
            this.throws(function () {
                var x = { test: 'Object' };
                var y = { test: 'Object' };
                this.areIdentical(x, y);
            });
        }

        withDifferentTypes() {
            this.throws(function () {
                this.areIdentical('1', 1);
            });
        }
    }

    class AssertAreNotIdenticalTests extends tsUnit.TestClass {
        withIdenticalNumbers() {
            this.throws(function () {
                this.areNotIdentical(4, 4);
            });
        }

        withDifferentNumbers() {
            this.areNotIdentical(4, -4);
        }

        withIdenticalStrings() {
            this.throws(function () {
                this.areNotIdentical('Hello', 'Hello');
            });
        }

        withDifferentStrings() {
            this.areNotIdentical('Hello', 'Hella');
        }

        withSameInstance() {
            this.throws(function () {
                var x = { test: 'Object' };
                var y = x;
                this.areNotIdentical(x, y);
            });
        }

        withDifferentInstance() {
            var x = { test: 'Object' };
            var y = { test: 'Object' };
            this.areNotIdentical(x, y);
        }

        withDifferentTypes() {
            this.areNotIdentical('1', 1);
        }
    }

    class IsTrueTests extends tsUnit.TestClass {
        withBoolTrue() {
            this.isTrue(true);
        }

        withBoolFalse() {
            this.throws(function () {
                this.isTrue(false);
            });
        }
    }

    class IsFalseTests extends tsUnit.TestClass {
        withBoolFalse() {
            this.isFalse(false);
        }

        withBoolTrue() {
            this.throws(function () {
                this.isFalse(true);
            });
        }
    }

    class IsTruthyTests extends tsUnit.TestClass {
        withBoolTrue() {
            this.isTruthy(true);
        }

        withNonEmptyString() {
            this.isTruthy('Hello');
        }

        withTrueString() {
            this.isTruthy('True');
        }

        with1() {
            this.isTruthy(1);
        }

        withBoolFalse() {
            this.throws(function () {
                this.isTruthy(false);
            });
        }

        withEmptyString() {
            this.throws(function () {
                this.isTruthy('');
            });
        }

        withZero() {
            this.throws(function () {
                this.isTruthy(0);
            });
        }

        withNull() {
            this.throws(function () {
                this.isTruthy(null);
            });
        }

        withUndefined() {
            this.throws(function () {
                this.isTruthy(undefined);
            });
        }
    }

    class IsFalseyTests extends tsUnit.TestClass {
        withBoolFalse() {
            this.isFalsey(false);
        }

        withEmptyString() {
            this.isFalsey('');
        }

        withZero() {
            this.isFalsey(0);
        }

        withNull() {
            this.isFalsey(null);
        }

        withUndefined() {
            this.isFalsey(undefined);
        }

        withBoolTrue() {
            this.throws(function () {
                this.isFalsey(true);
            });
        }

        withNonEmptyString() {
            this.throws(function () {
                this.isFalsey('Hello');
            });
        }

        withTrueString() {
            this.throws(function () {
                this.isFalsey('True');
            });
        }

        with1() {
            this.throws(function () {
                this.isFalsey(1);
            });
        }
    }

    class FailTests extends tsUnit.TestClass {
        expectFails() {
            this.throws(function () {
                this.fail();
            });
        }
    }
}