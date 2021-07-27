using final.Models;
using final.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly AccountService _ass;

    public ProfilesController(ProfilesService ps, AccountService ass)
    {
      _ps = ps;
      _ass = ass;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> GetOne(int id)
    {
      try
      {
        Profile profile = _ps.GetOne(id);
        return Ok(profile);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}