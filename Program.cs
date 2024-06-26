// See https://aka.ms/new-console-template for more information
using Microsoft.Data.SqlClient;
using System.Data.Common;
using QueryProviderExample;
using System.Linq.Expressions;


const string connectionString = "Server=localhost;Database=Northwind;User=sa;Password=Dracarys3;TrustServerCertificate=True";
using var sqlConnection = new SqlConnection(connectionString);

sqlConnection.Open();

var db = new Northwind(sqlConnection);

var city = "London";
var query = db.Customers.Where(c => c.City == city)
    .Select( x => new { Name = x.ContactName, Phone = x.Phone});    

Console.WriteLine(query.Expression.ToString());

foreach (var customer in query.ToList())
{
    Console.WriteLine($"{customer.Name} - {customer.Phone}");
}

sqlConnection.Close();

var a = () => 1;
var b = a();


public class Customers
{
    public string CustomerID { get; set; }
    public string ContactName { get; set; }
    public string Phone { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

public class Orders
{
    public int OrderID { get; set; }
    public string CustomerID { get; set; }
    public DateTime OrderDate { get; set; }

}

public class Northwind
{
    public MyQueryable<Orders> Orders;
    public MyQueryable<Customers> Customers;
    public Northwind(DbConnection connection)
    {
        DbQueryProvider provider  = new DbQueryProvider(connection);
        Customers = new MyQueryable<Customers>(provider);
        Orders = new MyQueryable<Orders>(provider);
    }

}