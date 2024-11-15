using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();
        var ui = new UserInterface(db);

        var dog = ui.PickDog();
        
        var averageScore = db.GetDogAverageScore(dog!);

        Console.WriteLine($"{dog.Name} genomsnittsbetyg är {averageScore}");
    }
}