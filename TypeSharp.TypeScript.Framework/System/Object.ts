/// <reference path="Type.ts"/>
/// <reference path="Action.ts"/>
/// <reference path="Enumerations.ts"/>

// Module
module System {

    // Class
    export class Object {

        _context: Object;

        // Constructor
        constructor() {
            this._context = this;
        }

        // Instance member
        Equals(obj: Object): boolean;

        Equals(obj1: Object, obj2?: Object): boolean {

            if (typeof obj2 === 'undefined') {
                return this.ReferenceEquals(this._context, obj1);
            }

            return obj1.Equals(this);
        }

        Finalize(): void {
            return;
        }

        GetHashCode(): number {

            return 0;

        }

        GetType(): Type {
            return new Type();
        }

        MemberwiseClone(): Object {
            return new Object();
        }

        ReferenceEquals(obj1: Object, obj2: Object): boolean {
            return obj1 === obj2;
        }

        ToString(): string {

            var test = DateTimeKind.Local;

            test = DateTimeKind.Utc;

            return "System.Object";
        }


    }

}
