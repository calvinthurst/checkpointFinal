namespace checkpointFinal.Repositories;

public class VaultKeepsRepository
{
  private readonly IDbConnection _db;

  public VaultKeepsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal VaultKeeps CreateVaultKeep(VaultKeeps vaultKeepsData)
  {
    string sql = @"
    INSERT INTO VaultKeep
    (creatorId, vaultId, keepId)
    VALUES
    (@creatorId, @vaultId, @keepId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, vaultKeepsData);
    vaultKeepsData.id = id;
    return vaultKeepsData;

  }

  internal bool DeleteVaultKeep(int id)
  {
    string sql = @"
    DELETE from VaultKeep
    WHERE id = @id
    ";
    int rows = _db.Execute(sql, new { id });
    return rows > 0;
  }

  internal List<KeepVault> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT 
    ke.*,
    vk.*,
    ac.*
    FROM VaultKeep vk
    JOIN keeps ke ON ke.id = vk.keepId
    JOIN accounts ac ON ke.creatorId = ac.id
    WHERE vk.vaultId = @vaultId
    ";
    List<KeepVault> vaultKeeps = _db.Query<KeepVault, VaultKeeps, Account, KeepVault>(sql, (ke, vk, ac) =>
    {
      ke.vaultKeepId = vk.vaultId;
      ke.creator = ac;
      return ke;
    }, new { vaultId }).ToList();
    return vaultKeeps;
  }

  internal VaultKeeps GetOneVaultKeep(int id)
  {
    string sql = @"
    SELECT * FROM VaultKeep
    WHERE id = @id
    ";
    VaultKeeps vaultKeep = _db.Query<VaultKeeps>(sql, new { id }).FirstOrDefault();
    return vaultKeep;
  }
}
