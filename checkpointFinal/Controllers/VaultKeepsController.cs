namespace checkpointFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultKeepsController(Auth0Provider auth, VaultKeepsService vaultKeepsService)
  {
    _auth = auth;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeeps>> Create([FromBody] VaultKeeps vaultKeepsData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultKeepsData.creatorId = userInfo.Id;
      VaultKeeps vaultKeeps = _vaultKeepsService.CreateVaultKeep(vaultKeepsData);
      return Ok(vaultKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<VaultKeeps>> Delete(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultKeepsService.DeleteVaultKeep(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
