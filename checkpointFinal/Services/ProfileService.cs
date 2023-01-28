namespace checkpointFinal.Services;

public class ProfileService
{
  private readonly ProfileRepository _repo;

  public ProfileService(ProfileRepository repo)
  {
    _repo = repo;
  }

  internal Account GetProfile(string id)
  {
    Account profile = _repo.GetProfile(id);
    if (profile == null) throw new Exception("No profile at that id");
    return profile;
  }
}
