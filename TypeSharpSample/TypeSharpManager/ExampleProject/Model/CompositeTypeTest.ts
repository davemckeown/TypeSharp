/// <reference path="ExampleProject.Model.d.ts" />

module ExampleProject.Model {

	export class CompositeTypeTest {
	
		User: IUser;
	
		Repository: ExampleProject2.Repository.DataRepository;
	
		ComplexMethod(item: string): ExampleProject.Model.Data.IDataItem {
			return null;
		}
	
	}
	
}