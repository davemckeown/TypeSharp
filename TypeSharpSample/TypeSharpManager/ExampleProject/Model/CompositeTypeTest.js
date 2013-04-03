var ExampleProject;
(function (ExampleProject) {
    /// <reference path="ExampleProject.Model.d.ts" />
    (function (Model) {
        var CompositeTypeTest = (function () {
            function CompositeTypeTest() { }
            CompositeTypeTest.prototype.ComplexMethod = function (item) {
                return null;
            };
            return CompositeTypeTest;
        })();
        Model.CompositeTypeTest = CompositeTypeTest;        
    })(ExampleProject.Model || (ExampleProject.Model = {}));
    var Model = ExampleProject.Model;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=CompositeTypeTest.js.map
