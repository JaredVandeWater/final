using System;
using System.Collections.Generic;
using final.Models;
using final.Repositories;

namespace final.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;
    private readonly VaultsRepository _vRepo;

    public ProfilesService(ProfilesRepository repo, VaultsRepository vRepo)
    {
      _repo = repo;
      _vRepo = vRepo;
    }

    internal string GetProfileEmailById(string id)
    {
      return _repo.GetById(id).Email;
    }
    internal Profile GetProfileByEmail(string email)
    {
      return _repo.GetByEmail(email);
    }
    internal Profile GetOrCreateProfile(Profile userInfo)
    {
      Profile profile = _repo.GetById(userInfo.Id);
      if (profile == null)
      {
        return _repo.Create(userInfo);
      }
      return profile;
    }

    internal Profile Edit(Profile editData, string userEmail)
    {
      Profile original = GetProfileByEmail(userEmail);
      original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
      original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
      return _repo.Edit(original);
    }

    public Profile GetOne(string id)
    {
      Profile profile = _repo.GetOne(id);
      if (profile != null)
      {
        return profile;
      }
      throw new Exception("Cannot Get Profile - Id does not exist");
    }



    public List<Keep> GetKeepsByProfileId(string id, string accountId, bool signedIn)
    {

      List<Keep> Keep = _repo.GetKeepsByProfileId(id);

      return Keep;

    }


    public List<Vault> GetVaultsByProfileId(string id, string accountId, bool signedIn)
    {
      {
        List<Vault> vaults = _repo.GetVaultsByProfileId(id);

        return vaults;
      }
    }
  }
}



