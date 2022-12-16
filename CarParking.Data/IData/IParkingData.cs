using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarParking.Data.IData
{
    public interface IParkingData
    {
        void CloseConnection();
        bool AddParking(InsertParkingRequest request);
        Parking GetParkingByEmail(string email);
    }
}
