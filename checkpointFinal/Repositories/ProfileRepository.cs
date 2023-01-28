namespace checkpointFinal.Repositories;

public class ProfileRepository
{

  private readonly IDbConnection _db;

  public ProfileRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Account GetProfile(string id)
  {
    string sql = @"
    SELECT * FROM accounts
    WHERE id = @id
    ";
    Account profile = _db.Query<Account>(sql, new { id }).FirstOrDefault();
    return profile;
  }
}
