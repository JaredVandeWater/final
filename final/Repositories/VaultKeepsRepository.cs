using System;
using System.Data;
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
            (CreatorId, VaultId, KeepId);
            SELECT LAST_INSERT_ID();";
      return _db.ExecuteScalar<int>(sql, newVaultKeep);
    }
  }
}