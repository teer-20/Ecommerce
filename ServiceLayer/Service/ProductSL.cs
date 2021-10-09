using ECommerce.CommonLayer.Model;
using ECommerce.RepositoryLayer.Interface;
using ECommerce.ServiceLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ServiceLayer.Service
{
    
        public class ProductSL : IProductSL
        {
            public readonly IProductRL _productRL;
             public readonly ILogger<ProductSL> _logger;
            public readonly IConfiguration _configuration;
        public ProductSL(IProductRL ProductRL, ILogger<ProductSL> logger, IConfiguration configuration)  //Dependency Injection /Constructor
            {
            _logger = logger;
            _productRL = ProductRL;
            _configuration = configuration;
        }

            public async Task<AddProductResponse> AddProduct(AddProductRequest request)
            {
            AddProductResponse response = null;
            response = await _productRL.AddProduct(request);
            //if (response.IsSuccess == true)
            //{

            //   string key = _configuration["Jwt:Key"];
            //  string Issuer = _configuration["Jwt:Issuer"];
            //    response. = Processor.TokenProcessing.CreateToken(response.Role, response.Email, key, Issuer);
            //}
            return response;
            }
            public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
            {
            UpdateProductResponse response = null;
            response = await _productRL.UpdateProduct(request);
                return response;
            }
            public async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request)
            {
            DeleteProductResponse response = null;
            response = await _productRL.DeleteProduct(request);
                return response;
            }

            public async Task<List<GetAllProductsResponse>> GetAllProducts()
            {
                var resultList = new List<GetAllProductsResponse>();
                resultList = await _productRL.GetAllProducts();
                return resultList;
            }

            public async Task<GetProductByNameResponse> GetProductByName(GetProductByNameRequest request)
            {
            GetProductByNameResponse response = null;
            response = await _productRL.GetProductByName(request);
                return response;
            }

        
    }
    }

