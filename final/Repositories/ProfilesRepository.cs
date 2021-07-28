using System.Data;
using final.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace final.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Profile>(sql, new { userEmail });
    }

    internal Profile GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal Profile Create(Profile newProfile)
    {
      string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newProfile);
      return newProfile;
    }

    internal Profile Edit(Profile update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }

    internal Profile GetOne(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Profile>(sql, new { id });
    }

    internal List<Keep> GetKeepsByProfileId(string id)
    {
      {
        string sql = @"
                SELECT * 
                FROM keeps
               WHERE keeps.creatorId === id;";

        return _db.Query<Keep>(sql).ToList();
      }
    }

    internal List<Vault> GetVaultsByProfileId(string id)
    {
      {
        string sql = @"
                SELECT * 
                FROM vaults
               WHERE vaults.creatorId === id;";

        return _db.Query<Vault>(sql).ToList();
      }
    }
  }
}
