using Model;

namespace View;

public class UserInterface(DatabaseRepo db)
{
  private readonly DatabaseRepo _db = db;

  
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

  public int ChooseCompetition()
  {
    List<Competition> _menuOptions = db.GetAllCompetitions();
    int selectedIndex = 0; // Startposition för pilen
      while (true)
        {

            // Console.Clear();
            Console.WriteLine("Use arrow keys to navigate and Enter to select:");

            // Rita menyn
            for (int i = 0; i < _menuOptions.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine($"-> {_menuOptions[i].Name}"); // Markerat val
                }
                else
                {
                    Console.WriteLine($"   {_menuOptions[i].Name}"); // Ej markerat val
                }
            }

            // Hantera tangentbordets inmatning
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow) // Flytta upp
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = _menuOptions.Count - 1; // Loop till slutet
            }
            else if (key == ConsoleKey.DownArrow) // Flytta ner
            {
                selectedIndex++;
                if (selectedIndex >= _menuOptions.Count) selectedIndex = 0; // Loop till början
            }
            else if (key == ConsoleKey.Enter) // Bekräfta val
            {
                Console.Clear();
                Console.WriteLine($"You selected: Id:{_menuOptions[selectedIndex].Id}, {_menuOptions[selectedIndex].Name}");
                // break;
                return _menuOptions[selectedIndex].Id;
            }
        }
  }

}