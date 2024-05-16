namespace postit_csharp.Models;

// View Model to show the album side of the AlbumMember many-to-many
public class AlbumCollaboration : Album
{
  // All members (properties) of album class brought in through inheritance
  public int AlbumMemberId { get; set; } // ID of AlbumMember many-to-many
  public string AccountId { get; set; }
}