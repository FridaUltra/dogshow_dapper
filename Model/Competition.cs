namespace Model;

public class Competition
{
  public int Id { get; set; } 
  public DateTime DateOfCompetition { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Location { get; set; } = string.Empty;

  public override string ToString()
    {
        return $"{Name}";
    }
}