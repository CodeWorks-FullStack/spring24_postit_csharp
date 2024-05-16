namespace postit_csharp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumMembersController : ControllerBase
{
  private readonly AlbumMembersService _albumMembersService;
  private readonly Auth0Provider _auth0Provider;

  public AlbumMembersController(AlbumMembersService albumMembersService, Auth0Provider auth0Provider)
  {
    _albumMembersService = albumMembersService;
    _auth0Provider = auth0Provider;
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<AlbumMember>> CreateAlbumMember([FromBody] AlbumMember albumMemberData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumMemberData.AccountId = userInfo.Id;
      AlbumMember albumMember = _albumMembersService.CreateAlbumMember(albumMemberData);
      return Ok(albumMember);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}