/// <reference path="BusinessObjects.d.ts" />

module BusinessObjects {

	/**
	* Testing Comment
	*/
	export class Order {
	
		Products: Product[];
	
		/**
		* Add products to the order
		*/
		AddProducts(products: Product[], group: string): void {
			
		}
	
	}
	
}