using System.Text;
using Model;
using View;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var db = new DatabaseRepo();
        var ui = new Ui(db);
        ui.Run();
    }
}