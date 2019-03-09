Feature: ShoppingBasket

Scenario: Shopping Basket Creation
	When creating a shopping basket
	Then an id should be returned

Scenario: Shopping Basket Deletion
	Given a shopping basket is created
	When deleting the shopping basket
	Then should return successfully

Scenario: Shopping Basket Deletion without creation
	When deleting a shopping basket with id "foo"
	Then should return successfully