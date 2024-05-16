

namespace postit_csharp.Services;

public class AlbumMembersService
{
  private readonly AlbumMembersRepository _repository;

  public AlbumMembersService(AlbumMembersRepository repository)
  {
    _repository = repository;
  }

  internal AlbumMember CreateAlbumMember(AlbumMember albumMemberData)
  {
    AlbumMember albumMember = _repository.CreateAlbumMember(albumMemberData);
    return albumMember;
  }

  internal List<MemberProfile> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    List<MemberProfile> albumMembersProfiles = _repository.GetAlbumMemberProfilesByAlbumId(albumId);
    return albumMembersProfiles;
  }
}