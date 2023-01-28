namespace checkpointFinal.Services;

public class VaultService
{
  private readonly VaultRepository _repo;

  public VaultService(VaultRepository repo)
  {
    _repo = repo;
  }

  internal Vault CreateVault(Vault vaultData, string accountId)
  {
    Vault vault = _repo.CreateVault(vaultData);
    return vault;
  }

  internal string DeleteVault(int vaultId, string accountId)
  {
    Vault vault = _repo.GetVault(vaultId);
    if (vault.creatorId != accountId) throw new Exception("Thats not your vault");
    _repo.DeleteVault(vaultId);
    return $"{vault.name} has been deleted";
  }

  internal Vault GetVault(int vaultId, string accountId)
  {
    Vault vault = _repo.GetVault(vaultId);
    if (vault == null) throw new Exception("No vault at that id");
    if (vault.isPrivate == true && vault.creatorId != accountId) throw new Exception("that vault is private and is not yours");
    return vault;

  }

  internal List<Vault> GetVaultsByAccountId(string profileId, string accountId)
  {
    List<Vault> vaults = _repo.GetVaultsByProfileId(profileId);
    return vaults;
  }

  internal Vault UpdateVault(Vault vaultData)
  {
    Vault original = _repo.GetVault(vaultData.id);
    if (original.creatorId != vaultData.creatorId) throw new Exception("That vault is not yours");
    original.name = vaultData.name ?? original.name;
    original.description = vaultData.description ?? original.description;
    original.img = vaultData.img ?? original.img;
    original.isPrivate = vaultData.isPrivate;
    _repo.UpdateVault(original);
    return original;
  }
}
