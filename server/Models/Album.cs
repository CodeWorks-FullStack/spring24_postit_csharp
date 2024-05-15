namespace postit_csharp.Models;

public class Album
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public string CoverImg { get; set; }
  public bool Archived { get; set; }
  public string Category { get; set; }

  // FIXME make this better
  public Account Creator { get; set; }
}