using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSearch.model
{
	internal class DataStore
	{
		public DataStore(int id, string full_name, string email, string phone_number)
		{
			this.id = id;
			this.full_name = full_name;
			this.email = email;
			this.phone_number = phone_number;
		}

		public int id { get; set; }
		public string full_name { get; set; }
		public string email { get; set; }
		public string phone_number { get; set; }
	}
}
