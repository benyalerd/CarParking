using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Service.IService
{
    public interface IParkingService
    {
        BaseResponse AddParking(InsertParkingRequest request);
        Parking ParkingLogIn(GetParkingRequest request);
    }
}
