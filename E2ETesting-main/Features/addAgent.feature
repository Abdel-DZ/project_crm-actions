@addAgent
Feature: Add Agent with Validation

    Scenario Outline: Add an agent with validation
        Given I am on the Agents List page
        When I enter "<firstName>" as the First Name
        And I enter "<lastName>" as the Last Name
        And I enter "<email>" as the Email
        And I enter "<password>" as the Password
        And I click on the Add button
        Then I should see the new agent in the agents list


        Examples:
          | firstName | lastName  | email           | password   |
          | Ann       | Kimo      | ann@example.com | ann123     |
          | Samiro    | Klascho   | samiro@test.com | samiro123  |