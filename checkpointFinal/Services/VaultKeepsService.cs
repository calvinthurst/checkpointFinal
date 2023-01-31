namespace checkpointFinal.Services;

public class VaultKeepsService
{
  private readonly VaultKeepsRepository _repo;
  private readonly VaultService _vaultService;
  private readonly KeepService _keepService;

  public VaultKeepsService(VaultKeepsRepository repo, VaultService vaultService, KeepService keepService)
  {
    _repo = repo;
    _vaultService = vaultService;
    _keepService = keepService;
  }

  internal VaultKeeps CreateVaultKeep(VaultKeeps vaultKeepsData)
  {
    Keep keep = _keepService.GetKeepForCreateVaultKeep(vaultKeepsData.keepId);
    Vault vault = _vaultService.GetVault(vaultKeepsData.vaultId, vaultKeepsData.creatorId);
    if (vaultKeepsData.creatorId != vault.creatorId) throw new Exception("you dont own that vault");
    VaultKeeps vaultKeeps = _repo.CreateVaultKeep(vaultKeepsData);

    return vaultKeeps;
  }

  internal string DeleteVaultKeep(int id, string accountId)
  {
    VaultKeeps vaultKeeps = _repo.GetOneVaultKeep(id);
    Keep keep = _keepService.GetKeepForDeleteVaultKeep(vaultKeeps.keepId);
    Vault vault = _vaultService.GetVault(vaultKeeps.vaultId, vaultKeeps.creatorId);
    if (vaultKeeps.creatorId != accountId) throw new Exception("you didn't add that");
    _repo.DeleteVaultKeep(id);
    return $" keep at {id} has been deleted ";
  }

  internal List<KeepVault> GetVaultKeeps(int vaultId, string accountId)
  {
    Vault vault = _vaultService.GetVault(vaultId, accountId);
    List<KeepVault> keeps = _repo.GetKeepsByVaultId(vaultId);
    return keeps;
  }
}
