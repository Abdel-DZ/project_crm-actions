@changePassword
Feature: Change Password with Validation and API Integration

    Scenario Outline: Change user password
        Given I am on the Change Password page
        When I enter "<email>" as the Email
        And I enter "<newPassword>" as the New Password
        And I click on the Change Password button
        Then I should see the appropriate message: "<message>"

        Examples:
          | email           | newPassword | message                    |
          | Sara@gmail.com  | newPass123  | You changed your password! | 
          | samira@test.com | newPass456  | You changed your password! |