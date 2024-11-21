namespace Model;

public class Menu<T>()
{
  // visa menyn
    public T Display(List<T> menuOptions, string header)
    {
      int selectedIndex = 0; // Startposition för pilen
      while (true)
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(header + "\n");
            Console.WriteLine("Pila upp och ner. Välj med enter:\n");
            Console.ForegroundColor = ConsoleColor.White;
            
            // Rita menyn
            for (int i = 0; i < menuOptions.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.WriteLine($"-> {menuOptions[i]}"); // Markerat val
                }
                else
                {
                    Console.WriteLine($"   {menuOptions[i]}"); // Ej markerat val
                }
            }

            // Hantera tangentbordets inmatning
            ConsoleKey key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow: // Flytta upp
                    selectedIndex--;
                    if (selectedIndex < 0) selectedIndex = menuOptions.Count - 1; // Loop till slutet
                    break;
                case ConsoleKey.DownArrow: // Flytta ner
                    selectedIndex++;
                    if (selectedIndex >= menuOptions.Count) selectedIndex = 0; // Loop till början
                    break;
                case ConsoleKey.Enter: // Välj
                    Console.Clear();
                    return menuOptions[selectedIndex];
                    
                default: break;
            }
        }
    }
}