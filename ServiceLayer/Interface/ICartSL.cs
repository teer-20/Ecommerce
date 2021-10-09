using ECommerce.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ServiceLayer.Interface
{
   public  interface ICartSL
    {
        Task<AddToCartResponse> AddToCart(AddToCartRequest request);
        Task<UpdateCartResponse> UpdateCart(UpdateCartRequest request);
        Task<DeleteFromCartResponse> DeleteFromCart(DeleteFromCartRequest request);
        Task<List<GetAllCartResponse>> GetAllCart(GetAllCartRequest request);
    }
}
