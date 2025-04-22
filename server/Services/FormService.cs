using Npgsql;

namespace server;

public class FormService
{
    private readonly NpgsqlDataSource _db;

    public FormService(NpgsqlDataSource db)
    {
        _db = db;
    }

    public async Task<bool> AddForm(string email, string service_product, string message, Guid token)
    {
        if (string.IsNullOrWhiteSpace(email) || 
            string.IsNullOrWhiteSpace(service_product) || 
            string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        await using var cmd = _db.CreateCommand(
            "INSERT INTO forms (email, service_product, message, token) VALUES (@email, @service_product, @message, @token)");
        
        cmd.Parameters.AddWithValue("@email", email);
        cmd.Parameters.AddWithValue("@service_product", service_product);
        cmd.Parameters.AddWithValue("@message", message);
        cmd.Parameters.AddWithValue("@token", token);

        await cmd.ExecuteNonQueryAsync();
        return true;
    }
}
