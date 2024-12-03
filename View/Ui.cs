using System.Globalization;
using Model;

namespace View;

public class Ui(DatabaseRepo db)
{
  private readonly DatabaseRepo _db = db;

  public void Run()
  {
    bool isRunning = true;
    while (isRunning)
    {
      Console.Clear();
      Console.WriteLine("StartMeny \n");
      Console.WriteLine($"[1] ResultatListor");
      Console.WriteLine($"[2] Bedömning");
      Console.WriteLine($"[3] Lägga till tävling");
      Console.WriteLine($"[4] Avsluta");

      string choice = Console.ReadLine();
      switch (choice)
      {
        case "1": 
        {
          ResultMenu();
          break;
        }
        case "2": 
        {
          JudgeToday();
          break;
        }
         case "3": 
        {
          AddCompetition();
          Console.ReadKey();
          break;
        }
        case "4": 
        {
          Console.WriteLine("Avsluta");
          Console.ReadKey();
          isRunning = false;
          break;
        }
        default: Console.WriteLine("Ogiltigt val. Försök igen");
        break;
      }
    }
  }


  private void ResultMenu()
  {
    bool exit = false;
    while (!exit)
    {
      Console.Clear();
      Console.WriteLine("ResultatMeny \n");
      Console.WriteLine($"[1] Resultatlista för en tävling");
      Console.WriteLine($"[2] ResultatLista för alla tävlingar");
      Console.WriteLine($"[3] Genomsnittsbetyg för en hund");
      Console.WriteLine($"[4] Återgå till huvudmenyn");

      string choice = Console.ReadLine();

      switch (choice)
      {
        case "1":
        {
          var competition = ChooseCompetition();
          if(competition == null) continue;
          Console.WriteLine($"\n\n{competition}\n");

          List<HighscoreEntry> highscores = _db.GetHighscoreForCompetition(competition);
        
          foreach (var item in highscores)
          {
            Console.WriteLine($"{item.Dog},\tPoäng: {item.Points},\tÄgare: {item.Owner},\t Ras: {item.Breed} ");
          }
          Console.ReadKey();
          break;
        }
        case "2":
        {
          ShowHighScoreForAllCompetitions();
          break;
        }
        case "3":
        {
          DisplayDogAverageScore();
          break;
        }
        case "4":
        {
          exit = true;
          break;
        }
        default:
        {
          Console.WriteLine("Försök igen, välj 1, 2, 3 eller 4");
          Thread.Sleep(1500);
          break;
        } 
      }
    }
  }

  private Competition ChooseCompetition()
  { 
    while (true)
    {
      Console.Clear();
      Console.WriteLine("Meny");
      Console.WriteLine("[E] Återgå till föregående meny");
      Console.WriteLine("=================================\n");
      Console.WriteLine("--> Resultatlista för en tävling <--");
      Console.Write("Ange E eller tävlingsid: ");

      string choice = Console.ReadLine();
      if(int.TryParse(choice, out int id))
      {
        var competition = _db.GetCompetitionById(id);
        if(competition == null)
        {
          Console.WriteLine("Det fanns ingen tävling med det id:et. Försök igen");
          Thread.Sleep(1500);
          continue;
        }
        return competition;    
      }
      else
      {
        if(choice.ToLower() == "e") return null;

        Console.WriteLine("Endast siffror eller E (exit) tillåtet. Försök igen");
        Thread.Sleep(1500);
      }
    }
  }

  private void ShowHighScoreForAllCompetitions()
  {
    var list = _db.GetHighscoreForAllCompetitions();
    
    Console.WriteLine($"Resultatlista för alla tävlingar\n");
    

    foreach (var item in list)
    {
      Console.WriteLine($"{item.Dog},\tPoäng: {item.Points},\tÄgare: {item.Owner},\t Ras: {item.Breed} ");
    }
    
    Console.ReadLine();
  }

  public void DisplayDogAverageScore()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("Meny");
      Console.WriteLine("[E] Återgå till föregående meny");
      Console.WriteLine("=================================\n");
      Console.WriteLine("Genomsnittsbetyg för en hund \n");
      Console.Write("Ange E eller hundens id: ");

      string choice = Console.ReadLine();

