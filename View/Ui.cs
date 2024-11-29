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
          Console.WriteLine("Bedömning");
          Console.ReadKey();
          break;
        }
         case "3": 
        {
          Console.WriteLine("Lägga till tävling");
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
      Console.WriteLine($"[2] ResultatLista för alla tävling");
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

}