namespace postit_csharp.Models;

// View Model to show the profile side of the AlbumMember many-to-many
public class MemberProfile : Profile
{
  // All members (properties) of profile class brought in through inheritance
  public int AlbumMemberId { get; set; } // ID of AlbumMember many-to-many
  public int AlbumId { get; set; }
}