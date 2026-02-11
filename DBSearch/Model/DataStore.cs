using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSearch.model
{
	internal class DataStore
	{
		private int id;
		private string fullName;
		private string email;
		private string phoneNumber;

		public int Id
		{
			get => id;
			set => id = value > 0 ? value : -1;
		}

		public string FullName
		{
			get => fullName;
			set => fullName = string.IsNullOrWhiteSpace(value) ? "UNKNOWN" : value;
		}

		public string Email
		{
			get => email;
			set => email = value.Contains("@") ? value : "invalid@email";
		}

		public string PhoneNumber
		{
			get => phoneNumber;
			set => phoneNumber = value.StartsWith("+") ? value : "N/A";
		}

		public DataStore() { }

		public DataStore(int id, string name, string email, string phone)
		{
			Id = id;
			FullName = name;
			Email = email;
			PhoneNumber = phone;
		}

		public override string ToString()
		{
			return $"{Id,-4} | {FullName,-25} | {Email,-30} | {PhoneNumber}";
		}
	}
}