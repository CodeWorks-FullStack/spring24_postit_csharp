namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  private readonly AlbumsService _albumsService;
  private readonly Auth0Provider _auth0Provider;
  private readonly PicturesService _picturesService;
  private readonly AlbumMembersService _albumMembersService;

  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider, PicturesService picturesService, AlbumMembersService albumMembersService)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
    _picturesService = picturesService;
    _albumMembersService = albumMembersService;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet]
  public ActionResult<List<Album>> GetAllAlbums()
  {
    try
    {
      List<Album> albums = _albumsService.GetAllAlbums();
      return Ok(albums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  // NOTE example of soft delete, maybe not a great reference for your checkpoints going forward
  [Authorize]
  [HttpDelete("{albumId}")]
  public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbum(albumId, userInfo.Id);
      return Ok(album);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
      return Ok(pictures);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpGet("{albumId}/albumMembers")]
  public ActionResult<List<MemberProfile>> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    try
    {
      List<MemberProfile> albumMemberProfiles = _albumMembersService.GetAlbumMemberProfilesByAlbumId(albumId);
      return Ok(albumMemberProfiles);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}