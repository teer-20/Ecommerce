using ECommerce.CommonLayer.Model;
using ECommerce.ServiceLayer.Interface;
using Microsoft.AspNetCore.Authorization;
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
    public class AdminController : ControllerBase
    {
        public readonly IAdminSL _serviceLayer;
        public readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger, IAdminSL serviceLayer)
        {
            _logger = logger;
            _serviceLayer = serviceLayer;

        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> AdminRegisterDetails(AdminRegRequest request)
        {
            _logger.LogInformation("Enter in Admin Register controller");
            AdminRegResponse response = null;
            bool Success = true;

            try
            {
                response = await this._serviceLayer.AdminRegister(request);
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
            return Ok(new { Success, Message = "Registered successfully", data = response });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAdminDetails(AdminLoginRequest request)
        {
            AdminLoginResponse response = null;
            bool LoginSuccess = true;
            _logger.LogInformation("Enter in Admin Login controller");

            try
            {
                response = await this._serviceLayer.AdminLogin(request);
                if (response.IsSuccess == false)
                {
                    _logger.LogError($"Error Occur in login part");
                    bool Status = false;
                    return BadRequest(new { Status });
                }
                response = await this._serviceLayer.AdminLogin(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"login Error Occur => {ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { LoginSuccess, Message = "Admin Logged In successfully", data = response });
        }


        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordRequest request)
        {
            _logger.LogInformation("Enter in ForgetPassword_API");
            ForgetPasswordResponse response = new ForgetPasswordResponse();
            bool Forget = true;
            try
            {
                response = await this._serviceLayer.ForgetPassword(request);
                if (response.IsSuccess == false)
                {
                    _logger.LogError(" Error Occured in Forget password-API");
                    bool Status = false;
                    return BadRequest(new { Status });
                }
                response = await this._serviceLayer.ForgetPassword(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Error occured with :=>{ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { Forget, Message = "Please Note the OTP", data = response });
        }


        [Authorize(Roles = "FPAdmin")]
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPasswordDetails(ResetPasswordRequest request)
        {
            _logger.LogInformation("Enter in ResetPassword_api");
            ResetPasswordResponse response = new ResetPasswordResponse();
            bool IsSuccess = true;
            try
            {
                response = await this._serviceLayer.ResetPassword(request);
                if (response.IsSuccess == false)
                {
                    _logger.LogError(" Error Occured in ResetPassword_api");
                    bool Status = false;
                    return BadRequest(new { Status });
                }
                response = await this._serviceLayer.ResetPassword(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Error occured with :=>{ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { IsSuccess, Message = "Password reset Successfully!", data = response });
        }


    }
}
