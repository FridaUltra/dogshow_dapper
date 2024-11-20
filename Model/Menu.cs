namespace Model;
// försökte göra denna generisk, men kan inte lägga till namn då. Flyttade över till ui metod istället.
public class CompetitionMenu(List<Competition> menuOptions)
{
    private List<Competition> _menuOptions = menuOptions;

  // visa menyn
    public int Display()
    {
      int selectedIndex = 0; // Startposition för pilen
      while (true)
        {

            Console.Clear();
            Console.WriteLine("Use arrow keys to navigate and Enter to select:");

            // Rita menyn
            for (int i = 0; i < _menuOptions.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine($"-> {_menuOptions[i].Name}"); // Markerat val
                }
                else
                {
                    Console.WriteLine($"   {_menuOptions[i].Name}"); // Ej markerat val
                }
            }

            // Hantera tangentbordets inmatning
            ConsoleKey key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow) // Flytta upp
            {
                selectedIndex--;
                if (selectedIndex < 0) selectedIndex = _menuOptions.Count - 1; // Loop till slutet
            }
            else if (key == ConsoleKey.DownArrow) // Flytta ner
            {
                selectedIndex++;
                if (selectedIndex >= _menuOptions.Count) selectedIndex = 0; // Loop till början
            }
            else if (key == ConsoleKey.Enter) // Bekräfta val
            {
                Console.Clear();
                Console.WriteLine($"You selected: Id:{_menuOptions[selectedIndex].Id}, {_menuOptions[selectedIndex].Name}");
                // break;
                return _menuOptions[selectedIndex].Id;
            }
        }
    }
}