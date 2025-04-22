using System;
using System.Threading.Tasks;
using Npgsql;

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

        try
        {
            using var conn = await _db.OpenConnectionAsync();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO forms (email, service_product, message, token)
                VALUES (@e, @s, @m, @t)";
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@s", service_product);
            cmd.Parameters.AddWithValue("@m", message);
            cmd.Parameters.AddWithValue("@t", token);
            await cmd.ExecuteNonQueryAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }
}
