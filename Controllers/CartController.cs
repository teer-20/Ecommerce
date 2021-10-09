using ECommerce.CommonLayer.Model;
using ECommerce.ServiceLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        public readonly ICartSL _serviceLayer;

        public CartController(ICartSL serviceLayer)
        {
            _serviceLayer = serviceLayer;

        }


       [AllowAnonymous]
        [HttpPost]
        [Route("AddtoCart")]
        public async Task<IActionResult> AddToCart(AddToCartRequest request)
        {
            AddToCartResponse response = null;
            try
            {
                response = await this._serviceLayer.AddToCart(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("UpdateCart/{CartId}")]
        public async Task<IActionResult> UpdateCart(UpdateCartRequest request)
        {
            UpdateCartResponse response = null;

            try
            {
                response = await this._serviceLayer.UpdateCart(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpDelete]
        [Route("DeleteFromCart/{CartId}")]
        public async Task<IActionResult> DeleteProductDetails(DeleteFromCartRequest request)
        {
            DeleteFromCartResponse response = null;

            try
            {
                response = await this._serviceLayer.DeleteFromCart(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetCart")]
        public async Task<IActionResult> GetAllCart(GetAllCartRequest request)
        {

            var resultList = new List<GetAllCartResponse>();
            try
            {
                resultList = await this._serviceLayer.GetAllCart(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(resultList);
        }
    }
}
