Feature: Order Cancellation Functionality

  Background:
    Given I am on the products page

@CancelOrder
  Scenario: Successfully cancel an order
    When I add a backpack and a bike light to the cart
    And I navigate to the cart
    And I proceed to checkout
    And I enter checkout information with first name "Sumanta", last name "Swain", and zip code "500081"
    And I cancel the order
    Then I would be on the product page
