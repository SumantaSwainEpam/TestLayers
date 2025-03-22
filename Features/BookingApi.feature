Feature: Booking Api Tests

@POST
  Scenario: Create a new booking
    Given I have the booking details
    When I create a new booking
    Then the booking should be created successfully
@GET
  Scenario: Get booking details by ID
    Given I have an existing booking ID
    When I retrieve the booking details
    Then the booking details should be returned successfully
@PUT
  Scenario: Update an existing booking
    Given I have updated booking details
    When I update the booking
    Then the booking should be updated successfully
@PATCH
  Scenario: Partially update an existing booking
    Given I have updated booking details for a specific field
    When I partially update the booking
    Then the booking should be updated successfully with the new details
@DELETE
  Scenario: Delete an existing booking
    Given I have an existing booking ID
    When I delete the booking
    Then the booking should be deleted successfully
