using Model;

namespace View;

public class Ui(DatabaseRepo db)
{
  private readonly DatabaseRepo _db = db;

  public void Run()
  {
    int variabel = StartMenu();
    Console.Write($"menuval {variabel} valdes");
    Console.ReadKey();

  }

  private int StartMenu()
  {
    while (true)
    {
      Console.Clear();
      Console.WriteLine("StartMenu \n");
      Console.WriteLine($"[1] ResultatListor");
      Console.WriteLine($"[2] Bedömning");
      Console.WriteLine($"[3] Avsluta");

      if(int.TryParse(Console.ReadLine(), out int choice ))
      {
        if(choice == 1 || choice == 2 || choice == 3 ) return choice;
        else
        {
          Console.WriteLine("Försök igen, välj 1, 2 eller 3");
          Thread.Sleep(1500);
          continue;
        }
      }
      else
      {
        Console.WriteLine("Försök igen, välj 1, 2 eller 3");
        Thread.Sleep(1500);
      }
      
    }
    
  }
}