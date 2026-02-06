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
        int input1 = 0;
        string input2 = null;
        bool switch1 = false;
        Regex filter = new Regex("");

        do
        {
            Console.WriteLine("What do you want to do? Give us the number of the option \n 1: Search \n 2: Add \n 3: Delete");
            try
            {
                input1 = Convert.ToInt32(Console.ReadLine()[0]);

                if (input1 > 0 && input1 < 4)
                {
                    switch1 = true;
                }
                else
                {
                    Console.WriteLine($"{input1} number is not a option");
                }
            }
            catch
            {
                Console.WriteLine("Wrong format! Try again");
            }

            if (input1 == 2)
            {
                Console.WriteLine("Write down your new row like this:\nTeljes Név;abc123@email.com;+00 00 000 0000");
                input2 = Console.ReadLine();

                input2 ? :
            }
            else if (switch1)
            {
                Console.WriteLine();
                input2 = Console.ReadLine();
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