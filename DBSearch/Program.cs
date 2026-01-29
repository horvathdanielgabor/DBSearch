using System.Runtime.Intrinsics;
using DBSearch.model;

internal class Program
{
    public static readonly string connectionString = "Server=localhost;Database=DBSearch;User=root;";
    List<DataStore> store = DataLoad("users", connectionString);

    private static void Main(string[] args)
    {
        
    }

    private List<DataStore> DataLoad(string v1, string v2)
    {
        foreach (DataRow row in DatabaseServices.GetAllData(v1, v2).Rows)
        {
            DataStore o = new DataStore(row.Field<int>(0), row.Field<string>(1), row.Field<string>(2), row.Field<string>(3), Convert.ToDouble(row.Field<decimal>(4)), row.Field<string>(5), row.Field<int>(6), row.Field<int>(7), row.Field<string>(8), row.Field<string>(9), row.Field<string>(10), row.Field<string>(11), row.Field<string>(12), row.Field<string>(13), row.Field<int>(14), row.Field<int>(15), row.Field<int>(16));
            store.Add(o);
        }
    }
}