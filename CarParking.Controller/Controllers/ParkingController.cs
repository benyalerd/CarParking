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
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }
     
        [HttpPost]
        [Route("AddParking")]
        public IActionResult AddParking([FromBody] InsertParkingRequest request)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _parkingService.AddParking(request);
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
        [Route("AddParking")]
        public IActionResult ParkingLogIn([FromBody] GetParkingRequest request)
        {

            BaseResponse baseResponse = new BaseResponse();
            try
            {
                baseResponse = _parkingService.ParkingLogIn(request);
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
