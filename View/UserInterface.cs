using Model;

namespace View;

public class UserInterface(DatabaseRepo db, Menu<Competition> competitionsMenu)
{
  private readonly DatabaseRepo _db = db;
  private readonly Menu<Competition> _competitionsMenu = competitionsMenu;

  
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
    List<Competition> menuOptions = _db.GetAllCompetitions();
    var competition = competitionsMenu.Display(menuOptions);
        Console.WriteLine(competition.Id);
    return competition;    
  }

}