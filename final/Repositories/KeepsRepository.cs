using System;
using System.Data;
using final.Models;

namespace final.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal int Put(Keep kData)
    {
      throw new NotImplementedException();
    }

    internal int Post(Keep newKeep)
    {
      throw new NotImplementedException();
    }

    internal Keep GetOne(int id)
    {
      throw new NotImplementedException();
    }

    internal Keep GetAll()
    {
      throw new NotImplementedException();
    }
  }
}