Feature: ShoppingBasketItems

Background:
	Given the following shopping basket items:
	| Id | Value | Description    | ProductType | IsDiscountable |
	| 1  | 10.50 | Cheap Hat      | Headwear    | true           |
	| 2  | 54.65 | Awesome Jumper | Jumper      | true           |
	| 3  | 25.00 | Thermal Hat    | Headwear    | true           |
	| 4  | 26.00 | Woven Jumper   | Jumper      | true           |
	| 5  | 3.50  | Head Light     | Headgear    | true           |
	| 6  | 30.00 | Gift Voucher   | GiftVoucher | false          |
	And the following gift vouchers:
	| Id      | Value |
	| 000-001 | 5.00  |
	And the following offer vouchers:
	| Id      | Value | Threshold | ProductType |
	| 000-002 | 5.00  | 50        | Headgear    |
	| 000-003 | 5.00  | 5         | Headgear    |

Scenario Outline: Shopping Basket gift voucher
	Given a shopping basket is created
	And <productitems> have been added to the basket
	When <vouchers> are applied
	Then the total should be <total>

	Examples: 
	| productitems | vouchers | total |
	| 1, 2         | 000-001  | 60.15 |
	| 3, 4, 5      | 000-002  | 51.00 |

Scenario Outline: Shopping Basket gift voucher rejections
	Given a shopping basket is created
	And <productitems> have been added to the basket
	When <vouchers> are applied
	Then the total should be <total>
	And the basket should have message '<message>'

	Examples: 
	| productitems | vouchers         | message                                                             | total |
	| 3, 4         | 000-002          | There are no products in your basket applicable to voucher 000-002. | 51.00 |
	| 3, 4         | 000-002, 000-003 | Another offer voucher has already been applied.                     | 51.00 |