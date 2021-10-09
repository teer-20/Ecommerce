using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.CommonLayer.Model
{
    public class Orderrequest
    {
        public int CartID { get; set; }

        public int ProductID { get; set; }

        public int UserID { get; set; }

        public bool IsActive { get; set; }

        public int Quantity { get; set; }
    }

    public class Orderresponse
    {

        public bool IsActive { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}
