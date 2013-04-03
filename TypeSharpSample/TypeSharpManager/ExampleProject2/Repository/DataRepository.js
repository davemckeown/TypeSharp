var ExampleProject2;
(function (ExampleProject2) {
    /// <reference path="ExampleProject2.Repository.d.ts" />
    (function (Repository) {
        var DataRepository = (function () {
            function DataRepository() { }
            return DataRepository;
        })();
        Repository.DataRepository = DataRepository;        
    })(ExampleProject2.Repository || (ExampleProject2.Repository = {}));
    var Repository = ExampleProject2.Repository;
})(ExampleProject2 || (ExampleProject2 = {}));
//@ sourceMappingURL=DataRepository.js.map
