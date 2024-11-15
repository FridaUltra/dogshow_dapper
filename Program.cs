using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();
        var ui = new UserInterface(db);

        var dog = ui.PickDog();
		ui.DisplayDogAverageScore(dog!);
        
    }
}