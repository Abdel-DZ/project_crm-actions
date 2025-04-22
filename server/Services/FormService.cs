using System;
using System.Threading.Tasks;
using Npgsql;

public class FormService
{
    private readonly NpgsqlDataSource _db;

    // Constructor to inject NpgsqlDataSource dependency
    public FormService(NpgsqlDataSource db)
    {
        _db = db;
    }

    // AddForm logic (with input validation)
    public async Task<bool> AddForm(string email, string service_product, string message, Guid token)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(service_product) || string.IsNullOrWhiteSpace(message))
        {
            return false; // If any field is empty, return false
        }

        // Using NpgsqlCommand to execute an SQL insert
        var query = "INSERT INTO FormData (Email, ServiceProduct, Message, Token) VALUES (@Email, @ServiceProduct, @Message, @Token)";

        // Execute the query asynchronously
        using (var command = _db.CreateCommand(query))
        {
            command.Parameters.AddWithValue("Email", email);
            command.Parameters.AddWithValue("ServiceProduct", service_product);
            command.Parameters.AddWithValue("Message", message);
            command.Parameters.AddWithValue("Token", token);

            await command.ExecuteNonQueryAsync();
        }

        return true; // Return true if the form was "added" successfully
    }
}
