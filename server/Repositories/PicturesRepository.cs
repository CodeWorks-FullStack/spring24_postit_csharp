



namespace postit_csharp.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  private Picture PopulateCreator(Picture picture, Profile profile)
  {
    picture.Creator = profile;
    return picture;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO
    pictures(imgUrl, creatorId, albumId)
    VALUES(@ImgUrl, @CreatorId, @AlbumId);
    
    SELECT 
    pictures.*,
    accounts.* 
    FROM pictures
    JOIN accounts ON accounts.id = pictures.creatorId
    WHERE pictures.id = LAST_INSERT_ID();";

    // Picture picture = _db.Query<Picture, Profile, Picture>(sql, (picture, profile) =>
    // {
    //   picture.Creator = profile;
    //   return picture;
    // }, pictureData).FirstOrDefault();
    Picture picture = _db.Query<Picture, Profile, Picture>(sql, PopulateCreator, pictureData).FirstOrDefault();

    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON accounts.id = pictures.creatorId
    WHERE pictures.albumId = @albumId;";

    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, PopulateCreator, new { albumId }).ToList();

    return pictures;
  }

  internal Picture GetPictureById(int pictureId)
  {
    string sql = "SELECT * FROM pictures WHERE id = @pictureId;";

    Picture picture = _db.Query<Picture>(sql, new { pictureId }).FirstOrDefault();

    return picture;
  }

  internal void DestroyPicture(int pictureId)
  {
    string sql = "DELETE FROM pictures WHERE id = @pictureId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { pictureId });

    // Silly code beyond this point

    if (rowsAffected == 0)
    {
      throw new Exception("Delete failed!");
    }

    if (rowsAffected > 1)
    {
      throw new Exception("UH OH. CALL MICK FOR HELP");
    }
  }
}