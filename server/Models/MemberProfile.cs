namespace postit_csharp.Models;

public class MemberProfile : Profile
{
  // All members of profile class brought in through inheritance
  public int AlbumMemberId { get; set; }
  public int AlbumId { get; set; }
}