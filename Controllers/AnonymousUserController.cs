using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruYumWebAPI.Model;

namespace TruYumWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AnonymousUserController : ControllerBase
    { 
        MenuItemOperation menuItemoper = new MenuItemOperation();


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAlldata()
        {
            return Ok(menuItemoper.GetData());

        }

       
    }
}
