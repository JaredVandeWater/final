using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using final.Models;

namespace final.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    // edit a vault
    public Vault Put(Vault kData)
    {
      string sql = @"
      UPDATE vaults 
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
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


    // create a vault
    public int Post(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults
      (creatorId, name, description, isPrivate)
      VALUES( @CreatorId, @Name, @Description, @IsPrivate);
      SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVault);
    }


    // get one vault
    public Vault GetOne(int id)
    {
      string sql = @"
                SELECT 
                    v.*,
                    a.*
                FROM vaults v
                JOIN accounts a ON v.creatorId = a.id
                WHERE v.id = @id;";

      return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
          v.Creator = p;
          return v;
        }, new { id }).FirstOrDefault();
    }

    public int Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults
      WHERE id = @id;";
      return _db.Execute(sql, new { id });
    }

  }
}


