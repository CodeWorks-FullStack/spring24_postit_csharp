
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
}