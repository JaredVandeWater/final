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

      var keep = _kRepo.GetOne(id);

      if (keep != null)
      {

        return keep;
      }
      throw new Exception("Not sure how this is null, you just made it.");

    }



    public Keep GetOne(int id)
    {
      Keep keep = _kRepo.GetOne(id);

      if (keep != null)
      {
        keep.Views += 1;
        Keep updatedKeep = _kRepo.AddView(keep);
        return keep;
      }
      throw new Exception("Cannot Get Keep - Id does not exist");
    }

    public Keep Put(Keep kData, string accountId)
    {
      Keep keep = _kRepo.GetOne(kData.Id);
      if (keep == null)
      {
        throw new Exception("bad id");
      }
      if (keep.CreatorId != accountId)
      {


        throw new Exception("incorrect user");
      }
      keep.Name = kData.Name ?? keep.Name;
      keep.Description = kData.Description ?? keep.Description;
      keep.Img = kData.Img ?? keep.Img;


      return _kRepo.Put(keep);
    }

    public List<Keep> GetAll()
    {
      {
        return _kRepo.GetAll();
      }
    }

    public string Delete(int id, string accountId)
    {
      Keep keep = GetOne(id);
      if (keep?.CreatorId == accountId)
      {
        if (_kRepo.Delete(id) > 0)
        {
          return "Deleted";
        }
        throw new Exception("Bad Id");
      }
      throw new Exception("Unauthorized");
    }
  }
}