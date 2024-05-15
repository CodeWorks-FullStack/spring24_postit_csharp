


namespace postit_csharp.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);
    if (album == null)
    {
      throw new Exception($"Invalid id: {albumId}");
    }
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    List<Album> albums = _repository.GetAllAlbums();
    return albums;
  }
}