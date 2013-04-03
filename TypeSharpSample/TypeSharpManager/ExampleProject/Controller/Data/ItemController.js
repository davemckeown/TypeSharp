var ExampleProject;
(function (ExampleProject) {
    (function (Controller) {
        /// <reference path="ExampleProject.Controller.Data.d.ts" />
        (function (Data) {
            var ItemController = (function () {
                function ItemController() { }
                return ItemController;
            })();
            Data.ItemController = ItemController;            
        })(Controller.Data || (Controller.Data = {}));
        var Data = Controller.Data;
    })(ExampleProject.Controller || (ExampleProject.Controller = {}));
    var Controller = ExampleProject.Controller;
})(ExampleProject || (ExampleProject = {}));
//@ sourceMappingURL=ItemController.js.map
