using ECommerce.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ServiceLayer.Interface
{
    public interface IUserSL
    {
        Task<UserAddResponse> UserAddress(UserAddRequest request);
       // Task<UserLoginResponse> UserLogin(UserLoginRequest request);
    }
}
