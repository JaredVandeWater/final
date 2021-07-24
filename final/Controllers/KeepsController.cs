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

  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _ks;
    private readonly AccountService _ass;

    public KeepsController(KeepsService ks, AccountService ass)
    {
      _ks = ks;
      _ass = ass;
    }



    [HttpPost]
    [Authorize]
    async public Task<ActionResult<Keep>> Post([FromBody] Keep newKeep)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = account.Id;
        return Ok(_ks.Post(newKeep));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetAll()
    {
      try
      {
        return Ok(_ks.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{id}")]
    public ActionResult<Keep> GetOne(int id)
    {
      try
      {
        Keep keep = _ks.GetOne(id);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Keep>> Put(int id, [FromBody] Keep kData)
    {
      try
      {
        Account account = await HttpContext.GetUserInfoAsync<Account>();
        kData.Id = id;
        kData.CreatorId = account.Id;
        return Ok(_ks.Put(kData));
      }
      catch (System.Exception)
      {

        throw;
      }
    }
  }
}

