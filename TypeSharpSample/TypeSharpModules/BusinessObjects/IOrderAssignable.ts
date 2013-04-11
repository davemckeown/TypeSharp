/// <reference path="BusinessObjects.d.ts" />

module BusinessObjects {

	export interface IOrderAssignable {
	
		/**
		* Assigns the Order to assignable
		* @param order The order to assign
		* @return true on success
		*/
		AssignOrder(order: Order): bool;
	
	}
	
}