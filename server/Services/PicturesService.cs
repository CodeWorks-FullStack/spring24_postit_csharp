

namespace postit_csharp.Services;

public class PicturesService
{
  private readonly PicturesRepository _repository;
  private readonly AlbumsService _albumsService;

  public PicturesService(PicturesRepository repository, AlbumsService albumsService)
  {
    _repository = repository;
    _albumsService = albumsService;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Album album = _albumsService.GetAlbumById(pictureData.AlbumId);

    if (album.Archived)
    {
      throw new Exception($"{album.Title} is archived and no longer accepting new pictures");
    }

    Picture picture = _repository.CreatePicture(pictureData);

    return picture;
  }

  internal Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);

    if (picture == null)
    {
      throw new Exception($"Invalid id: {pictureId}");
    }

    return picture;
  }

  internal string DestroyPicture(int pictureId, string userId)
  {
    Picture picture = GetPictureById(pictureId);

    if (picture.CreatorId != userId)
    {
      throw new Exception("YOU CANNOT DELETE A PICTURE YOU DID NOT CREATE, DUDER 🙎‍♂️");
    }

    _repository.DestroyPicture(pictureId);

    return "Picture was deleted!";
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}