using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.CommonLayer.Model
{
    public class UserAddRequest
    {
        public string Address1{ get; set; }
        public string Address2{ get; set; }
        public string City { get; set; }
        public string State { get; set; }
        //public string CPassword { get; set; }
        public string Country { get; set; }

        public int PostalCode { get; set; }

        public bool IsSuccess { get; set; }

        public int UserId { get; set; }
        public bool IsActive { get; set; }
       
        public DateTime DOC { get; set; }
        public DateTime DOM { get; set; }

    }

    public class UserAddResponse
    {
        
        
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        
    }

    //public class UserLoginRequest
    //{
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    //public bool IsSuccess { get; set; }
    //    public bool IsSuccess { get; set; }

    //    public string Role { get; set; }

    //}

    //public class UserLoginResponse
    //{
    //    public int UserId { get; set; }
    //    public string Email { get; set; }
    //    public string Role { get; set; }
    //    public string token { get; set; }

    //    public bool IsActive { get; set; }
    //    public bool IsSuccess { get; set; }
    //    public string Message { get; set; }


   // }
}
