
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
}