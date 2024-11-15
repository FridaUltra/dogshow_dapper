using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

  // Highscore for a specific dog
  public float? GetDogAverageScore(Dog dog)
  {
    using IDbConnection connection = Connect();
    string query ="SELECT AVG(Points) FROM Result WHERE DogId = @DogId";
    var parameter = new { DogId = dog.Id };

    var averageScore = connection.QueryFirstOrDefault<float?>(query, parameter);
    return averageScore;
  }


 // sätta genomsnittsbetyg för en hund. Eller hämta visa.
 // visa highscore för en viss tillställning
 // visa highscore för alla tillställningar någonsin
}