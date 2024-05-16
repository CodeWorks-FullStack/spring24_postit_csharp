



namespace postit_csharp.Services;

public class AlbumMembersService
{
  private readonly AlbumMembersRepository _repository;

  public AlbumMembersService(AlbumMembersRepository repository)
  {
    _repository = repository;
  }

  internal MemberProfile CreateAlbumMember(AlbumMember albumMemberData)
  {
    MemberProfile albumMember = _repository.CreateAlbumMember(albumMemberData);
    return albumMember;
  }

  internal AlbumMember GetAlbumMemberById(int albumMemberId)
  {
    AlbumMember albumMember = _repository.GetAlbumMemberById(albumMemberId);

    if (albumMember == null)
    {
      throw new Exception($"Invalid id: {albumMemberId}");
    }

    return albumMember;
  }

  internal string DestroyAlbumMember(int albumMemberId, string userId)
  {
    AlbumMember albumMemberToDestroy = GetAlbumMemberById(albumMemberId);

    if (albumMemberToDestroy.AccountId != userId)
    {
      throw new Exception("NOT YOUR ALBUM MEMBER, FRIEND-O 🙎‍♂️");
    }

    _repository.DestroyAlbumMember(albumMemberId);

    return "No longer an album member!";
  }

  internal List<MemberProfile> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    List<MemberProfile> albumMembersProfiles = _repository.GetAlbumMemberProfilesByAlbumId(albumId);
    return albumMembersProfiles;
  }

  internal List<AlbumCollaboration> GetMyAlbumCollaborations(string userId)
  {
    List<AlbumCollaboration> albumCollaborations = _repository.GetMyAlbumCollaborations(userId);
    return albumCollaborations;
  }
}