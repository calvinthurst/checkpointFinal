namespace checkpointFinal.Repositories;

public class VaultRepository
{
  private readonly IDbConnection _db;

  public VaultRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Vault CreateVault(Vault vaultData)
  {
    string sql = @"
    INSERT INTO vault
    (name, description, img, creatorId, isPrivate)
    VALUES
    (@name, @description, @img, @creatorId, @isPrivate);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultData);
    vaultData.id = id;
    return vaultData;
  }

  internal bool DeleteVault(int vaultId)
  {
    string sql = @"
    DELETE FROM vault
    WHERE id = @vaultId;
    ";
    int rows = _db.Execute(sql, new { vaultId });
    return rows > 0;
  }

  internal Vault GetVault(int vaultId)
  {
    string sql = @"
    SELECT 
    v.*,
    acc.*
    FROM vault v
    JOIN accounts acc ON v.creatorId = acc.id
    WHERE v.id = @vaultId;
    ";
    Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
      vault.creator = account;
      return vault;
    }, new { vaultId }).FirstOrDefault();
    return vault;
  }

  internal List<Vault> GetVaultsByProfileId(string profileId)
  {
    string sql = @"
    SELECT
    v.*,
    acc.*
    FROM vault v
    JOIN accounts acc ON v.creatorId = acc.id
    WHERE v.creatorId = @profileId && v.isPrivate = false;
    ";
    List<Vault> vaults = _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
    {
      vault.creator = account;
      return vault;
    }, new { profileId }).ToList();
    return vaults;

  }

  internal bool UpdateVault(Vault original)
  {
    string sql = @"
    UPDATE vault SET
    name = @name,
    description = @description,
    img= @img,
    isPrivate= @isPrivate
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, original);
    return rows > 0;

  }
}
