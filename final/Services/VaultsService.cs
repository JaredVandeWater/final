using System;
using System.Collections.Generic;
using final.Models;
using final.Repositories;

namespace final.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vRepo;

    public VaultsService(VaultsRepository vRepo)
    {
      _vRepo = vRepo;
    }

    public Vault Post(Vault newVault)
    {
      int id = _vRepo.Post(newVault);
      newVault.Id = id;

      var vault = _vRepo.GetOne(id);

      if (vault != null)
      {
        return vault;
      }
      throw new Exception("Not sure how this is null, you just made it.");

    }



    public Vault GetOne(int id, string AccountId, bool signedIn)
    {
      Vault vault = _vRepo.GetOne(id);
      if (vault != null)
      {
        if (signedIn)
        {

          if (AccountId == vault.CreatorId)
          {
            return vault;
          }


          if (AccountId != vault.CreatorId)
          {
            if (vault.IsPrivate)
            {
              throw new Exception("Cannot Get Vault - Unauthorized");
            }
            if (!vault.IsPrivate)
            {
              return vault;
            }
          }

        }
        if (!signedIn)
        {
          if (vault.IsPrivate)
          {
            throw new Exception("Cannot Get Vault - Unauthorized");
          }
          else
          {
            return vault;
          }
        }


      }
      throw new Exception("Vault Id not found");
    }



    public Vault Put(Vault kData, string accountId)
    {
      Vault vault = _vRepo.GetOne(kData.Id);
      if (vault == null)
      {
        throw new Exception("bad id");
      }
      if (vault.CreatorId != accountId)
      {
        throw new Exception("incorrect user");
      }
      vault.Name = kData.Name ?? vault.Name;
      vault.Description = kData.Description ?? vault.Description;

      return _vRepo.Put(vault);
    }



    public string Delete(int id, string accountId)
    {
      Vault vault = _vRepo.GetOne(id);
      if (vault?.CreatorId == accountId)
      {
        if (_vRepo.Delete(id) > 0)
        {
          return "Deleted";
        }
        throw new Exception("Bad Id");
      }
      throw new Exception("Unauthorized");
    }
  }
}