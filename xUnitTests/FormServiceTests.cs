using NSubstitute;
using Xunit;
using server; 

public class FormServiceTests
{
    [Fact]
    public async Task AddForm_ShouldReject_WhenFieldsAreEmpty()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        
        // Act (testing Program.AddForm directly)
        await Program.AddForm("", "product", "msg", Guid.NewGuid(), fakeDb);
        
        // Assert (verify database wasn't called)
        await fakeDb.DidNotReceive().CreateCommand(Arg.Any<string>());
    }

    [Fact]
    public async Task AddForm_ShouldAccept_WhenFieldsAreValid()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var fakeCmd = Substitute.For<NpgsqlCommand>();
        fakeDb.CreateCommand(Arg.Any<string>()).Returns(fakeCmd);
        
        // Act
        await Program.AddForm("valid@email.com", "valid-product", "valid-msg", Guid.NewGuid(), fakeDb);
        
        // Assert (verify database was called)
        await fakeCmd.Received().ExecuteNonQueryAsync();
    }
}
