/// <reference path="tsUnit.ts" />
/// <reference path="Tests/Tests.ts" />
/// <reference path="Tests/BadTests.ts" />

window.onload = function () {
    var test = new tsUnit.Test();
    Tests.Composer.compose(test);
    BadTests.Composer.compose(test);
    test.showResults(document.getElementById('result'), test.run());
};