Feature: Login functionality for Agents

    
    Scenario Outline: Logging in with valid credentials as an Agent
        Given I am on the login page as an agent
        When I enter "<email>" as my Email for agent
        And I enter "<password>" as my Password for agent
        And I select "<role>" from the dropdown as an agent
        And I click on the login button as agent
        Then I should see a dashboard displayed for agent

        Examples:
          | email               | password | role  |
          | Sara@gmail.com      | sara123  | agent |
          | valllle25@gmail.com | hej123   | agent |