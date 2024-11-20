namespace Model;
// försökte göra denna generisk, men kan inte lägga till namn då. Flyttade över till ui metod istället.
public class Menu<T>(List<T> menuOptions)
{
    private List<T> _menuOptions = menuOptions;

  // visa menyn
    public T Display()
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
                    Console.WriteLine($"-> {_menuOptions[i]}"); // Markerat val
                }
                else
                {
                    Console.WriteLine($"   {_menuOptions[i]}"); // Ej markerat val
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
                Console.WriteLine($"You selected: {_menuOptions[selectedIndex]}");
               
                return _menuOptions[selectedIndex];
            }
        }
    }
}