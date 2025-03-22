Feature: Order Place Functionality

  Background:
    Given I am on the productpage

 @OrderPlaced
  Scenario: Successfully place an order
    Given I have added products to the cart
    When I proceed to checkout
    And I enter my checkout information
    And I complete the order
    Then I would see the order confirmation page "Thank you for your order!"

