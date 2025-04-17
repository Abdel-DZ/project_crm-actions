Feature: Login functionality for Admin

    Scenario Outline: Logging in with valid credentials as an Admin
        Given I am on the login page as an admin
        When I enter "<email>" as my Email for admin
        And I enter "<password>" as my Password for admin
        And I select "<role>" from the dropdown as an admin
        And I click on the login button as admin
        Then I should see a dashboard displayed for admin

        Examples:
          | email            | password | role  |
          | admin1@gmail.com | 12345678 | admin |
          | admin2@gmail.com | 87654321 | admin |