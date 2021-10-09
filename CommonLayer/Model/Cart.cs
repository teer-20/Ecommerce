using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.CommonLayer.Model
{
    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

    }
    public class AddToCartResponse
    {
        public bool IsSuccess { get; set; }
    }
    public class UpdateCartRequest
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateCartResponse
    {
        public bool IsUpdateSuccess { get; set; }
    }
    public class DeleteFromCartRequest
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
    public class DeleteFromCartResponse
    {
        public bool IsDeleteSuccess { get; set; }
    }

    // =========  Get All cart Products   =====================
    public class GetAllCartRequest
    {
        public int UserId { get; set; }
    }
    public class GetAllCartResponse
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Images { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsSuccess { get; set; }

    }
    // Save for later=== indirectly Adding it in the Wishlist
    public class SaveForLaterRequest
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class SaveForLaterResponse
    {
        public bool IsUpdateSuccess { get; set; }
    }



}
