using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

public class FormServiceTests
{
    [Fact]
    public async Task AddForm_ShouldReturnFalse_WhenFieldsAreEmpty()
    {
        // Arrange: mock the NpgsqlDataSource dependency
        var fakeDb = Substitute.For<NpgsqlDataSource>();  // Mock the NpgsqlDataSource dependency
        var service = new FormService(fakeDb);  // Pass the mocked db into FormService

        // Act: test AddForm with empty email
        var result = await service.AddForm("", "product", "msg", Guid.NewGuid());

        // Assert: expect false because email is empty
        Assert.False(result);
    }

    [Fact]
    public async Task AddForm_ShouldReturnTrue_WhenFieldsAreValid()
    {
        // Arrange: mock the NpgsqlDataSource dependency
        var fakeDb = Substitute.For<NpgsqlDataSource>();  // Mock the NpgsqlDataSource dependency
        var service = new FormService(fakeDb);  // Pass the mocked db into FormService

        // Act: test AddForm with valid data
        var result = await service.AddForm("email@example.com", "product", "message", Guid.NewGuid());

        // Assert: expect true because fields are valid
        Assert.True(result);
    }
}
