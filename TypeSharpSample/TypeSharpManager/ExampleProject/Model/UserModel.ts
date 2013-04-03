/// <reference path="ExampleProject.Model.d.ts" />

module ExampleProject.Model {

	export class UserModel {
	
		Name: string;
	
		LastLogin: Date;
	
		User: IUser;
	
		Organization: string;
	
		CombineArguments(argument1: string, argument2: number): number {
			return 0;
		}
	
	}
	
}