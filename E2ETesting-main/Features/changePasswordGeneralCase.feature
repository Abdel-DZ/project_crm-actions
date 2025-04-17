@changePasswordGeneralCase
Feature: Change Password General Case Flow

    Scenario Outline: Change user password
        Given I am on the Change Password page
        When I enter "<email>" as the Email
        And I enter "<newPassword>" as the New Password
        And I click on the Change Password button
        Then I should see the appropriate message: "<message>"

        Examples:
          | email               | newPassword | message                                       |
          | bob@update.com      | newPass123  | You changed your password!                    |
          | invalid@example.com | newPass123  | Fail                                          |
          | bob@update.com      |             | You need to enter email and your new password |
          |                     | newPass123  | You need to enter email and your new password |

    Scenario: Change password with empty fields
        Given I am on the Change Password page
        When I click on the Change Password button without entering any details
        Then I should see the message "You need to enter email and your new password"

    Scenario: Handle API failure on password change
        Given I am on the Change Password page
        When I enter "user@example.com" as the Email
        And I enter "newPass123" as the New Password
        And I click on the Change Password button
        Then I should see the message "Fail"

    Scenario: Verify particle animation and background
        Given I am on the Change Password page
        Then I should see the animated particles background correctly rendered