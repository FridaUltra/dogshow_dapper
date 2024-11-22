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

  // Highscore for a specific dog
  public float? GetDogAverageScore(Dog dog)
  {
    using IDbConnection connection = Connect();
    string query ="SELECT AVG(Points) FROM Result WHERE DogId = @DogId";
    var parameter = new { DogId = dog.Id };

    var averageScore = connection.QueryFirstOrDefault<float?>(query, parameter);
    return averageScore;
  }
  // Get all breeds for a specific competition
  public List<Breed> GetDogsBreedForACompetition(Competition competition)
  {
    using IDbConnection connection = Connect();
    string query = @"SELECT DISTINCT
                      b.Name,
                      b.Id
                    FROM Result r
                    INNER JOIN 
                      Dog d ON r.DogId = d.Id
                    INNER JOIN
                      Breed b ON d.BreedId = b.Id
                    WHERE r.CompetitionId = @CompetitionId";
    var breeds = connection.Query<Breed>(query, new {CompetitionId = competition.Id }).AsList();
    return breeds;
  }

  // Get all dogs from a breed for a specific competition
  public List<Dog> GetDogsWithSameBreedFromACompetition(Competition competition, Breed breed)
  {
    using IDbConnection connection = Connect();
    string query = @"SELECT
                      d.Name,
                      d.Id,
                      d.OwnerId,
                      d.BreedId
                    FROM Result r
                    INNER JOIN 
                      Dog d ON r.DogId = d.Id
                    INNER JOIN
                      Breed b ON d.BreedId = b.Id
                    WHERE
                      r.CompetitionId = @CompetitionId
                      AND b.Id = @BreedId";
                      
    var dogs = connection.Query<Dog>(query, new {CompetitionId = competition.Id, BreedId = breed.Id }).AsList();
    return dogs;
  }

  public List<Competition> GetAllCompetitions()
  {
    using IDbConnection connection = Connect();
    string query = "SELECT * FROM Competition";
    var allCompetitions = connection.Query<Competition>(query).AsList();
    return allCompetitions;
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
                b.Name AS Breed,
                o.Name AS Owner,
                r.Points AS Points 
            FROM 
                Result r
            INNER JOIN 
                Dog d ON r.DogId = d.Id
            INNER JOIN
                Breed b ON d.BreedId = b.Id
            INNER JOIN
                Owner o ON d.OwnerId = o.Id
            WHERE 
                r.CompetitionId = @CompetitionId
            ORDER BY 
                r.Points DESC;";

    var parameter = new {CompetitionId = competition.Id};
    var highscoreList = connection.Query<HighscoreEntry>(query, parameter).AsList();
    return highscoreList;
  }

  public List<HighscoreEntry> GetHighscoreForAllCompetitions()
  {
    using IDbConnection connection = Connect();
    string query = @"SELECT 
                    d.Name AS Dog, 
                    b.Name AS Breed,
                    o.Name As Owner,
                    SUM(r.Points) AS Points
                FROM 
                    Result r
                INNER JOIN 
                    Dog d ON r.DogId = d.Id
                INNER JOIN 
                    Breed b ON d.BreedId = b.Id
                INNER JOIN
                    Owner o ON d.OwnerId = o.Id
                GROUP BY 
                    d.Id, d.Name, b.Name, o.Name
                ORDER BY 
                    Points DESC;";
    var highscoreList = connection.Query<HighscoreEntry>(query).AsList();
    return highscoreList;
  }

  public void AddResult(Result result)
  {
    using IDbConnection connection = Connect();
     string sql = $"INSERT INTO Result (CompetitionId, DogId, Points) VALUES(@CompetitionId, @DogId, @Points)";
    var parameters = new { result.CompetitionId, result.DogId, result.Points};
    connection.Execute(sql, parameters);
  }

 // visa highscore för alla tillställningar någonsin
}