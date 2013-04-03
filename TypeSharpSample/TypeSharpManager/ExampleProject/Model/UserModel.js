var ExampleProject;
(function (ExampleProject) {
    /// <reference path="ExampleProject.Model.d.ts" />
    (function (Model) {
        var UserModel = (function () {
            function UserModel() { }
            UserModel.prototype.CombineArguments = function (argument1, argument2) {
                return 0;
            };
            return UserModel;
        })();
        Model.UserModel = UserModel;        
    })(ExampleProject.Model || (ExampleProject.Model = {}));
    var Model = ExampleProject.Model;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=UserModel.js.map
