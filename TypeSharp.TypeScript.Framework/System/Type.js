// Module
var System;
(function (System) {
    // Class
    var Type = (function () {
        // Constructor
        function Type() {
        }
        Object.defineProperty(Type.prototype, "Assembly", {
            get: function () {
                return "Assembly";
            },
            enumerable: true,
            configurable: true
        });
        Object.defineProperty(Type.prototype, "AssemblyQualifiedName", {
            get: function () {
                return "AssemblyQualifiedName";
            },
            enumerable: true,
            configurable: true
        });
        return Type;
    })();
    System.Type = Type;
})(System || (System = {}));
//# sourceMappingURL=Type.js.map
