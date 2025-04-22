using NSubstitute;
using Xunit;
using Npgsql;
using System.Reflection;
using server;

public class FormServiceTests
{
    private static async Task InvokeAddForm(string email, string service_product, string message, Guid token)
    {
        var method = typeof(Program).GetMethod("AddForm", BindingFlags.NonPublic | BindingFlags.Static);
        await (Task)method.Invoke(null, new object[] { email, service_product, message, token });
    }

    [Fact]
    public async Task AddForm_ShouldReject_WhenFieldsAreEmpty()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var fakeDatabase = Substitute.For<Database>();
        fakeDatabase.Connection().Returns(fakeDb);

        // Replace the Database instance in Program.cs via reflection
        var field = typeof(Program).GetField("_database", BindingFlags.NonPublic | BindingFlags.Static);
        field.SetValue(null, fakeDatabase);

        // Act
        await InvokeAddForm("", "product", "msg", Guid.NewGuid());

        // Assert
        fakeDb.DidNotReceive().CreateCommand(Arg.Any<string>());
    }

    [Fact]
    public async Task AddForm_ShouldAccept_WhenFieldsAreValid()
    {
        // Arrange
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var fakeCmd = Substitute.For<NpgsqlCommand>();
        fakeDb.CreateCommand(Arg.Any<string>()).Returns(fakeCmd);
        
        var fakeDatabase = Substitute.For<Database>();
        fakeDatabase.Connection().Returns(fakeDb);

        // Replace the Database instance in Program.cs via reflection
        var field = typeof(Program).GetField("_database", BindingFlags.NonPublic | BindingFlags.Static);
        field.SetValue(null, fakeDatabase);

        // Act
        await InvokeAddForm("valid@email.com", "valid-product", "valid-msg", Guid.NewGuid());

        // Assert
        await fakeCmd.Received(1).ExecuteNonQueryAsync();
    }
}
