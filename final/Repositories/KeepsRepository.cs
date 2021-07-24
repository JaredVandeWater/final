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
    internal int Put(Keep kData)
    {
      string sql = @"
      UPDATE keeps SET
      name = @Name
      description = @Description
      img = @Img
      WHERE id = @id;";
      return _db.Execute(sql, kData);
    }


    // create a keep
    internal int Post(Keep newKeep)
    {
      string sql = @"
      INSERT INTO keeps
      (creatorId, name, description, img)
      VALUES( @CreatorId, @Name, @Description, @Img);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newKeep);
    }


    // get one keep
    internal Keep GetOne(int id)
    {
      string sql = @"
      SELECT * FROM keeps
      WHERE id = @id;";
      return _db.QueryFirstOrDefault<Keep>(sql, new { id });
    }



    // gets all keeps and adds profile to them
    public List<Keep> GetAll()
    {
      string sql = @"
                 SELECT 
                    k.*,
                    a.*
                FROM keeps k
                JOIN accounts a ON k.creatorId = a.id;
            ";
      return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
           {
             k.Creator = p;
             return k;
           }, splitOn: "id").ToList();
    }
  }
}


