namespace checkpointFinal.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly VaultRepository _vaultRepo;

  public AccountService(AccountsRepository repo, VaultRepository vaultRepo)
  {
    _repo = repo;
    _vaultRepo = vaultRepo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    original.coverImg = editData.coverImg.Length > 0 ? editData.coverImg : original.coverImg;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string accountId)
  {
    List<Vault> vaults = _vaultRepo.GetMyVaults(accountId);
    if (vaults == null) throw new Exception("you dont have any vaults");
    return vaults;
  }

}
