using DBSearch.model;
using System.Data;
using System.Runtime.Intrinsics;

internal class Program
{
    public static readonly string connectionString = "Server=localhost;Database=DBSearch;User=root;";
    static List<DataStore> store = DataLoad("users", connectionString);

    private static void Main(string[] args)
    {
        int input = 0;

        do
        {
            Console.WriteLine("What do you want to do? Give us the number of the option \n 1: Search \n 2: Add \n 3: Delete");
            try
            {
                input = Convert.ToInt32(Console.ReadLine()[0]);

                if (input > 0 && input < 4)
                {
                    
                }
                else
                {
                    Console.WriteLine($"{input} number is not a option");
                }
            }
            catch
            {
                Console.WriteLine("Wrong format! Try again");
            }

            if (input == 2)
            {
                Console.WriteLine();
                break;
            }
            else
            {
                Console.WriteLine();
                break;
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