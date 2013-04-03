interface IUser {
	Name: string;
	Organization: string;
	TestMethod(argument1: string, argument2: string): string;
}

interface IDataItem {
	DataField: string;
	DataValue: any;
}

class DataRepository {
	DataContext: string;
	DataRepeat(numb: number): string {
		return "";
	}
}

class CompositeTypeTest {
	User: IUser;
	Repository: DataRepository;
	ComplexMethod(item: string): IDataItem {
		return null;
	}
}

class UserModel {
	Name: string;
	LastLogin: Date;
	User: IUser;
	Organization: string;
	TestMethod(argument1: string, argument2: number): number {
		return 0;
	}
}

