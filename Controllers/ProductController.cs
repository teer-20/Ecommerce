using ECommerce.CommonLayer.Model;
using ECommerce.ServiceLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ECommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductSL _serviceLayer;
        public readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger,IProductSL serviceLayer)
        {
            //_productSL = ProductSL;
            _serviceLayer = serviceLayer;
            _logger = logger;
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProductDetails(AddProductRequest request)
        {
          //  _logger.LogInformation("Enter in Admin Register controller");
            bool Success = true;
            AddProductResponse response = null;
            try
            {
                response = await this._serviceLayer.AddProduct(request);
                if (response.IsSuccess == false)
                {
                   // _logger.LogError($"Error Occur");
                    bool Status = false;
                    return BadRequest(new { Status });
                }


            }
            catch (Exception ex)
            {
                //_logger.LogError($"Signup Error Occur=>{ex}");
                bool Status = false;
                return BadRequest(new { Status, Message = ex.Message });
            }
            return Ok(new { Success, Message = "Product added successfully", data = response });
        }


        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("UpdateProduct/{ProductId}")]
        public async Task<IActionResult> UpdateProductDetails(UpdateProductRequest request)
        {
            UpdateProductResponse response = null;
            bool Success = true;

            try
            {
                response = await this._serviceLayer.UpdateProduct(request);
            }
            catch (Exception ex)
            {
                bool Status = false;

                return BadRequest(new { Status, Message = ex.Message });

            }
            return Ok(new { Success, Message = "Product Updated successfully", data = response });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteProduct/{ProductId}")]
        public async Task<IActionResult> DeleteEmployeeDetails(DeleteProductRequest request)
        {
            DeleteProductResponse response = null;

            try
            {
                response = await this._serviceLayer.DeleteProduct(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProductDetails()
        {

            var resultList = new List<GetAllProductsResponse>();
            try
            {
                resultList = await this._serviceLayer.GetAllProducts();
            }
            catch (Exception ex)
            {

            }
            return Ok(resultList);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("GetProduct/ProductName")]
        public async Task<IActionResult> GetProductByNameDetails(GetProductByNameRequest request)
        {
            GetProductByNameResponse response = null;

            try
            {
                response = await this._serviceLayer.GetProductByName(request);
            }
            catch (Exception ex)
            {

            }
            return Ok(response);
        }

        

    }
}
