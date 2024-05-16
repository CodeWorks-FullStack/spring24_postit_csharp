

namespace postit_csharp.Repositories;

public class AlbumMembersRepository
{
  private readonly IDbConnection _db;

  public AlbumMembersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal AlbumMember CreateAlbumMember(AlbumMember albumMemberData)
  {
    // FIXME make this better
    string sql = @"
    INSERT INTO
    albumMembers(albumId, accountId)
    VALUES(@AlbumId, @AccountId);

    SELECT * FROM albumMembers WHERE id = LAST_INSERT_ID();";

    AlbumMember albumMember = _db.Query<AlbumMember>(sql, albumMemberData).FirstOrDefault();

    return albumMember;
  }

  internal List<MemberProfile> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT 
    albumMembers.*,
    accounts.* 
    FROM albumMembers 
    JOIN accounts ON accounts.id = albumMembers.accountId
    WHERE albumMembers.albumId = @albumId
    ;";

    List<MemberProfile> albumMembersProfiles = _db.Query<AlbumMember, MemberProfile, MemberProfile>
    (sql, (albumMember, profile) =>
    {
      profile.AlbumMemberId = albumMember.Id;
      profile.AlbumId = albumMember.AlbumId;
      return profile;
    }, new { albumId }).ToList();
    return albumMembersProfiles;
  }
}