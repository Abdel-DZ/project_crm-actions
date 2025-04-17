@formValidation
Feature: Form Validation

    Scenario Outline: Validate that the form does not submit if any fields are empty
        Given I am on the form page
        When I enter "<email>" as my Email
        And I enter "<product>" as my Product
        And I enter "<message>" as my Message
        And I click on the Send button
        Then I should see a validation message indicating that all fields are required

        Examples:
          | email            | product  | message      |
          | ""               | ProductA | Test message |
          | user@example.com | ""       | Test message |
          | user@example.com | ProductA | ""           |
          | ""               | ""       | ""           |