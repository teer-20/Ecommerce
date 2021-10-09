using ECommerce.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.RepositoryLayer.Interface
{
    public interface IAdminRL
    {
        Task<AdminRegResponse> AdminRegister(AdminRegRequest request);
        Task<AdminLoginResponse> AdminLogin(AdminLoginRequest request);
        Task<ForgetPasswordResponse> ForgetPassword(ForgetPasswordRequest request);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
    }
}
