using System.Data;
using System.Data.SqlClient;
using Dapper;
using Model;

public class DatabaseRepo
{

  private IDbConnection Connect()
  {
    string connectionString = File.ReadAllText("connectionString.txt");
    IDbConnection connection = new SqlConnection(connectionString);
    return connection;
  }

  // Hämta alla hundar
  public List<Dog> GetDogs()
  {
    using IDbConnection connection = Connect();
    string query = "SELECT * FROM Dog";
    var dogs = connection.Query<Dog>(query).AsList();
    return dogs;
  }

  public Dog GetDogById(int id)
  {
    using IDbConnection connection = Connect();
    string query = "SELECT * FROM Dog WHERE Id = @Id";
    var parameter = new {Id = id};

    return connection.QuerySingle<Dog>(query, parameter);
  }


 // sätta genomsnittsbetyg för en hund. Eller hämta visa.
 // visa highscore för en viss tillställning
 // visa highscore för alla tillställningar någonsin
}