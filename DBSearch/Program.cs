using DBSearch.model;
using System.Data;
using System.Runtime.Intrinsics;
using System.Text.RegularExpressions;

internal class Program
{
    public static readonly string connectionString = "Server=localhost;Database=DBSearch;User=root;";

    private static void Main(string[] args)
    {
        DatabaseServices db = new DatabaseServices(connectionString);
        int choice = 0;
        string input = null;
        bool active = false;

        if (!db.CheckConnection())
        {
            Console.WriteLine("Database connection failed!");
            return;
        }

        do
        {
            Console.Write("What do you want to do? Give us the number of the option \n 1: Search\n 2: Add\n 3: Delete\n 0: Exit\nChoice: ");
            try
            {
                choice = Convert.ToInt32(Console.ReadLine()[0].ToString());

                if (Regex.IsMatch(choice.ToString(), @"^[1-3]{1}$"))
                {
                    active = true;
                }
                else if (choice == 0)
                {
                    active = false;
                }
                else
                {
                    Console.WriteLine($"{choice} number is not a option\n");
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
                    Console.Write("Write down your new row like this:\nSample: Full Name;abc123@email.com;+00 00 000 0000\nYour insert: ");
                    input = Console.ReadLine();

                    if (Regex.IsMatch(input, @"^[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+(\s[A-ZÁÉÍÓÖŐÚÜŰ][a-záéíóöőúüű]+)+;[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,};\+\d{2}\s\d{2}\s\d{3}\s\d{4}$"))
                    {
                        Console.WriteLine($"Insert {(db.AddUser(input.Split(';')[0], input.Split(';')[1], input.Split(';')[2]) == 1 ? "Ok" : "Failed")}");
                        break;
                    }

                    Console.WriteLine("Your insert didn't match with the pattern!\nTry again!");
                }
                else if (choice != 2)
                {
                    if (active)
                    {
                        Console.Write("Write here one of their data here!(Full Name/Email/Phone Number)\nData: ");
                        input = Console.ReadLine();
                    }

                    switch (choice)
                    {
                        case 1:
                            DataTable res = db.SearchLike(input);
                            List<DataStore> store = DataLoad(res);
                            Console.WriteLine($"\nResults: {res.Rows.Count}\n");

                            store.ForEach(v => Console.WriteLine(v));
                            break;
                        case 3:
                            Console.WriteLine($"Deleted rows: {db.DeleteByValue(input)}");
                            break;
                        default:
                            break;
                    }

                    break;
                }
            }
            while (true);
            
            if (active)
            {
                Console.WriteLine("Try again?");
                if (Regex.IsMatch(Console.ReadLine().ToLower(),@"^\s{0,}n|\s{0,}no"))
                {
                    break;
                }
            }
        }
        while (choice != 0);
    }

    private static List<DataStore> DataLoad(DataTable v)
    {
        return v.AsEnumerable().Select(r => new DataStore(r.Field<int>("id"), r.Field<string>("full_name"), r.Field<string>("email"), r.Field<string>("phone_number"))).ToList();
    }
}