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


        //Hämta alla tävlingar
        List<Competition> competitions = db.GetAllCompetitions();
        // foreach (var item in competitions)
        // {
        //     Console.WriteLine(item.Name);           
        // }


    //    CompetitionMenu competitionsMenu = new(competitions); 
    //    int competiotionsId = competitionsMenu.Display();

        int competitionId = ui.ChooseCompetition();
        Console.WriteLine(competitionId);
    }
}