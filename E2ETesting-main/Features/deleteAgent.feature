@deleteAgent
Feature: Delete Agent

    Scenario Outline: Delete an agent
        Given I am on the Agents List page
        When I click the Delete button for an agent with ID "<agentId>"
        Then I should no longer see the agent with ID "<agentId>" in the list

        Examples:
          | agentId |
          | 13       |
          | 39       |
       
