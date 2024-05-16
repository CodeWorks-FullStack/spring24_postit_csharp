namespace postit_csharp.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly AlbumMembersService _albumMembersService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, AlbumMembersService albumMembersService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _albumMembersService = albumMembersService;
  }

  [HttpGet]
  [Authorize]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateProfile(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpGet("albumMembers")]
  public async Task<ActionResult<List<AlbumCollaboration>>> GetMyAlbumCollaborations()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<AlbumCollaboration> albumCollaborations = _albumMembersService.GetMyAlbumCollaborations(userInfo.Id);
      return albumCollaborations;
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

}
