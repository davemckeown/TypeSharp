var ExampleProject2;
(function (ExampleProject2) {
    /// <reference path="ExampleProject2.Repository.d.ts" />
    (function (Repository) {
        var UserRepository = (function () {
            function UserRepository() { }
            return UserRepository;
        })();
        Repository.UserRepository = UserRepository;        
    })(ExampleProject2.Repository || (ExampleProject2.Repository = {}));
    var Repository = ExampleProject2.Repository;
})(ExampleProject2 || (ExampleProject2 = {}));
//@ sourceMappingURL=UserRepository.js.map
