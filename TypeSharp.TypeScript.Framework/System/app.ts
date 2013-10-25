/// <reference path="Object.ts"/>

module TestCompileProject {

    export class TestSyntaxGenClass<T> {

        public publicField: string;
        private privateField: string;

        private _NumberProp: number;
        private _GenericProp: T;
        private _StringProp: string;

        get NumberProp(): number { return this._NumberProp; }

        set NumberProp(value: number) { this._NumberProp = value; }

        get GenericProp(): T { return this._GenericProp; }

        set GenericProp(value: T) { this._GenericProp = value; }

        get StringProp(): string { return this._StringProp; }

        /**
        * Void method as an example of a method accepting generics
        * @param key The key
        * @param value The value
        */
        VoidMethod<TKey, TValue>(key: TKey, value: TValue): void {
            /** @todo Implement VoidMethod */
            return;
        }

    }

}

var testing = new TestCompileProject.TestSyntaxGenClass<string>();

testing.GenericProp = "hello";

var testing2 = new TestCompileProject.TestSyntaxGenClass<System.Object>();

testing2.GenericProp = new System.Object();

testing.GenericProp = testing2.GenericProp.ToString();

testing.VoidMethod("hello", 123);