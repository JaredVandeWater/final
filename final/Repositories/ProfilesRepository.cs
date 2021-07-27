using System;
using System.Data;
using System.Linq;
using Dapper;
using final.Models;

namespace final.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    public Profile GetOne(int id)
    {
      string sql = @"
                SELECT * FROM accounts
                WHERE id = @Id;";

      return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
    }
  }
}
