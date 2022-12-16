using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Request
{
    public class UpdateTransactionRequest
    {
        public int HourDiscount { get; set; }
        public int TransactionId { get; set; }
    }
}
