using System;
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

    public Keep GetAll()
    {
      return _kRepo.GetAll();
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

    public Keep Put(Keep kData)
    {
      Keep original = GetOne(kData.Id);
      if (original.CreatorId == kData.CreatorId)
      {
        kData.Name = kData.Name ?? original.Name;
        kData.Description = kData.Description ?? original.Description;
        kData.Img = kData.Img ?? original.Img;
        kData.Views += 1;

        if (_kRepo.Put(kData) > 0)
        {
          return kData;
        }
        throw new Exception("Something Failed on this update");
      }
      throw new Exception("Incorrect User is trying to update");
    }
  }
}