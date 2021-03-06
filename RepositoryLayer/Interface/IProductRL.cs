using ECommerce.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.RepositoryLayer.Interface
{
    public interface IProductRL
    {
        Task<AddProductResponse> AddProduct(AddProductRequest request);
        Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request);
        Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request);
        Task<List<GetAllProductsResponse>> GetAllProducts();
        Task<GetProductByNameResponse> GetProductByName(GetProductByNameRequest request);
    }
}
