using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using final.Models;
using final.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;

    public ProfilesController(ProfilesService profilesService)
    {
      _profilesService = profilesService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Profile>> Get()
    {
      try
      {
        Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
        return Ok(_profilesService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }



    [HttpGet("{id}")]
    public ActionResult<Profile> GetOne(string id)
    {
      try
      {
        Profile profile = _profilesService.GetOne(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

   

    [HttpGet("{id}/keeps")]
    async public Task<ActionResult<List<Keep>>> GetKeepsByProfileId(string id)
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

        List<Keep> Keeps = _profilesService.GetKeepsByProfileId(id, accountId, signedIn);
        return Ok(Keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpGet("{id}/vaults")]
    async public Task<ActionResult<List<Keep>>> GetVaultsByProfileId(string id)
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

        List<Vault> vaults = _profilesService.GetVaultsByProfileId(id, accountId, signedIn);
        return Ok(vaults);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }


}