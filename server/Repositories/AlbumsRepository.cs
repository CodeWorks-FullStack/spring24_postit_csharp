



namespace postit_csharp.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  private Album PopulateCreator(Album album, Profile profile)
  {
    album.Creator = profile;
    return album;
  }

  internal Album ArchiveAlbum(int albumId)
  {
    string sql = @"
    UPDATE 
    albums 
    SET archived = true 
    WHERE id = @albumId;
    
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creatorId
    WHERE albums.id = @albumId;";

    Album album = _db.Query<Album, Profile, Album>(sql, PopulateCreator, new { albumId }).FirstOrDefault();

    return album;
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

    // Album album = _db.Query<Album, Account, Album>(sql, (album, account) =>
    // {
    //   album.Creator = account;
    //   return album;
    // }, albumData).FirstOrDefault();

    Album album = _db.Query<Album, Profile, Album>(sql, PopulateCreator, albumData).FirstOrDefault();

    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creatorId
    WHERE albums.id = @albumId;";

    Album album = _db.Query<Album, Profile, Album>(sql, PopulateCreator, new { albumId }).FirstOrDefault();

    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creatorId = accounts.id;";

    List<Album> albums = _db.Query<Album, Profile, Album>(sql, PopulateCreator).ToList();

    return albums;
  }
}