      if(int.TryParse(choice, out int id))
      {
        var dog = _db.GetDogById(id);
        if(dog == null)
        {
          Console.WriteLine("Det fanns ingen hund med det id:et. Försök igen");
          Thread.Sleep(1500);
          continue;
        }
        var averageScore = _db.GetDogAverageScore(dog);
        Console.WriteLine($"Genomsnittsbetyget för {dog.Name} är {averageScore} poäng");
        Console.ReadLine();
        break; 
      }
      else
      {
        if(choice.ToLower() == "e") break;

        Console.WriteLine("Endast siffror eller E (exit) tillåtet. Försök igen");
        Thread.Sleep(1500);
      }
    }
  }

  private void AddCompetition()
  {
    //TODO: testa att det nya funktionen att lägga till fungerar
    while (true)
    {
      Console.Clear();
      Console.WriteLine("-->Lägga till tävling \n");

      Console.Write("Ange tävlingsnamn: ");
      string name = Utility.ReadString();
      
      Console.Write("Ange ort/stad: ");
      string location = Utility.ReadString();

      Console.Write("Ange tävlingsdatum: ");
      DateTime dateOfCompetition = Utility.ReadDate();

      Competition competition = new()
      {
        Name = name,
        Location = location,
        DateOfCompetition = dateOfCompetition
      };

      _db.AddCompetition(competition);
      Console.WriteLine($"Id för den nya tävlingen är: {competition.Id}");
      Console.Write("Vill du lägga till en tävling till (j/n): ");
      
      if (Utility.IsAnswerYes()) continue;
      else break;
    }
    
  }

  private void JudgeToday()
  {
    // Hämta tävlingar för idag
    List<Competition> competitions = _db.GetAllCompetitions();
    List<Competition> competitionsToday = competitions.Where(c => c.DateOfCompetition == DateTime.Now.Date).ToList();
    Competition competition;
    
    int choice;
    if (competitionsToday.Count != 0)
    {
      while (true)
      {
        Console.Clear();
        Console.WriteLine("Dagens tävlingar:\n");
        for (int i = 0; i < competitionsToday.Count; i++)
        {
          Console.WriteLine($"[{i+1}] {competitionsToday[i].Name}");
        }
      
        Console.Write("Välj den tävling du ska bedömma. Ange siffran: ");
        choice = Utility.ReadInt();

        if (choice == 0)
        {
          Console.WriteLine("Försök igen. 0 finns inte i listan ");
          Thread.Sleep(2000);
          continue;
        }
        int index = choice - 1;
        //TODO: Lägg till felhantering. Det ska inte gå att välja ett högre tal än listans längd. Leder till ArgumentOutOfRangeException.
        competition = competitionsToday[index];
        break;
      }
      MakeJudgement(competition);

      List<HighscoreEntry> highscores = _db.GetHighscoreForCompetition(competition);
      Console.WriteLine($"Alla bedömningar för {competition}\n");
      foreach (var item in highscores)
      {
        Console.WriteLine($"{item.Dog},\tPoäng: {item.Points},\tÄgare: {item.Owner},\t Ras: {item.Breed} ");
      }
      Console.ReadLine();
    }
    else
    {
      Console.Clear();
      Console.WriteLine("Det finns inga tävlingar idag");
      Console.WriteLine("Lägg upp dagen tävling - val 3 i huvudmenyn");
      Console.ReadLine();
    }
  }

  private void MakeJudgement(Competition competition)
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine($"Tävling: {competition.Name}, Datum: {competition.DateOfCompetition.ToShortDateString()}\n");
      Console.Write("Ange id på den hund som ska bedömmas: ");
      int id = Utility.ReadInt();
      
      //Hämta hunden
      Dog dog = _db.GetDogById(id);
      if(dog == null)
      {
        Console.WriteLine("Det finns ingen hund med det id:t. Försök igen");
        continue;
      }

      //Kolla om hunden redan har fått poäng för denna tävlingen
      var results = _db.GetAllResultsByCompetitionId(competition.Id);
      bool isDogJudged = results.Any(r => r.DogId == dog.Id);
      if(!isDogJudged)
      {
        SetPoints(dog, competition);
      }
      else
      {
        Console.WriteLine("Hunden har fått bedömning");
        var result = results.FirstOrDefault(r => r.DogId == dog.Id);
        var points = result.Points;
        Console.WriteLine($"Hund: {dog.Name}, Poäng: {points}");
        Console.ReadKey();
        break;
      }
      Console.Write("Vill du bedömma en hund till?: (j/n)");

      if(Utility.IsAnswerYes()) continue;
      else break;
    }
    
  }

  private void SetPoints(Dog dog, Competition competition)
  {
    var breed = _db.GetBreedById(dog.BreedId);
    var owner = _db.GetOwnerById(dog.OwnerId);
    Console.Clear();
    Console.WriteLine($"Tävling: {competition}");
    Console.WriteLine($"Hund: {dog.Name}");
    Console.WriteLine($"Ras: {breed.Name}");
    Console.WriteLine($"Ägare: {owner.Name}\n");

    Console.Write("Ange poäng: ");
    int points = Utility.ReadInt();
    
    _db.AddResult(competition.Id, dog.Id, points);
    Console.WriteLine("Tack för din bedömning");
    Console.ReadLine();
  }
  
}