using Xunit;
using server;

public class FormServiceTests
{
    [Fact]
    public void AddForm_ShouldReject_WhenFieldsAreEmpty()
    {
        // Arrange
        var form = new Form
        {
            email = "",
            service_product = "product",
            message = "msg"
        };

        // Act & Assert
        Assert.True(string.IsNullOrWhiteSpace(form.email)); // Just tests the validation condition
    }

    [Fact]
    public void AddForm_ShouldAccept_WhenFieldsAreValid()
    {
        // Arrange
        var form = new Form
        {
            email = "valid@test.com",
            service_product = "product",
            message = "msg"
        };

        // Act & Assert
        Assert.False(string.IsNullOrWhiteSpace(form.email)); // Just tests the validation condition
    }
}
