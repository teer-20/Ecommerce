using ECommerce.CommonLayer.Model;
using ECommerce.RepositoryLayer.Interface;
using ECommerce.ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ServiceLayer.Service
{
    public class CartSL : ICartSL
    {

        public  readonly ICartRL _CartRL;
        public CartSL(ICartRL CartRL)    //Defination Injection /Constructor
        {
            _CartRL = CartRL;
        }

        public async Task<AddToCartResponse> AddToCart(AddToCartRequest request)
        {
            AddToCartResponse response = null;
            response = await _CartRL.AddToCart(request);
            return response;
        }

        public async Task<DeleteFromCartResponse> DeleteFromCart(DeleteFromCartRequest request)
        {
            DeleteFromCartResponse response = null;
            response = await _CartRL.DeleteFromCart(request);
            return response;
        }

        public async Task<List<GetAllCartResponse>> GetAllCart(GetAllCartRequest request)
        {
            var resultList = new List<GetAllCartResponse>();
            resultList = await _CartRL.GetAllCart(request);
            return resultList;
        }

        public async Task<UpdateCartResponse> UpdateCart(UpdateCartRequest request)
        {
            UpdateCartResponse response = null;
            response = await _CartRL.UpdateCart(request);
            return response;


        }
    }
}
