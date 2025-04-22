using NSubstitute;
using Xunit;
using Npgsql;
using server;  // Assuming your FormService is in the 'server' namespace

public class FormServiceTests
{
    [Fact]
    public async Task AddForm_ShouldReject_WhenFieldsAreEmpty()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var service = new FormService(fakeDb);  // Use FormService instead of Program

        // Act
        var result = await service.AddForm("", "product", "msg", Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddForm_ShouldAccept_WhenFieldsAreValid()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var fakeCmd = Substitute.For<NpgsqlCommand>();
        fakeDb.CreateCommand(Arg.Any<string>()).Returns(fakeCmd);
        var service = new FormService(fakeDb);  // Use FormService instead of Program

        // Act
        var result = await service.AddForm("valid@email.com", "valid-product", "valid-msg", Guid.NewGuid());

        // Assert
        Assert.True(result);
        await fakeCmd.Received().ExecuteNonQueryAsync();
    }
}
