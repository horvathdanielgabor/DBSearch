using MySqlConnector;
using System.Data;

internal class DatabaseServices
{
	private string connectionString;

	public DatabaseServices(string conn)
	{
		connectionString = conn;
	}

	public bool CheckConnection()
	{
		try
		{
			using var c = new MySqlConnection(connectionString);
			c.Open();
			return true;
		}
		catch { return false; }
	}

	public DataTable SearchLike(string value)
	{
		using var c = new MySqlConnection(connectionString);
		c.Open();

		using var cmd = new MySqlCommand(
			@"SELECT * FROM users
              WHERE full_name LIKE @v
                 OR email LIKE @v
                 OR phone_number LIKE @v", c);

		cmd.Parameters.AddWithValue("@v", "%" + value + "%");

		DataTable dt = new DataTable();
		dt.Load(cmd.ExecuteReader());
		return dt;
	}

	public int AddUser(string name, string email, string phone)
	{
		using var c = new MySqlConnection(connectionString);
		c.Open();

		using var cmd = new MySqlCommand(
			@"INSERT INTO users (full_name, email, phone_number)
              VALUES (@n, @e, @p)", c);

		cmd.Parameters.AddWithValue("@n", name);
		cmd.Parameters.AddWithValue("@e", email);
		cmd.Parameters.AddWithValue("@p", phone);

		return cmd.ExecuteNonQuery();
	}

	public int DeleteByValue(string value)
	{
		using var c = new MySqlConnection(connectionString);
		c.Open();

		using var cmd = new MySqlCommand(
			@"DELETE FROM users
              WHERE full_name = @v
                 OR email = @v
                 OR phone_number = @v", c);

		cmd.Parameters.AddWithValue("@v", value);
		return cmd.ExecuteNonQuery();
	}
}