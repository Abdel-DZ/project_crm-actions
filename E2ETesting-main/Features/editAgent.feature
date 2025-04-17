@editAgent  
Feature: Edit Agent Details

    Scenario Outline: Edit an agent's information
        Given I am on the Agents List page
        When I click Edit for agent with ID "<agentId>"
        And I update first name to "<newFirstName>"
        And I update last name to "<newLastName>"
        And I update email to "<newEmail>"
        And I click Save
        Then I should see first name "<newFirstName>" in the agents list
        And I should see email "<newEmail>" in the agents list

        Examples:
          | agentId | newFirstName | newLastName | newEmail        |
          | 1       | Anna         | Kimov       | anna.k@mail.com |