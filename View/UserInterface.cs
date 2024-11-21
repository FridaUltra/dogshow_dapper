using Model;

namespace View;

public class UserInterface(DatabaseRepo db, Menu<Competition> competitionsMenu, Menu<string> menu, Menu<Dog> dogsMenu)
{
  private readonly DatabaseRepo _db = db;
  private readonly Menu<Competition> _competitionsMenu = competitionsMenu;
  private readonly Menu<string> _menu = menu;
  private readonly Menu<Dog> _dogsMenu = dogsMenu;

  public void Welcome()
  {
    while (true)
    {
      Console.ForegroundColor = ConsoleColor.DarkCyan;
      Console.WriteLine("Välkommen!\n");
      Console.WriteLine("Här kan du se resultat och genomsnittsbetyg för alla hundar och tävlingar");
      Console.ForegroundColor = ConsoleColor.White;
      List<string> menuoptions = new List<string>{"Highscore för en tävling", "Highscoore för alla tävlingar", "Genomsnittsbetyg för en hund"};
      string header = "Huvudmenu";  
      var retur = _menu.Display(menuoptions, header);

      
      if (retur == "Highscore för en tävling")
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
        Console.WriteLine("Vill du återgå till menyn? y/n");
        Console.ForegroundColor = ConsoleColor.White;
        string svar = Console.ReadLine().ToLower();
        if(svar == "y") continue;
        else if(svar == "n") break;
      }
      else if(retur == "Highscoore för alla tävlingar")
      {

      }
      else if(retur == "Genomsnittsbetyg för en hund")
      {
        while (true)
        {
          // Hämta ut alla hundar.
          List<Dog> menuOptions = _db.GetDogs();
          header = "Välj den hund du vill se genomsnittsbetyg för";
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
      else{
        break;
      }
    }
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("Tack och välkommen åter");
    Console.ForegroundColor = ConsoleColor.White;
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

  public Competition ChooseCompetition()
  {
    // Hämtar alla tävlingar
    List<Competition> menuOptions = _db.GetAllCompetitions();
    string header = "Alla tävlingar";
    //Visar menu och returnerar den valda tävlingen.
    var competition = _competitionsMenu.Display(menuOptions, header);
    return competition;    
  }

}