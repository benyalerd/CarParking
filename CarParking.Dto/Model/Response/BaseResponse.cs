using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Dto.Model.Response
{
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
