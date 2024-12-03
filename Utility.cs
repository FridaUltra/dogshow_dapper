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

  public static int ReadInt()
  {
    int input;
    while (!int.TryParse(Console.ReadLine(), out input))
    {
      Console.WriteLine("Endast siffror tillåtna, försök igen");
    }
    return input;
  }

  public static bool IsAnswerYes()
  {
    bool isYes = false;
    string input = ReadString();
    if(input.Equals("j", StringComparison.CurrentCultureIgnoreCase))
    {
      isYes = true;
    }

    return isYes;
  }
}