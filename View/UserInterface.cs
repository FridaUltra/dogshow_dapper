namespace View;

public class UserInterface(DatabaseRepo db)
{
  private readonly DatabaseRepo _db = db;

  
  public void PickDog()
  {
    Console.Write("Ange id för den hund du vill se: ");
    
    if(int.TryParse(Console.ReadLine(), out int id))
    {
      var dog = _db.GetDogById(id);

      Console.WriteLine($"Hunden med id {id} heter {dog.Name}");
    }
  }
}