@formSubmission
Feature: Form submission functionality

    Scenario Outline: Successfully submitting a form with valid inputs
        Given I am on the form page
        When I enter "<email>" as my Email
        And I enter "<product>" as my Product
        And I enter "<message>" as my Message
        And I click on the Send button
        Then I should be redirected to the confirmation page

        Examples:
          | email            | product  | message                 |
          | test@example.com | ProductA | This is a test message. |
          | user@example.com | ProductB | Another test message.   |