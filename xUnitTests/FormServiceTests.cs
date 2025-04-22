using NSubstitute;
using Xunit;
using Npgsql;
using server;

public class FormServiceTests
{
    [Fact]
    public async Task AddForm_ShouldReturnFalse_WhenFieldsAreEmpty()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var service = new FormService(fakeDb);

        // Act
        var result = await service.AddForm("", "product", "msg", Guid.NewGuid());

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddForm_ShouldReturnTrue_WhenFieldsAreValid()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var fakeCmd = Substitute.For<NpgsqlCommand>();
        fakeDb.CreateCommand(Arg.Any<string>()).Returns(fakeCmd);
        
        var service = new FormService(fakeDb);

        // Act
        var result = await service.AddForm("test@test.com", "product", "msg", Guid.NewGuid());

        // Assert
        Assert.True(result);
    }
}
