namespace checkpointFinal.Repositories;

public class KeepRepository
{
  private readonly IDbConnection _db;

  public KeepRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    string sql = @"
    INSERT INTO keeps
    (name, description, img, creatorId)
    VALUES
    (@name, @description, @img, @creatorId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, keepData);
    keepData.id = id;
    return keepData;
  }

  internal bool DeleteKeep(int keepId)
  {
    string sql = @"
    DELETE FROM keeps
    WHERE id = @keepId
    ";
    int rows = _db.Execute(sql, new { keepId });
    return rows > 0;
  }

  internal Keep GetKeepById(int keepId)
  {
    string sql = @"
    SELECT 
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON k.creatorId = acc.id
    WHERE k.id = @keepId;
    ";
    Keep keep = _db.Query<Keep, Account, Keep>(sql, (kee, account) =>
    {
      kee.creator = account;
      return kee;
    }, new { keepId }).FirstOrDefault();
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    string sql = @"
    SELECT 
    k.*,
    acc.*
    FROM keeps k
    JOIN accounts acc ON k.creatorId = acc.id
    ";
    List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
    {
      keep.creator = account;
      return keep;
    }).ToList();
    return keeps;
  }

  internal List<KeepVault> GetKeepsByVaultId(int vaultId)
  {
    string sql = @"
    SELECT
    k.*,
    acc.*,
    v.
    FROM keeps k
    JOIN accounts acc ON k.creatorId = acc.id
    JOIN vault v ON k.vaultId = @vaultId
    WHERE k.vaultId
    ";
    List<KeepVault> vaultKeeps = _db.Query<KeepVault, Account, KeepVault>(sql, (vKeep, account) =>
    {
      vKeep.creator = account;
      return vKeep;
    }, new { vaultId }).ToList();
    return vaultKeeps;

  }

  internal bool UpdateKeep(Keep keep)
  {
    string sql = @"
    UPDATE keeps SET 
    name = @name,
    description = @description,
    img = @img,
    views = @views,
    kept = @kept
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, keep);
    return rows > 0;
  }
}
