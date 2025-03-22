Feature: Add and Remove Products from Cart

  Background:
    Given I am on the products page

@AddToCart
  Scenario: User adds products to the cart
    When I add the backpack and bike light to the cart
    Then the cart should contain the backpack and bike light

@RemoveFromCart
  Scenario: User removes products from the cart
    When I remove the backpack and bike light from the cart
    Then the cart should be empty
