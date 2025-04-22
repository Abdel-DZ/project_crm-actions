using System;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using Npgsql;

public class FormServiceTests
{
    [Fact]
    public async Task AddForm_ShouldReturnFalse_WhenFieldsAreEmpty()
    {
        var fakeDb = Substitute.For<NpgsqlDataSource>();
        var service = new FormService(fakeDb);

        var result = await service.AddForm("", "product", "msg", Guid.NewGuid());

        Assert.False(result);
    }
}
