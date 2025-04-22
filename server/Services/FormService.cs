using System;
using System.Threading.Tasks;

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

        // Assuming you would insert data here, for example:
        await _db.InsertFormAsync(email, service_product, message, token);

        return true; // Return true if the form was "added" successfully
    }
}
