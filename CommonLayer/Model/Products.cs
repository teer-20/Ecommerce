using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.CommonLayer.Model
{
   
        public class AddProductRequest
        {
            public string ProductName { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public bool IsActive { get; set; }
       // public DateTime AddedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }

        // public string Images { get; set; }

    }

    public class AddProductResponse
        {
            // public string Email { get; set; }
           //  public string Role { get; set; }
            //public string token { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public bool IsActive { get; set; }

    }

    //============ Update Product  =================
    public class UpdateProductRequest
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
        // public string Images { get; set; }
        public bool IsActive { get; set; }

    }

    public class UpdateProductResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        //public bool IsActive { get; set; }


    }

    //=============  Delete Product =============
    public class DeleteProductRequest
        {
            public int ProductId { get; set; }
        }
        public class DeleteProductResponse
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
       // public bool IsActive { get; set; }


    }

    // =========  Get All Products   =====================

    public class GetAllProductsResponse
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public bool IsActive { get; set; }

    }



    //========== Get Product by ID =================
    public class GetProductByNameRequest
        {
        public string ProductName { get; set; }
        public bool IsActive { get; set; }

    }
    public class GetProductByNameResponse
        {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }


    }


    //========  Delete All Products ============


    //public class FilterProductReq
    //{
    //    public string Type { get; set; }
    //     public double Price { get; set; }
    //     public string PriceSubType { get; set; }

    //    public string Category { get; set; }

    //    //public string Images { get; set; }
    //    public bool IsAvailable { get; set; }

    //}
    //public class AddProductRes
    //{
    //    public bool IsSuccess { get; set; }
    //}

}

