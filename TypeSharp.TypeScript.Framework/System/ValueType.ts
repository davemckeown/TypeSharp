// Module
module System {

    // Class
    export class ValueType extends Object {
    
        // Instance member
        Equals(obj: Object): boolean;

        Equals(obj1: Object, obj2?: Object): boolean {

            if (typeof obj2 === 'undefined') {
                return this.ReferenceEquals(this._context, obj1);
            }

            return obj1.Equals(this);
        }

        GetHashCode(): number {
            return 1;
        }

        ToString(): string {
            return "System.ValueType";
        }
    }

}