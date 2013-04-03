var ExampleProject;
(function (ExampleProject) {
    (function (Model) {
        /// <reference path="ExampleProject.Model.Data.d.ts" />
        (function (Data) {
            var DataRepository = (function () {
                function DataRepository() { }
                DataRepository.prototype.DataRepeat = function (numb) {
                    return "";
                };
                return DataRepository;
            })();
            Data.DataRepository = DataRepository;            
        })(Model.Data || (Model.Data = {}));
        var Data = Model.Data;
    })(ExampleProject.Model || (ExampleProject.Model = {}));
    var Model = ExampleProject.Model;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=DataRepository.js.map
