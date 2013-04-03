/// <reference path="ExampleProject.Model.d.ts" />

module ExampleProject.Model {

	export interface IUser {
	
		Name: string;
	
		Organization: string;
	
		CombineArguments(argument1: string, argument2: number): number;
	
	}
	
}