using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using final.Models;
using final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly AccountService _ass;
    private readonly VaultKeepsService _vks;

    public VaultsController(VaultsService vs, AccountService ass, VaultKeepsService vks)
    {
      _vs = vs;
      _ass = ass;
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Vault>> Post([FromBody] Vault newVault)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = account.Id;
        return Ok(_vs.Post(newVault));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{id}")]
    async public Task<ActionResult<Vault>> GetOne(int id)
    {
      try
      {
        bool signedIn = false;
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        string accountId = "null";
        if (account != null)
        {
          accountId = account.Id;
          signedIn = true;
        }
        else signedIn = false;

        Vault vault = _vs.GetOne(id, accountId, signedIn);
        return Ok(vault);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Vault>> Put(int id, [FromBody] Vault kData)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        kData.Id = id;
        Vault k = _vs.Put(kData, account.Id);
        return Ok(k);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [Authorize]
    [HttpDelete("{id}")]

    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_vs.Delete(id, account.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }

    [HttpGet("{id}/keeps")]
    async public Task<ActionResult<List<Keep>>> GetKeepsByVaultId(int id)
    {
      try
      {
        bool signedIn = false;
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        string accountId = "null";
        if (account != null)
        {
          accountId = account.Id;
          signedIn = true;
        }
        else signedIn = false;

        List<Keep> Keeps = _vks.GetKeepsByVaultId(id, accountId, signedIn);
        return Ok(Keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}

