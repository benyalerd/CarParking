using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using CarParking.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarParking.Controller.Controllers
{
    [Route("api/[controller]")]
    public class FeeController : ControllerBase
    {
        private readonly IFeeService _feeService;
        public FeeController(IFeeService feeService)
        {
            _feeService = feeService;
        }

        [HttpPost]
        [Route("AddFee")]
        public IActionResult AddFee([FromBody] InsertFeeRequest request)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _feeService.AddFee(request);
            }
            catch (Exception ex)
            {
                baseResponse.ErrorMessage = ex.Message;
                baseResponse.ErrorCode = "003";
                baseResponse.IsSuccess = false;
            }
            return Ok(baseResponse);
        }

        [HttpPost]
        [Route("UpdateFee")]
        public IActionResult AddParking([FromBody] InsertFeeRequest request)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _feeService.UpdateFee(request);
            }
            catch (Exception ex)
            {
                baseResponse.ErrorMessage = ex.Message;
                baseResponse.ErrorCode = "003";
                baseResponse.IsSuccess = false;
            }
            return Ok(baseResponse);
        }

        [HttpPost]
        [Route("insertTransaction")]
        public IActionResult InsertTransaction([FromBody] InsertTransactionRequest request)
        {

            InsertTransactionResponse baseResponse = new InsertTransactionResponse();
            try
            {
                baseResponse = _feeService.InsertTransaction(request);
            }
            catch (Exception ex)
            {
                baseResponse.ErrorMessage = ex.Message;
                baseResponse.ErrorCode = "003";
                baseResponse.IsSuccess = false;
            }
            return Ok(baseResponse);
        }

        [HttpPost]
        [Route("UpdateTransaction")]
        public IActionResult UpdateTransaction([FromBody] UpdateTransactionRequest request)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _feeService.UpdateTransaction(request);
            }
            catch (Exception ex)
            {
                baseResponse.ErrorMessage = ex.Message;
                baseResponse.ErrorCode = "003";
                baseResponse.IsSuccess = false;
            }
            return Ok(baseResponse);
        }
    }
}
