/// <reference path="tsUnit.ts" />
/// <reference path="Tests.ts" />
/// <reference path="BadTests.ts" />
window.onload = function () {
    var test = new tsUnit.Test();
    Tests.Composer.compose(test);
    BadTests.Composer.compose(test);
    test.showResults(document.getElementById('result'), test.run());
};
//@ sourceMappingURL=TestRunner.js.map
