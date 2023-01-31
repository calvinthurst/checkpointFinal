namespace checkpointFinal.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly VaultService _vaultService;
  private readonly Auth0Provider _auth0Provider;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, VaultService vaultService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _vaultService = vaultService;
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
  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Account>> Edit([FromBody] Account editData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      editData.Id = userInfo.Id;
      editData.Email = userInfo.Email;
      Account updated = _accountService.Edit(editData, userInfo.Email);
      return Ok(updated);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("vaults")]
  [Authorize]
  public async Task<ActionResult<List<Vault>>> GetAccountVaults()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Vault> vaults = _accountService.GetMyVaults(userInfo.Id);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
