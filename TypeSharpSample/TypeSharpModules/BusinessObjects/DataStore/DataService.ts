/// <reference path="BusinessObjects.DataStore.d.ts" />

module BusinessObjects.DataStore {

	/**
	* @classdesc DataServices Class
	*/
	/**
	* @classdesc BusinessObjects.DataStore
	*/
	export class DataService {
	
		/**
		* Gets or sets the connection string
		*/
		ConnectionString: string;
	
		/**
		* Load the Products
		* @return All Products
		*/
		LoadProducts(): BusinessObjects.Product[] {
			/** @todo Implement LoadProducts */
			return null;
		}
	
		/**
		* Load the Customers
		* @return Loads all Customers
		*/
		LoadCustomers(): BusinessObjects.Customer[] {
			/** @todo Implement LoadCustomers */
			return null;
		}
	
		/**
		* Load all Orders
		* @return Loads all Orders
		*/
		LoadOrders(): BusinessObjects.Order[] {
			/** @todo Implement LoadOrders */
			return null;
		}
	
	}
	
}