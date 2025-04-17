@chat
Feature: Chat Functionality

    Scenario: Sending a message in the chat
        Given I am on the Chat page with token "demo123"
        When I enter "bob" as the username
        And I type "Hello there!" as the message
        And I click on the Send button
        Then I should see the message "Hello there!" from "bob" in the chat

    Scenario: Username input should be disabled after blur
        Given I am on the Chat page with token "demo123"
        When I enter "alice" as the username
        And I blur the username input
        Then the username input should be disabled

    Scenario: Prevent sending empty messages
        Given I am on the Chat page with token "demo123"
        When I enter "tester" as the username
        And I leave the message empty
        And I click on the Send button
        Then the message should not be sent

    Scenario: Background canvas should render
        Given I am on the Chat page with token "demo123"
        Then I should see the animated background canvas