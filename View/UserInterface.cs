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
}