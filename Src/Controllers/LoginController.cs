/// <summary>
/// Login API
/// </summary>
/// <remarks>
/// for user login, and generate JWT token
/// </remarks>
/// <author>
/// Chi Xu (Peter) -- 10/10/2024
/// </author>
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OXL_Assessment2.Src.Attributes;
using OXL_Assessment2.Src.Constants;
using OXL_Assessment2.Src.Data.Entities;
using OXL_Assessment2.Src.Models;

namespace OXL_Assessment2.Src.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : AbstractBaseController
    {
        private readonly UserManager<NZTUser> _userManager;

        public LoginController(UserManager<NZTUser> userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// normal user login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [ModelStateVerification]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            // try to get user from database by applying UserManager
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            // check if user exists
            if (user == null)
            {
                return NotFound(CreateResponse(ServiceCode.UserNotExist,
                MessageConstants.UserNotExist));
            }
            // check the password by UserManager
            bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (isPasswordCorrect)
            {
                // todo generate JWT token
                return Ok();
            }
            else return Unauthorized(CreateResponse(ServiceCode.PasswordNotCorrect,
            MessageConstants.PasswordNotCorrect));
        }
    }
}
