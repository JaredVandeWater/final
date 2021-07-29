using System;
using System.Collections.Generic;
using final.Models;
using final.Repositories;

namespace final.Services
{
  public class VaultKeepsService
  {
    private readonly VaultsRepository _vRepo;
    private readonly VaultKeepsRepository _vkRepo;
    private readonly KeepsRepository _kRepo;

    public VaultKeepsService(VaultsRepository vRepo, VaultKeepsRepository vkRepo, KeepsRepository kRepo)
    {
      _vRepo = vRepo;
      _vkRepo = vkRepo;
      _kRepo = kRepo;
    }



    // create vaultkeep, get a vault, make sure its not null, and add a keep to it
    public VaultKeep Post(VaultKeep newVaultKeepData)
    {
      Vault vault = _vRepo.GetOne(newVaultKeepData.VaultId);
      if (vault != null && vault.CreatorId == newVaultKeepData.CreatorId)
      {
        int id = _vkRepo.Post(newVaultKeepData);
        VaultKeep vaultKeep = _vkRepo.GetOne(id);
        if (vaultKeep != null)
        {
          Keep keep = _kRepo.GetOne(vaultKeep.KeepId);
          keep.Keeps += 1;
          _vkRepo.EditKeepCount(keep);
          return vaultKeep;
        }
        else
        {
          throw new Exception("VaultKeep Null");
        }
      }
      throw new Exception("Invalid User or Vault is Null");
    }

    public List<Keep> GetKeepsByVaultId(int id, string accountId, bool signedIn)
    {
      Vault vault = _vRepo.GetOne(id);

      if (!vault.IsPrivate)
      {
        List<Keep> Keep = _vkRepo.GetKeepsByVaultId(vault.Id);

        return Keep;
      }
      else if (vault.IsPrivate && accountId == vault.CreatorId)
      {
        List<Keep> Keep = _vkRepo.GetKeepsByVaultId(vault.Id);

        return Keep;
      }
      else
      {

        throw new Exception("Unauthorized or Invalid Vault");
      }




    }

    internal object Delete(int id, string accountId)
    {
      VaultKeep vaultKeep = _vkRepo.GetOne(id);
      if (vaultKeep?.CreatorId == accountId)
      {
        if (_vkRepo.Delete(id) > 0)
        {
          Keep keep = _kRepo.GetOne(vaultKeep.KeepId);
          keep.Keeps -= 1;
          _vkRepo.EditKeepCount(keep);
          return "Deleted";
        }
        throw new Exception("Bad Id");
      }
      throw new Exception("Unauthorized");
    }
  }
}
