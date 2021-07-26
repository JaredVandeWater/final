using System;
using final.Models;
using final.Repositories;

namespace final.Services
{
  public class VaultKeepsService
  {
    private readonly VaultsRepository _vRepo;
    private readonly VaultKeepsRepository _vkRepo;

    public VaultKeepsService(VaultsRepository vRepo, VaultKeepsRepository vkRepo)
    {
      _vRepo = vRepo;
      _vkRepo = vkRepo;
    }

    public VaultKeep Post(VaultKeep newVaultKeep)
    {
      Vault v = _vRepo.GetOne(newVaultKeep.VaultId);
      newVaultKeep.Id = _vkRepo.Post(newVaultKeep);
      return newVaultKeep;
    }
  }

}