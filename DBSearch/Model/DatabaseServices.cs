using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSearch.model
{
	internal class DatabaseServices
	{
		private static string connectionString;
		private static string table;
		private static string queryParameters;

		public static void DbConnectionCheck(string connectionString)
		{
			try
			{
				using (MySqlConnection connection = new MySqlConnection(connectionString))
				{
					connection.Open();
					Console.WriteLine("Successful connection to database");
				}
			}
			catch (Exception error)
			{
				Console.WriteLine("Unsuccesful connection to database");
				Console.WriteLine(error);
			}




		}

		public static DataTable DBFunctions(string tableName, string connectionString, string userInput, int userChoice)
		{
			using var connection = new MySqlConnection(connectionString);
			connection.Open();
			string tipus = "";


			if (userInput.Contains("@"))
			{
				tipus = "email";
			}
			else if (userInput.Contains("+"))
			{
				tipus = "phone_number";
			}
			else if(userInput.Contains(" "))
			{
				tipus = "full_name";
			}
			else
			{
				Console.WriteLine("Wrong format! Try again");
				return null;
			}



			using var Command = new MySqlCommand($"select * from {tableName} where {tipus} = {userInput}", connection); //keresesalapja

			using var nameCommand = new MySqlCommand($"select * from {tableName}  where {tipus} = {userInput}", connection);

			using var reader = nameCommand.ExecuteReader();
			var dataTable = new DataTable();

			dataTable.Load(reader);


			return dataTable;
		}
		


		public static DataTable GetAllData(string tableName, string connectionString)
		{
			using var connection = new MySqlConnection(connectionString);
			connection.Open();
			using var command = new MySqlCommand($"select * from " + tableName, connection);

			using var reader = command.ExecuteReader();
			var dataTable = new DataTable();

			dataTable.Load(reader);


			return dataTable;
		}
	}
}
