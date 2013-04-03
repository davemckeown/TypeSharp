/// <reference path="../tsUnit.ts" />

module BadTests {
    export class Composer {
        static compose(test: tsUnit.Test) {
            test.addTestClass(new DeliberateFailures(), 'DeliberateFailures');
        }
    }

    class DeliberateFailures extends tsUnit.TestClass {
        deliberateBadTest() {
            this.areIdentical('1', 1);
        }
    }
}