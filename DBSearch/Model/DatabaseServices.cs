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


		public static DataTable DBsearch(string tableName, string connectionString, string userInput)
		{
			using var connection = new MySqlConnection(connectionString);
			connection.Open();
			

			using var command = new MySqlCommand($"select * from {tableName} where {typeDecider(userInput)} = {userInput}", connection);

			using var reader = command.ExecuteReader();
			var dataTable = new DataTable();

			dataTable.Load(reader);


			return dataTable;
		}
		
		
		public static DataTable DBdelete(string tableName, string connectionString, string userInput)
		{
			using var connection = new MySqlConnection(connectionString);
			connection.Open();
			

			using var command = new MySqlCommand($"delete from {tableName} where {typeDecider(userInput)} = {userInput}", connection); 

			using var reader = command.ExecuteReader();
			var dataTable = new DataTable();

			dataTable.Load(reader);


			return dataTable;
		}
		public static DataTable BDadd(string tableName, string connectionString, string userInput, string fullName, string email, string phoneNumber)
		{
			using var connection = new MySqlConnection(connectionString);
			connection.Open();
			

			using var command = new MySqlCommand($"insert into users (full_name, email, phone_number) values ('{fullName}', '{email}', '{phoneNumber}');", connection); 

			using var reader = command.ExecuteReader();
			var dataTable = new DataTable();

			dataTable.Load(reader);


			return dataTable;
		}



		public static string typeDecider(string userInput)
		{
			string type = "";
			if (userInput.Contains("@"))
			{
				type = "email";
			}
			else if (userInput.Contains("+"))
			{
				type = "phone_number";
			}
			else if (userInput.Contains(" "))
			{
				type = "full_name";
			}
			else
			{
				Console.WriteLine("Wrong format! Try again");  //NOTE: hibát le kell kezelni, hogy újra megkérje a felhasználót a helyes formátumú inputra, jelenleg csak visszatér null-lal, ami nem jó
				return null;
			}
			return type;
		}


		/*
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

			
				switch (userChoice)
				{
					case 1:
					MySqlCommand command = new MySqlCommand($"select * from {tableName} where {tipus} = {userInput}", connection);
						break;
					case 2:
					MySqlCommand commandx = new MySqlCommand($"insert into users (full_name, email, phone_number) values ('Lenard Easterbrook', 'leasterbrook0@parallels.com', '+36 30 723 7523');", connection);
						break;
					case 3:
					MySqlCommand commandy = new MySqlCommand($"delete from {tableName} where {tipus} = {userInput}", connection);
						break;
				}







			using var nameCommand = new MySqlCommand($"select * from {tableName}  where {tipus} = {userInput}", connection);

			using var reader = nameCommand.ExecuteReader();
			var dataTable = new DataTable();

			dataTable.Load(reader);

		
			return dataTable;
		}*/
		


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
