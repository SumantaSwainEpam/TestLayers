Feature: LogInPage Functionality

  Background:
    Given I am on the login page

  @DifferentUser
  Scenario Outline: Login with different users
    When I enter "<username>" as Username and "<password>" as Password
    And I click the login button
    Then I should <expectedOutcome>

    Examples: 
      | username        | password     | expectedOutcome |
      | standard_user   | secret_sauce | Successful      |
      | locked_out_user | secret_sauce | Unsuccessful    |
      | false_user      | secret_sauce | Unsuccessful    |
