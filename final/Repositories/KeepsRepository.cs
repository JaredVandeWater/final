using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
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

    // edit a keep
    public Keep Put(Keep kData)
    {
      string sql = @"
      UPDATE keeps 
      SET
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @id;";
      var rowsAffected = _db.Execute(sql, kData);
      if (rowsAffected > 1)
      {
        throw new Exception("More than one row updated, not good.");
      }
      if (rowsAffected < 1)
      {
        throw new Exception("Update Failed");
      }
      return kData;
    }


    // create a keep
    public int Post(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img)
      VALUES( @CreatorId, @Name, @Description, @Img);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }


    // get one keep
    public Keep GetOne(int id)
    {
      string sql = @"
                SELECT 
                    k.*,
                    a.*
                FROM keeps k
                JOIN accounts a ON k.creatorId = a.id
                WHERE k.id = @id;";

      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
          k.Creator = p;
          return k;
        }, new { id }).FirstOrDefault();
    }

    public int Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps
      WHERE id = @id;";
      return _db.Execute(sql, new { id });
    }



    // gets all keeps and adds profile to them
    public List<Keep> GetAll()
    {
      string sql = @"
                SELECT 
                    k.*,
                    a.*
                FROM keeps k
                JOIN accounts a ON k.creatorId = a.id;";

      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
      {
        k.Creator = p;
        return k;
      }, splitOn: "id").ToList();
    }
  }
}


