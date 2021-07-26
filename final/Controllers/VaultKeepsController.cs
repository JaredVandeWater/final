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
    async public Task<ActionResult<Keep>> Post([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = account.Id;
        return Ok(_vks.Post(newVaultKeep));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}