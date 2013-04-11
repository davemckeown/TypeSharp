/// <reference path="BusinessObjects.d.ts" />

module BusinessObjects {

	/**
	* Testing Comment
	*/
	export class Order {
	
		/**
		* Gets or sets the products list
		*/
		Products: Product[];
	
		/**
		* Add products to the order
		* @param products The products to add
		* @param group The group name
		*/
		AddProducts(products: Product[], group: string): void {
			/** @todo Implement AddProducts */
			return;
		}
	
	}
	
}