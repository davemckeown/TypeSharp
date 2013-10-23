var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
// Module
var System;
(function (System) {
    // Class
    var ValueType = (function (_super) {
        __extends(ValueType, _super);
        function ValueType() {
            _super.apply(this, arguments);
        }
        ValueType.prototype.Equals = function (obj1, obj2) {
            if (typeof obj2 === 'undefined') {
                return this.ReferenceEquals(this._context, obj1);
            }

            return obj1.Equals(this);
        };

        ValueType.prototype.GetHashCode = function () {
            return 1;
        };

        ValueType.prototype.ToString = function () {
            return "System.ValueType";
        };
        return ValueType;
    })(System.Object);
    System.ValueType = ValueType;
})(System || (System = {}));
//# sourceMappingURL=ValueType.js.map
