using Microsoft.AspNetCore.Mvc;
using Shoping.Business;
using Shoping.Data_Access.DTOs;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IConfiguration Iconfiguration;
        private IUserBusiness UserBusiness;
        public UserController(IConfiguration iconfiguration) {
            Iconfiguration = iconfiguration;
            UserBusiness = new UserBusiness(iconfiguration, "MongoDB");
        }

        [HttpPost]
        public async Task<IActionResult> Login(Dictionary<string ,object> dict)
        {
            string Email = dict["Email"].ToString();
            string Password = dict["Password"].ToString();
            var userReponseDTO = await UserBusiness.GetUserAsync(Email, Password);
            return Ok(userReponseDTO);
        }
    }
}
