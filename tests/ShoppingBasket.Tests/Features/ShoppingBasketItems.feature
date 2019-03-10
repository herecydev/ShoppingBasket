Feature: ShoppingBasketItems

Background:
	Given the following shopping basket items:
	| Id | Value | Description  |
	| 1  | 10.50 | Hat          |
	| 2  | 54.65 | Jumper       |
	| 3  | 25.00 | Hat          |
	| 4  | 26.00 | Jumper       |
	| 5  | 3.50  | Head Light   |
	| 6  | 30.00 | Gift Voucher |
	And the following gift vouchers:
	| Id      | Value |
	| 000-001 | 5.00  |

Scenario Outline: Shopping Basket gift voucher
	Given a shopping basket is created
	And <productitems> have been added to the basket
	When <voucher> is applied
	Then it should return successfully
	And the total should be <total>

	Examples: 
	| productitems | voucher | total |
	| 1, 2         | 000-001 | 60.15 |