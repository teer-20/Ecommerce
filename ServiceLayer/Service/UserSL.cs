using ECommerce.CommonLayer.Exceptions;
using ECommerce.CommonLayer.Model;
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
    public class UserSL : IUserSL
    {
        public readonly ILogger<UserSL> _logger;
        public readonly IConfiguration _configuration;

        public readonly IUserRL _UserRL;
       // public readonly string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
       // public readonly string Mobile = @"^[2-9]{2}[0-9]{8}$";
        public UserSL(IUserRL UserRL, ILogger<UserSL>logger, IConfiguration configuration)  //Dependency Injection /Constructor
        {
            _logger = logger;
            _UserRL = UserRL;
            _configuration = configuration;
        }


        

        public async Task<UserAddResponse> UserAddress(UserAddRequest request)
        {
            UserAddResponse response = new UserAddResponse();

            try
            {
                //if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password) || String.IsNullOrEmpty(request.Mobile) || String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty((request.DoB).ToString()) || String.IsNullOrEmpty(request.Gender))
                //{
                //    throw new Exception(UserException.ExceptionType.Null_Empty_String_Exception.ToString());
                //}

                //Regex regexMail = new Regex(strRegex);
                //RegexOptions.CultureInvariant | RegexOptions.Singleline;
                //if (!(regexMail.IsMatch(request.Email)))
                //{
                //    response.Message = "Invalid Mail-ID";
                //    response.IsSuccess = false;
                //    return response;
                //}

                
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error Occur in Signup validation part");

                response.IsSuccess = false;
            }

            response = await _UserRL.UserAddress(request);
            return response;

        }
        
        
        //public async Task<UserLoginResponse> UserLogin(UserLoginRequest request)
        //{
        //    UserLoginResponse response = new UserLoginResponse();
        //    try
        //    {
        //        if (String.IsNullOrEmpty(request.Email) || String.IsNullOrEmpty(request.Password))
        //        {
        //            throw new Exception(UserException.ExceptionType.Password_Not_Match_Exception.ToString());
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        _logger.LogError($"Error Occur in login validation part");

        //    }

        //    response = await _UserRL.UserLogin(request);
        //    if (response.IsSuccess == true)
        //    {

        //        string key = _configuration["Jwt:Key"];
        //        string Issuer = _configuration["Jwt:Issuer"];
        //        response.token = Processor.TokenProcessing.CreateToken(response.Role, response.Email, key, Issuer);
        //    }
        //    return response;

        //}
    }
}
