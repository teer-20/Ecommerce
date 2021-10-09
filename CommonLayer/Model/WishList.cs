using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.CommonLayer.Model
{
    public class AddWishToListRequest
    {
        
            public string ProductId { get; set; }
            public string UserId { get; set; }
        public int Quantity { get; set; }

        // public string Category { get; set; }
        //public double Price { get; set; }
        // public DateTime AddedDate { get; set; }
        //public DateTime ModifiedDate { get; set; }

        // public string Images { get; set; }

    }

    public class AddToWishListResponse
    {
        
             public bool IsSuccess { get; set; }
            public string Message { get; set; }

        }

    //============ Update Product  =================
    public class UpdateWishlistRequest
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public string ProductId{ get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateWishlistRes
    {
        public bool IsSuccess { get; set; }
    }

    //=============  Delete Product =============
    public class DeleteFromWishListRequest
        {
            public int WishListId { get; set; }
             //public string UserId { get; set; }


          }
    public class DeleteWishFromListResponse
    {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }


        }

    // =========  Get All Products   =====================
    public class GetAllWishlistReq
    {
        public int UserId { get; set; }
    }
    public class GetAllWishListResponse
    {
        public int WishlistId { get; set; }

        public int ProductId { get; set; }
           // public string ProductName { get; set; }
           // public string Category { get; set; }
           // public double Price { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        public int Quantity { get; set; }

    }



    //========== Get Product by ID =================
    public class AddToCartVWRequest
        {
        public int ProductId { get; set; }

        }
        public class AddToCartVWResponse
    {

            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public double Price { get; set; }
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public bool IsActive { get; set; }


        }


    
}
