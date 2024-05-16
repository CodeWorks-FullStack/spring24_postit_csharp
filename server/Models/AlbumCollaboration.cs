namespace postit_csharp.Models;

public class AlbumCollaboration : Album
{
  public int AlbumMemberId { get; set; }
  public string AccountId { get; set; }
}