using ECommerce.CommonLayer.Exceptions;
using ECommerce.CommonLayer.Model;
using ECommerce.Processor;
using ECommerce.RepositoryLayer.Interface;
using ECommerce.RepositoryLayer.Service;
using ECommerce.ServiceLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.ServiceLayer.Service
{
    public class AdminSL : IAdminSL
    {
        public readonly ILogger<AdminSL> _logger;
        public readonly IConfiguration _configuration;

        public readonly IAdminRL _AdminRL;
        public readonly string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        public readonly string Mobile = @"^[2-9]{2}[0-9]{8}$";

        public object CustomExceptions { get; private set; }

        public AdminSL(IAdminRL AdminRL, ILogger<AdminSL> logger, IConfiguration configuration)  //Dependency Injection /Constructor
        {
            _logger = logger;
            _AdminRL= AdminRL;
            _configuration = configuration;
        }

       //Register
        public async Task<AdminRegResponse> AdminRegister(AdminRegRequest request)
        {
            AdminRegResponse response = new AdminRegResponse();

            try
            {
                if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password) || String.IsNullOrEmpty(request.Mobile) || String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty((request.DoB).ToString()) || String.IsNullOrEmpty(request.Gender))
                {
                    throw new Exception(AdminException.ExceptionType.Null_Empty_String_Exception.ToString());
                }

                Regex regexMail = new Regex(strRegex);
                //RegexOptions.CultureInvariant | RegexOptions.Singleline;
                if (!(regexMail.IsMatch(request.Email)))
                {
                    response.Message = "Invalid Mail-ID";
                    response.IsSuccess = false;
                    return response;
                }

                Regex regexMobile = new Regex(Mobile);
                if (!regexMobile.IsMatch(request.Mobile))
                {
                    response.Message = "Invalid Mobile";
                    response.IsSuccess = false;
                    return response;

                }
                // Minimum eight characters, at least one uppercase letter, one lowercase letter, one number and one special character
                Regex regexPassword = new Regex(@"^(?=\S*[a-z])(?=\S*[A-Z])(?=\S*\d)(?=\S*[^\w\s])\S{8,}$");
                if (!regexPassword.IsMatch(request.Password))
                {
                    response.Message = "Invalid Password: Minimum 8 characters, atleast 1 uppercase, 1 lowercase, 1 number and 1 special character";
                    response.IsSuccess = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occur in Signup validation part");

                response.IsSuccess = false;
            }

            response = await _AdminRL.AdminRegister(request);
            return response;

        }

        //Login
        public async Task<AdminLoginResponse> AdminLogin(AdminLoginRequest request)
        {
            AdminLoginResponse response = new AdminLoginResponse();
            try
            {
                if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password))
                {
                    throw new Exception(AdminException.ExceptionType.Password_Not_Match_Exception.ToString());
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                _logger.LogError($"Error Occur in login validation part");

            }

            response = await _AdminRL.AdminLogin(request);
            if (response.IsSuccess == true)
            {

                string key = _configuration["Jwt:Key"];
                string Issuer = _configuration["Jwt:Issuer"];
                response.token = Processor.TokenProcessing.CreateToken(response.Role, response.Email, key, Issuer);
            }
            return response;


        }


        //Forget Password
       

        public async Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request)
        {
            ForgetPasswordResponse response = new ForgetPasswordResponse();
            try
            {
                _logger.LogInformation("Enter in ForgetPassword Service layer");
                if (String.IsNullOrEmpty(request.Email))
                {
                    throw new Exception(AdminException.ExceptionType.Password_Not_Match_Exception.ToString());
                }
                response = await _AdminRL.ForgetPassword(request);
               // res = await _ForgetPasswordRL.ForgetPassword(req);
                if (response.IsSuccess == true)
                {
                    string Key = _configuration["Jwt:Key"];
                    string Issuer = _configuration["Jwt:Issuer"];
                    response.token = TokenProcessing.CreateToken( "FP" + response.Role,request.Email, Key, Issuer);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($" service layer exception (ForgetPassword):{ex}");
                response.IsSuccess = false;
            }

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();
            try
            {
                _logger.LogInformation("Enter in ResetPassword Service layer");
                if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.NewPassword) || String.IsNullOrEmpty(request.ConfirmPassword))
                {
                    throw new Exception(AdminException.ExceptionType.Password_Not_Match_Exception.ToString());
                }
                response = await _AdminRL.ResetPassword(request);
                if (response.IsSuccess == true)
                {
                    string Key = _configuration["Jwt:Key"];
                   string Issuer = _configuration["Jwt:Issuer"];
                //    //    res.Token = TokenProcessing.CreateToken(res.Email, res.Role, Key, Issuer);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($" service layer exception (ResetPassword):{ex}");
                response.IsSuccess = false;
            }

            return response;

        }
    }
}
