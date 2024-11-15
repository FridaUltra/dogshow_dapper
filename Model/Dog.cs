namespace Model;

public class Dog
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public int OwnerId { get; set; }
  public int BreedId { get; set; }
}