using Model;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();

        List<Dog> dogs = db.GetDogs();

        foreach (var dog in dogs)
        {
          Console.WriteLine(dog.Name);
        }
    }
}