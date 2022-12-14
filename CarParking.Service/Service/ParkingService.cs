using CarParking.Data.IData;
using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using CarParking.Service.Helper;
using CarParking.Service.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Service.Service
{
    public class ParkingService : CommonValidation, IParkingService
    {
        private readonly IParkingData _parkingService;
        public ParkingService(IParkingData parkingService)
        {
            _parkingService = parkingService;
        }

        public BaseResponse AddParking(InsertParkingRequest request)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.ParkingName) || string.IsNullOrEmpty(request.ParkingAddress))
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }
                if (CheckEmail(request.Email))
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }
                Parking parking = _parkingService.GetParkingByEmail(request.Email);
                if (parking != null && parking.ParkingId != 0)
                {
                    response.ErrorCode = "004";
                    response.ErrorMessage = "citizen id or email is duplicate";
                    return response;
                }
                request.Password = ComputeSha256Hash(request.Password);
                bool isAdd = _parkingService.AddParking(request);
                if (!isAdd)
                {
                    response.ErrorCode = "005";
                    response.ErrorMessage = "Failed to add or update parking";
                    return response;
                }
                response.IsSuccess = true;
                response.ErrorCode = "000";
                response.ErrorMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Parking ParkingLogIn(GetParkingRequest request)
        {
            Parking response = new Parking();
            try
            {
                if (request == null || string.IsNullOrEmpty(request.Email))
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }
                if (CheckEmail(request.Email))
                {
                    response.ErrorCode = "006";
                    response.ErrorMessage = "request is invalid";
                    return response;
                }
                Parking parking = _parkingService.GetParkingByEmail(request.Email);
                if (parking == null || parking.ParkingId == 0)
                {
                    response.ErrorCode = "002";
                    response.ErrorMessage = "email or password is incorrect";
                    return response;
                }
                var hashPassword = ComputeSha256Hash(request.Password);
                if(hashPassword != parking.Password)
                {
                    response.ErrorCode = "002";
                    response.ErrorMessage = "email or password is incorrect";
                    return response;
                }
               
                response.IsSuccess = true;
                response.ErrorCode = "000";
                response.ErrorMessage = "Success";
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
