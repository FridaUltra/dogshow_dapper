using System.Data;
using System.Data.SqlClient;
using Dapper;
using Model;

class DatabaseRepo
{

  private IDbConnection Connect()
  {
    string connectionString = File.ReadAllText("connectionString.txt");
    IDbConnection connection = new SqlConnection(connectionString);
    return connection;
  }

  // HÄmta alla hundar

  public List<Dog> GetDogs()
  {
    using IDbConnection connection = Connect();
    string query = "SELECT * FROM Dog";
    var dogs = connection.Query<Dog>(query).AsList();
    return dogs;
  }
}