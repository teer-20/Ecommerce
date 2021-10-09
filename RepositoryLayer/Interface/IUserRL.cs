using ECommerce.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.RepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<UserAddResponse> UserAddress(UserAddRequest request);
       // Task<UserLoginResponse> UserLogin(UserLoginRequest request);
    }
}
