using Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();

        Console.Write("Ange id för den hund du vill se: ");
        if(int.TryParse(Console.ReadLine(), out int id))
        {
          var dog = db.GetDogById(id);

          Console.WriteLine($"Hunden med id {id} heter {dog.Name}");
        }
    }
}