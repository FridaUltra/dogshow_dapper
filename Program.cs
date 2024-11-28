using System.Text;
using Model;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Competition competition = new();
        List<Competition> competitions;
        List<Competition> competitionsToday = [];
        Dog dog = new();
        string choice;

        var db = new DatabaseRepo();
        // Menu<string> menu = new();
        // Menu<Competition> competitionsMenu = new();
        // Menu<Dog> dogsMenu = new();
        // Menu<Breed> BreedsMenu = new();
        // var ui = new UserInterface(db, competitionsMenu, menu, dogsMenu, BreedsMenu);

        // ui.Welcome();

       

        Console.WriteLine("Välkommen juryn");
        Console.WriteLine("Dagens tävlingar:\n");

        // Hämta alla tävlingar
        competitions =  db.GetAllCompetitions();

        
        // Sortera på dagens datum.
       competitionsToday = competitions.Where(c => c.DateOfCompetition == DateTime.Now.Date).ToList();


       // Om listan är tom. Skriv ut det och fråga om de vill lägga upp en tävling
       if(competitionsToday.Count == 0)
       {
            while (true)
            {
                
                Console.WriteLine("Det finns inga tävlingar idag");
                Console.Write("Vill du lägga upp en tävling för idag (j/n): ");
                choice = Console.ReadLine() ?? string.Empty;
                choice.ToLower();
                if (choice == "j")
                {
                    Console.Write("Tävlingens namn: ");
                    competition.Name = Console.ReadLine() ?? string.Empty;
                    Console.Write("Tävlingsort: ");
                    competition.Location = Console.ReadLine() ?? string.Empty;
                    competition.DateOfCompetition = DateTime.Now.Date;
                    

                    // LÄgger till dagens tävling och sparar det till databasen.
                    db.AddCompetition(competition);
                    break;
                                    
                }
                else if(choice == "n")
                {
                    Console.WriteLine("du svarade nej. Programmet stängs");
                    //stämmer inte i detta flöde
                }
                else
                {
                    break;
                }
            }
            foreach (var item in competitionsToday)
            {  
                Console.WriteLine(item.Name); 
            }

       }
      
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Välj den tävling du ska bedömma. Ange siffran");
            // Visa tävlingar som finns idag
            for (int i = 0; i < competitionsToday.Count; i++)
            {
                Console.WriteLine($"[{i+1}] {competitionsToday[i].Name}");
            }
            choice = Console.ReadLine();
            if(int.TryParse(choice, out int choiceIndex))
            {
                if(choiceIndex == 0)
                {
                    Console.WriteLine("Försök igen. 0 finns inte i listan ");
                    Thread.Sleep(2000);
                    continue;
                }
                int index = choiceIndex - 1;
                competition = competitionsToday[index];
                break;
            }
            else
            {
                Console.WriteLine("Försök igen. Ange en siffra från listan");
                Thread.Sleep(2000);
            }
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Tävling: {competition.Name}, Datum: {competition.DateOfCompetition.ToShortDateString()}\n");
            Console.Write("Ange id på den hund som ska bedömmas: ");
            if(int.TryParse(Console.ReadLine(), out int id ))
            {
                //Hämta hunden
                dog = db.GetDogById(id);
                //Om id inte leder till någon hund
                //Om hunden inte finns kan man få valet att lägga till en hund

                //Kolla om hunden redan har fått poäng för denna tävlingen
                var results = db.GetAllResultsByCompetitionId(competition.Id);
                if(results.Count == 0) 
                {
                    Console.WriteLine("listan är tom");
                   // Det går att bedömma hunden
                   GiveJudgement(dog, competition, db);
                }
                // Checkar om hunden har ett resultat
                bool isDogJudged = results.Any(r => r.DogId == dog.Id);
                if(isDogJudged)
                {
                    Console.WriteLine("Hunden har fått bedömning");
                    var result = results.FirstOrDefault(r => r.DogId == dog.Id);
                    var points = result.Points;
                    Console.WriteLine($"Hund: {dog.Name}, Poäng: {points}");
                    Thread.Sleep(2000);
                    //Ge möjligheten att uppdatera poängen.
                    Console.WriteLine("Vill du uppdatera resultatet? (j/n)");
                    Console.ReadKey();
                }
                else
                {
                    // bedöm hunden
                    GiveJudgement(dog, competition, db);
                }
                
                
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("");
            }
          
        }
              
       




        
    }
        // Console.WriteLine("Dessa tävlingar finns idag");
    
    public static void GiveJudgement(Dog dog, Competition competition, DatabaseRepo db)
    {
        var breed = db.GetBreedById(dog.BreedId);
        var owner = db.GetOwnerById(dog.OwnerId);

        Console.WriteLine($"Tävling: {competition.Name} Datum: {competition.DateOfCompetition}");
        Console.WriteLine($"Ägare: {owner.Name}");
        Console.WriteLine($"Hund: {dog.Name}");
        Console.WriteLine($"Ras: {breed.Name}\n");

        Console.Write("Ange poäng: ");
        if(int.TryParse(Console.ReadLine(), out int points))
        {
            db.AddResult(competition.Id, dog.Id, points);
            Console.WriteLine("Tack för din bedömning");
            // Se lista för alla 
            List<HighscoreEntry> highscores = db.GetHighscoreForCompetition(competition);

            foreach (var item in highscores)
            {
                Console.WriteLine($"{item.Dog},\tPoäng: {item.Points},\tÄgare: {item.Owner},\t Ras: {item.Breed} ");
            }
        }
    }
    
  
}