using Model;

namespace View;

public class UserInterface(DatabaseRepo db, Menu<Competition> competitionsMenu, Menu<string> menu, Menu<Dog> dogsMenu, Menu<Breed> breedsMenu)
{
  private readonly DatabaseRepo _db = db;
  private readonly Menu<Competition> _competitionsMenu = competitionsMenu;
  private readonly Menu<string> _menu = menu;
  private readonly Menu<Dog> _dogsMenu = dogsMenu;
  private readonly Menu<Breed> _breedsMenu = breedsMenu;

  public void Welcome()
  {
    while (true)
    {
      List<string> menuoptions = new(){"Bedömning", "Resultat", "Exit"};
        Console.WriteLine();
        var choice = _menu.Display(menuoptions, "Huvudmeny\nVad vill du göra?");
        
        if(choice == "Bedömning")
        {
            // kalla på metod för det.
            MakeAJudgement();
        }
        else if(choice == "Resultat")
        {
            // kalla på metod för det
            ShowResultMenu();
        }
        else
        {
            Console.WriteLine("Avslutar programmet");
            break;
        }
    }
  }

  private void ShowResultMenu()
  {
    while (true)
    {
      List<string> menuoptions = new List<string>
      {
        "Highscore för en tävling",
        "Highscore för alla tävlingar",
        "Genomsnittsbetyg för en hund",
        "Återgå till huvudmeny"
      };
   
      var choice = _menu.Display(menuoptions, "ResultatMeny");

      
      if (choice == "Highscore för en tävling")
      {
        ShowHighScoreForACompetition();
      }
      else if(choice == "Highscore för alla tävlingar")
      {
          ShowHighScoreForAllCompetitions();
      }
      else if(choice == "Genomsnittsbetyg för en hund")
      {
        ShowAverageDogMenu();
      }
      else if(choice == "Återgå till huvudmeny")
      {
        break;
      }
    }  
  }

  private void ShowHighScoreForAllCompetitions()
  {
    var list = _db.GetHighscoreForAllCompetitions();
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine($"Resultatlista för alla tävlingar\n");
    Console.ForegroundColor = ConsoleColor.White;

    foreach (var item in list)
    {
      Console.WriteLine($"{item.Dog},\tPoäng: {item.Points},\tÄgare: {item.Owner},\t Ras: {item.Breed} ");
    }
    
    Console.ReadLine();
  }
  private void ShowHighScoreForACompetition()
  {
    while (true)
    {
      var competition = ChooseCompetition();
        List<HighscoreEntry> highscores = _db.GetHighscoreForCompetition(competition);

        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine(competition.Name +"\n");
        Console.ForegroundColor = ConsoleColor.White;

        foreach (var item in highscores)
        {
          Console.WriteLine($"{item.Dog},\tPoäng: {item.Points},\tÄgare: {item.Owner},\t Ras: {item.Breed} ");
        }

        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Vill du se en annan resultatlista? y/n");
        Console.ForegroundColor = ConsoleColor.White;
        string svar = Console.ReadLine().ToLower();
        if(svar == "y") continue;
        else if(svar == "n") break;
    }
  }

  private void ShowAverageDogMenu()
  {
      while (true)
        {
          // Hämta ut alla hundar.
          List<Dog> menuOptions = _db.GetDogs();
          string header = "Välj den hund du vill se genomsnittsbetyg för";
          var dog = _dogsMenu.Display(menuOptions, header);
          var averageScore = _db.GetDogAverageScore(dog);

          Console.WriteLine($"Genomsnittsbetyget för {dog.Name} är {averageScore}");
          Thread.Sleep(2000);

          Console.WriteLine();
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("Vill du se för någon annan hund? y/n");
          Console.ForegroundColor = ConsoleColor.White;
          string svar = Console.ReadLine().ToLower();
          if(svar == "y") continue;
          else if(svar == "n") break;
          
        }
  }
  
  public Dog? PickDog()
  {
    //TODO: Lägg till felhantering med loop så att man måste välja en hund som finns.

    Console.Write("Ange id för den hund du vill se: ");

    if(int.TryParse(Console.ReadLine(), out int id))
    {
      var dog = _db.GetDogById(id);

      Console.WriteLine($"Hunden med id {id} heter {dog.Name}");
      return dog;
    }

    return null;
  }

  public void DisplayDogAverageScore(Dog dog)
  {
    var averageScore = _db.GetDogAverageScore(dog!);

    Console.WriteLine($"Genomsnittsbetyget för {dog.Name} är {averageScore}");
  }

  public Competition? ChooseCompetitionByEnteringID()
  {
    //TODO: Lägg till felhantering

    Console.Write("Ange id för den tävling som du vill se highscorelistan för: ");
    if(int.TryParse(Console.ReadLine(), out int id))
    {
      var competition = _db.GetCompetitionById(id);

      Console.WriteLine($"{competition.Name}");

      return competition;

    }
    return null;
  }

  private Competition ChooseCompetition()
  {
    // Hämtar alla tävlingar
    List<Competition> menuOptions = _db.GetAllCompetitions();
    //Visar menu och returnerar den valda tävlingen.
    var competition = _competitionsMenu.Display(menuOptions, "Välj en tävling");
    return competition;    
  }

  public void MakeAJudgement()
  {
    // Visa meny för vilken tävling och välj tävling
    var competition = ChooseCompetition();

    // När tävlingen är vald så vill vi visa alla hundar som finns i den tävlingen av en viss ras
    var menuOptions = _db.GetDogsBreedForACompetition(competition);

    // visa en lista med alla raser spara det rasnamn som väljs.
    var breed = _breedsMenu.Display(menuOptions,"Välj den ras som ska bedömas");

    // Hämta alla hundar i den tävlingen som har den ras som valts

       var dogs = _db.GetDogsWithSameBreedFromACompetition(competition, breed);

    // Visa alla hundarna och välj en hund.

     var dog = _dogsMenu.Display(dogs, "Välj hund att bedömma");

    Console.WriteLine($"Uppgifter om hunden: ");
    Console.WriteLine($"Namn: {dog.Name}, Ras: {breed}");
    // Metod för att poängsätta en viss hund 
    SetPoint(competition, dog);
    Console.WriteLine("Nu kanske du vill bedömma en till ?");
    Console.ReadLine();
    Console.WriteLine("Får du inte");
  }

  private void SetPoint(Competition competition, Dog dog)
  {
    Console.Write("Ange poäng: ");
    while (true)
    {
      if(int.TryParse(Console.ReadLine(), out int points))
      {
        Result judgement = new Result()
        {
          DogId = dog.Id,
          CompetitionId = competition.Id,
          Points = points
        };
       
       // Spara till databasen
        _db.AddResult(judgement);
        Console.WriteLine("Poängen är satt");
        break;
      }
      else
      {
        Console.WriteLine("Något gick fel. Ange endast heltal");
      }
      
    }
    
  }
   
}