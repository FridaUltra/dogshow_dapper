static class Utility
{
  public static string ReadString()
  {
    string input;
    while (string.IsNullOrEmpty(input = Console.ReadLine()))
    {
      Console.WriteLine("Ogiltig inmatning, försök igen:");
    }
    return input;
  }

  public static DateTime ReadDate()
  {
    DateTime input;
    while (!DateTime.TryParse(Console.ReadLine(), out input))
    {
      Console.WriteLine("Ogiltigt datumformat, försök igen (åååå-mm-dd):");
    }
    return input;
  }
}