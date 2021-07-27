using System;
using final.Models;
using final.Repositories;

namespace final.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pRepo;

    public ProfilesService(ProfilesRepository pRepo)
    {
      _pRepo = pRepo;
    }

    public Profile GetOne(int id)
    {
      Profile profile = _pRepo.GetOne(id);
      if (profile != null)
      {
        return profile;
      }
      throw new Exception("Cannot Get Profile - Id does not exist");
    }



  }
}