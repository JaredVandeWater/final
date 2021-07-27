using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using final.Models;
using final.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    [Authorize]
    async public Task<ActionResult<VaultKeep>> Post([FromBody] VaultKeep newVaultKeepData)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeepData.CreatorId = account.Id;
        return Ok(_vks.Post(newVaultKeepData));
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
        return Ok(_vks.Delete(id, account.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }

    }
  }
}