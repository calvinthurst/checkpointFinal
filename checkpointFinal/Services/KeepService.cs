namespace checkpointFinal.Services;

public class KeepService
{
  private readonly KeepRepository _repo;

  public KeepService(KeepRepository repo)
  {
    _repo = repo;
  }

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repo.CreateKeep(keepData);
    return keep;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId, userId);
    if (keep.creatorId != userId) throw new Exception("Thats not your keep");
    _repo.DeleteKeep(keepId);
    return $"{keep.name} has been deleted";
  }

  internal Keep GetKeepById(int keepId, string accountId)
  {
    Keep keep = _repo.GetKeepById(keepId);
    if (keep == null) throw new Exception("No keep at that Id");
    if (accountId != null)
    {
      keep.views++;
      _repo.UpdateKeep(keep);
    }
    return keep;
  }

  internal List<Keep> GetKeeps()
  {
    List<Keep> keeps = _repo.GetKeeps();
    return keeps;
  }

  internal Keep UpdateKeep(Keep updateData)
  {
    Keep original = _repo.GetKeepById(updateData.id);
    if (original.creatorId != updateData.creatorId) throw new Exception("That isn't your keep to edit");
    original.name = updateData.name ?? original.name;
    original.description = updateData.description ?? original.description;
    original.img = updateData.img ?? original.img;
    _repo.UpdateKeep(original);
    return original;
  }
}
