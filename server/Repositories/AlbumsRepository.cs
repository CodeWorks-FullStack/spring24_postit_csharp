
namespace postit_csharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(title, category, description, coverImg, creatorId)
    VALUES(@Title, @Category, @Description, @CoverImg, @CreatorId);

    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creatorId = accounts.id
    WHERE albums.id = LAST_INSERT_ID();";

    Album album = _db.Query<Album, Account, Album>(sql, (album, account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).FirstOrDefault();

    return album;
  }
}