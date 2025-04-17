@stateUpdate
Feature: State Update for Agents List

    Scenario Outline: Ensure state updates after add, edit, or delete
        Given I am on the Agents List page
        When I add an agent with the name "<firstName>" and email "<email>"
        Then the agent list should include an agent with the name "<firstName>" and email "<email>"

        Examples:
          | firstName | email            |
          | John      | john@example.com |
          | Jane      | jane@example.com |