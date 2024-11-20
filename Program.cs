using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();
        Menu<Competition> competitionsMenu = new();
        var ui = new UserInterface(db, competitionsMenu);

        // var dog = ui.PickDog();
		// ui.DisplayDogAverageScore(dog!);

		// var competition = ui.ChooseCompetitionByEnteringID();
        
		// var highscoreList = db.GetHighscoreForCompetition(competition);

		// foreach (var item in highscoreList)
		// {
		// 	Console.WriteLine($"Hund: {item.Dog} \t Ras: {item.Breed} \t Poäng: {item.Points} \t Ägare: {item.Owner}");
		// }


    //     List<Competition> menuOptions = db.GetAllCompetitions();
    //    Menu<Competition> competitionsMenu2 = new(); 
    //    var competition2 = competitionsMenu.Display(menuOptions);
    //     Console.WriteLine(competition2.Id);

        // injecta menyn i ui:t och lägg in i nån metod där.

        //Hämtar alla tävlingar och visar en menu med pilar. Id på den valda returneras.
        var competition = ui.ChooseCompetition();
        Console.WriteLine(competition);


    }
}