using System;
using System.Collections.Generic;
using final.Models;
using final.Repositories;

namespace final.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kRepo;

    public KeepsService(KeepsRepository kRepo)
    {
      _kRepo = kRepo;
    }

    public Keep Post(Keep newKeep)
    {
      int id = _kRepo.Post(newKeep);
      newKeep.Id = id;
      return newKeep;
    }



    public Keep GetOne(int id)
    {
      Keep keep = _kRepo.GetOne(id);
      if (keep != null)
      {
        keep.Views++;
        return keep;
      }
      throw new Exception("Cannot Get Keep - Id does not exist");
    }

    public Keep Put(Keep kData, string accountId)
    {
      var keep = _kRepo.GetOne(kData.Id);
      if (keep == null)
      {
        throw new Exception("bad id");
      }
      // is this right??????
      if (keep.CreatorId != accountId)
      {
        throw new Exception("incorrect user");
      }
      group.Name = groupData.Name ?? group.Name;
      group.Img = groupData.Img ?? group.Img;
      group.Description = groupData.Description ?? group.Description;

      return _kRepo.Put(keep);
    }

    public List<Keep> GetAll()
    {
      {
        return _kRepo.GetAll();
      }
    }
  }
}