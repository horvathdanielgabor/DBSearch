using DBSearch.model;
using System.Data;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;

internal class Program
{
    public static readonly string connectionString = "Server=localhost;Database=DBSearch;User=root;";
    static List<DataStore> store = DataLoad("users", connectionString);

    private static void Main(string[] args)
    {
        int choice = 0;
        string input = null;
        bool active = false;

        do
        {
            Console.Write("What do you want to do? Give us the number of the option \n 1: Search \n 2: Add \n 3: Delete\nVálasztásod:");
            try
            {
                choice = Convert.ToInt32(Console.ReadLine()[0]);

                if (choice > 0 && choice < 4)
                {
                    active = true;
                }
                else
                {
                    Console.WriteLine($"{choice} number is not a option");
                }
            }
            catch
            {
                Console.WriteLine("Wrong format!");
            }

            do
            {
                if (choice == 2)
                {
                    Console.WriteLine("Write down your new row like this:\nTeljes Név;abc123@email.com;+00 00 000 0000");
                    input = Console.ReadLine();

                    if (Regex.IsMatch(input, @"^[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+(\s[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+)+;[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,};\+\d{2}\s\d{2}\s\d{3}\s\d{4}$"))
                    {
                        break;
                    }

                    Console.WriteLine("Your insert didn't match with the pattern!\nTry again!");
                }
                else if (active)
                {
                    Console.Write("Írd be a személy egyik adatát!\nAdat:");
                    input = Console.ReadLine();

                    switch (choice)
                    {
                        case 1:

                            break;
                        case 3:

                            break;
                    }

                    break;
                }
            }
            while (true);
            Console.WriteLine("Try again?");
            if (Console.ReadLine() == "No")
            {
                
            }
        }
        while (true);
    }

    private static List<DataStore> DataLoad(string v1, string v2)
    {
        List<DataStore> list = new List<DataStore>();

        foreach (DataRow row in DatabaseServices.GetAllData(v1, v2).Rows)
        {
            DataStore o = new DataStore(row.Field<int>(0), row.Field<string>(1), row.Field<string>(2), row.Field<string>(3));
            list.Add(o);
        }

        return list;
    }
}