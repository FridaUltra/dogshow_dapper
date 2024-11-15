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

  public Competition GetCompetitionById(int id)
  {
    using IDbConnection connection = Connect();
    string query = "SELECT * FROM Competition WHERE Id = @Id";
    var parameter = new {Id = id};

    return connection.QuerySingle<Competition>(query, parameter);
  }

  public List<HighscoreEntry> GetHighscoreForCompetition(Competition competition)
  {
    using IDbConnection connection = Connect();
    string query = @"
            SELECT 
                d.Name AS Dog, 
                r.Points AS Points
            FROM 
                Result r
            INNER JOIN 
                Dog d ON r.DogId = d.Id
            WHERE 
                r.CompetitionId = @CompetitionId
            ORDER BY 
                r.Points DESC;";

    var parameter = new {CompetitionId = competition.Id};
    var highscoreList = connection.Query<HighscoreEntry>(query, parameter).AsList();
    return highscoreList;
  }
 // visa highscore för en viss tillställning
 // visa highscore för alla tillställningar någonsin
}