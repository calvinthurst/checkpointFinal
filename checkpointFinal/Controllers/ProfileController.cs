namespace checkpointFinal.Controllers;

[ApiController]
[Route("api/profiles")]
public class ProfileController : ControllerBase
{
  private readonly ProfileService _profileService;
  private readonly KeepService _keepService;
  private readonly Auth0Provider _auth;

  public ProfileController(ProfileService profileService, Auth0Provider auth, KeepService keepService)
  {
    _profileService = profileService;
    _auth = auth;
    _keepService = keepService;
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Account>> GetProfile(string id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Account profile = _profileService.GetProfile(id);
      return profile;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{accountId}/keeps")]
  public async Task<ActionResult<List<Keep>>> GetKeepsByProfileId(string accountId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Keep> keeps = _keepService.GetKeepsByAccountId(accountId);
      return keeps;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
