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

		var competition = ui.ChooseCompetitionByEnteringID();
        
		var highscoreList = db.GetHighscoreForCompetition(competition);

		foreach (var item in highscoreList)
		{
			Console.WriteLine($"Hund: {item.Dog} \t Ras: {item.Breed} \t Poäng: {item.Points} \t Ägare: {item.Owner}");
		}


        List<Competition> menuOptions = db.GetAllCompetitions();
       CompetitionMenu<Competition> competitionsMenu = new(menuOptions); 
       var competition2 = competitionsMenu.Display();
        Console.WriteLine(competition2.Id);

        //Hämtar alla tävlingar och visar en menu med pilar. Id på den valda returneras.
        // int competitionId = ui.ChooseCompetition();
        // Console.WriteLine(competitionId);
    }
}