using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.CommonLayer.Model
{
    public class AdminRegRequest
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string CPassword { get; set; }
        public DateTime DoB { get; set; }

        public string Gender { get; set; }

        public bool IsActive { get; set; }
        public string Role { get; set; }

    }

    public class AdminRegResponse
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
        // public DateTime DoC { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }

    }

    public class AdminLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        //public bool IsSuccess { get; set; }

        public string Role { get; set; }

    }

    public class AdminLoginResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string token { get; set; }

        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }


    public class ForgetPasswordRequest
    {
        public string Email { get; set; }
        public string Role { get; set; }

    }
    public class ForgetPasswordResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int OTP { get; set; }
        public string token { get; set; }
        public bool IsSuccess { get; set; }

    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

        public int OTP { get; set; }
    }
    public class ResetPasswordResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsSuccess { get; set; }

    }

}
