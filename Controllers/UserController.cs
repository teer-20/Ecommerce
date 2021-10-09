using ECommerce.CommonLayer.Model;
using ECommerce.ServiceLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserSL _serviceLayer;
        public readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserSL serviceLayer)
        {
            _logger = logger;
            _serviceLayer = serviceLayer;

        }

        [HttpPost]
        [Route("UserAddress")]
        public async Task<IActionResult> UserAddressDetails(UserAddRequest request)
        {
            _logger.LogInformation("Enter in User Register controller");
            UserAddResponse response = null;
            bool Success = true;

            try
            {
                response = await this._serviceLayer.UserAddress(request);
                if (response.IsSuccess == false)
                {
                    _logger.LogError($"Error Occur");
                    bool Status = false;
                    return BadRequest(new { Status });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Signup Error Occur=>{ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { Success, Message = "User Address added successfully", data = response });
        }


        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> LoginUserDetails(UserLoginRequest request)
        //{
        //    UserLoginResponse response = null;
        //    bool LoginSuccess = true;
        //    _logger.LogInformation("Enter in User Login controller");

        //    try
        //    {
        //        response = await this._serviceLayer.UserLogin(request);
        //        if (response.IsSuccess == false)
        //        {
        //            _logger.LogError($"Error Occur in login part");
        //            bool Status = false;
        //            return BadRequest(new { Status });
        //        }
        //        response = await this._serviceLayer.UserLogin(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"login Error Occur => {ex}");
        //        bool Status = false;
        //        return BadRequest(new { Status, Message = ex.Message });
        //    }
        //    return Ok(new { LoginSuccess, Message = "User Logged In successfully", data = response });
        //}






    }
}
