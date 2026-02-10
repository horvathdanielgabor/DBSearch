using MySqlConnector;
using System;
using System.Data;

namespace DBSearch.model
{
	internal class DatabaseServices
	{
		public static string typeDecider(string userInput)
		{
			if (string.IsNullOrWhiteSpace(userInput))
				return null;

			if (userInput.Contains("@"))
				return "email";
			if (userInput.Contains("+"))
				return "phone_number";
			if (userInput.Contains(" "))
				return "full_name";

			return null;
		}

		public static DataTable DBsearch(string tableName, string connectionString, string userInput)
		{
			string column = typeDecider(userInput);
			if (column == null)
				throw new Exception("Hibás keresési formátum");

			using var connection = new MySqlConnection(connectionString);
			connection.Open();

			using var command = new MySqlCommand(
				$"SELECT * FROM {tableName} WHERE {column} = @value",
				connection
			);

			command.Parameters.AddWithValue("@value", userInput);

			using var reader = command.ExecuteReader();
			var table = new DataTable();
			table.Load(reader);

			return table;
		}

		public static int DBdelete(string tableName, string connectionString, string userInput)
		{
			string column = typeDecider(userInput);
			if (column == null)
				throw new Exception("Hibás törlési formátum");

			using var connection = new MySqlConnection(connectionString);
			connection.Open();

			using var command = new MySqlCommand(
				$"DELETE FROM {tableName} WHERE {column} = @value",
				connection
			);

			command.Parameters.AddWithValue("@value", userInput);

			return command.ExecuteNonQuery();
		}

		public static int DBadd(string connectionString, string fullName, string email, string phoneNumber)
		{
			using var connection = new MySqlConnection(connectionString);
			connection.Open();

			using var command = new MySqlCommand(
				"INSERT INTO users (full_name, email, phone_number) VALUES (@name, @email, @phone)",
				connection
			);

			command.Parameters.AddWithValue("@name", fullName);
			command.Parameters.AddWithValue("@email", email);
			command.Parameters.AddWithValue("@phone", phoneNumber);

			return command.ExecuteNonQuery();
		}
	}
}