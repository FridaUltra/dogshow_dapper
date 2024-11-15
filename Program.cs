using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();
        var ui = new UserInterface(db);

        // var dog = ui.PickDog();
		// ui.DisplayDogAverageScore(dog!);
        
		Console.Write("Ange id för den tävling som du vill se highscorelistan för: ");
		if(int.TryParse(Console.ReadLine(), out int id))
		{
			var competition = db.GetCompetitionById(id);

			Console.WriteLine($"{competition.Name}");

		}

    }
}