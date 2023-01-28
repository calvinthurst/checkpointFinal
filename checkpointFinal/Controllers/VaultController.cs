namespace checkpointFinal.Controllers;
[ApiController]
[Route("api/vaults")]

public class VaultController : ControllerBase
{
  private readonly Auth0Provider _auth;
  private readonly VaultService _vaultService;
  private readonly VaultKeepsService _vaultKeepsService;

  public VaultController(Auth0Provider auth, VaultService vaultService, VaultKeepsService vaultKeepsService)
  {
    _auth = auth;
    _vaultService = vaultService;
    _vaultKeepsService = vaultKeepsService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.creatorId = userInfo.Id;
      Vault vault = _vaultService.CreateVault(vaultData, userInfo.Id);
      vaultData.creator = userInfo;
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Vault>> GetVault(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Vault vault = _vaultService.GetVault(id, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> UpdateVault(int id, [FromBody] Vault vaultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      vaultData.creatorId = userInfo.Id;
      vaultData.id = id;
      Vault vault = _vaultService.UpdateVault(vaultData);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<Vault>> DeleteVault(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _vaultService.DeleteVault(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<KeepVault>>> GetKeepsInVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<KeepVault> keeps = _vaultKeepsService.GetVaultKeeps(vaultId, userInfo?.Id);
      return keeps;
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
