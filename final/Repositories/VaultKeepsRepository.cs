using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using final.Models;

namespace final.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    public int Post(VaultKeep newVaultKeep)
    {
      string sql = @"
            INSERT INTO vaultkeep
            (creatorId, vaultId, keepId)
            VALUES
            (@CreatorId, @VaultId, @KeepId);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }

    public List<Keep> GetKeepsByVaultId(int id)
    {
      string sql = @"
      SELECT 
      k.*,
      vk.*
      FROM vaultkeep vk
      JOIN keeps k ON k.id = vk.keepId
      WHERE vk.vaultId = @id;";
      return _db.Query<Keep, VaultKeep, Keep>(sql, (k, kq) =>
      {
        k.vaultKeepId = kq.Id;
        return k;
      }, new { id }, splitOn: "id").ToList();
    }


    public VaultKeep GetOne(int id)
    {
      string sql = @"
                SELECT *     
                FROM vaultkeep
                WHERE Id = @id;";

      return _db.Query<VaultKeep>(sql, new { id }).FirstOrDefault();
    }

    public int Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultkeep
      WHERE id = @id;";
      return _db.Execute(sql, new { id });
    }
  }
}