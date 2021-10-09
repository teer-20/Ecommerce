using ECommerce.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.RepositoryLayer.Interface
{
   public interface ICartRL
    {
        Task<AddToCartResponse> AddToCart(AddToCartRequest request);
        Task<UpdateCartResponse> UpdateCart(UpdateCartRequest request);
        Task<DeleteFromCartResponse> DeleteFromCart(DeleteFromCartRequest request);
        Task<List<GetAllCartResponse>> GetAllCart(GetAllCartRequest request);
    }
}
