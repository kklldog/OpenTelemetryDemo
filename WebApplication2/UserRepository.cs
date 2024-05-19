using Npgsql;
using System;

public class UserRepository
{
    private readonly string _connectionString = "Host=127.0.0.1;Username=postgres;Password=123456";

    public async Task<User> GetUserAsync(string username, string password)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM t_users WHERE username = @username AND password = @password", conn);
        cmd.Parameters.AddWithValue("username", username);
        cmd.Parameters.AddWithValue("password", password);

        using var reader = await cmd.ExecuteReaderAsync();
        if (reader.Read())
        {
            return new User
            {
                Id = reader.GetString(0),
                Username = reader.GetString(1),
                Password = reader.GetString(2),
                // 其他字段...
            };
        }

        return null;
    }
}

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    // 其他字段...
}