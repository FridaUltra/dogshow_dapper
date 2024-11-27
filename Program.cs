using System;
using System.Text;
using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Competition competition = new();
        List<Competition> competitions;
        List<Competition> competitionsToday = [];
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
            Console.WriteLine("Det finns inga tävlingar idag");
            Console.Write("Vill du lägga upp en tävling för idag: ja/nej ");
            choice = Console.ReadLine() ?? string.Empty;
            choice.ToLower();
            if (choice == "ja")
            {
                Console.Write("Tävlingens namn: ");
                competition.Name = Console.ReadLine() ?? string.Empty;
                //testar
                Console.WriteLine(competition.Name);
                System.IO.File.WriteAllText("test.txt", competition.Name, Encoding.UTF8);
                Console.Write("Tävlingsort: ");
                competition.Location = Console.ReadLine() ?? string.Empty;
                competition.DateOfCompetition = DateTime.Now.Date;
                

                // LÄgger till dagens tävling och sparar det till databasen.
                db.AddCompetition(competition);
                Console.WriteLine(competition.Id);
                Console.WriteLine(competition.Name);
                Console.WriteLine(competition.Location);
                Console.WriteLine(competition.DateOfCompetition);
                                
            }

       }
       else
       {
         foreach (var item in competitionsToday)
         {  
           Console.WriteLine(item.Name); 
         }
       }

        // Visa tävlingar som finns idag



        
    }
        // Console.WriteLine("Dessa tävlingar finns idag");
    
    
    
  
}