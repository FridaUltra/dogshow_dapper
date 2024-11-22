using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {
        var db = new DatabaseRepo();
        Menu<string> menu = new();
        Menu<Competition> competitionsMenu = new();
        Menu<Dog> dogsMenu = new();
        Menu<Breed> BreedsMenu = new();
        var ui = new UserInterface(db, competitionsMenu, menu, dogsMenu, BreedsMenu);

        ui.Welcome();
        
    }

    
  
}