using final.Services;
using Microsoft.AspNetCore.Mvc;

namespace final.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
  }
}