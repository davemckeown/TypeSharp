/// <reference path="Type.ts"/>
/// <reference path="Action.ts"/>
/// <reference path="Enumerations.ts"/>
// Module
var System;
(function (System) {
    // Class
    var Object = (function () {
        // Constructor
        function Object() {
            this._context = this;
        }
        Object.prototype.Equals = function (obj1, obj2) {
            if (typeof obj2 === 'undefined') {
                return this.ReferenceEquals(this._context, obj1);
            }

            return obj1.Equals(this);
        };

        Object.prototype.Finalize = function () {
            return;
        };

        Object.prototype.GetHashCode = function () {
            return 0;
        };

        Object.prototype.GetType = function () {
            return new System.Type();
        };

        Object.prototype.MemberwiseClone = function () {
            return new Object();
        };

        Object.prototype.ReferenceEquals = function (obj1, obj2) {
            return obj1 === obj2;
        };

        Object.prototype.ToString = function () {
            var test = System.DateTimeKind.Local;

            test = System.DateTimeKind.Utc;

            return "System.Object";
        };
        return Object;
    })();
    System.Object = Object;
})(System || (System = {}));
//# sourceMappingURL=Object.js.map
