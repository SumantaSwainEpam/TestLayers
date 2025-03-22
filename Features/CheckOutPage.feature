Feature: Checkout Functionality

  Background:
    
    Given am on the products page

  @CheckOutProducts
  Scenario: User Should proceed to checkout
    When I add the Bolt T-Shirt and Backpack to the cart
    And I navigate to the cart and proceed to checkout
    Then I sucessfully completed the checkout  with the title "Checkout: Your Information"

  
