using CarParking.Data.IData;
using CarParking.Dto;
using CarParking.Dto.Model.Request;
using CarParking.Dto.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CarParking.Data.Data
{
    public class ParkingData : BaseData, IParkingData
    {
        private const string SP_ADD_PARKING = "[SP_ADD_PARKING]";
        private const string SP_GET_PARKING_BY_EMAIL = "[SP_GET_PARKING_BY_EMAIL]";
        #region Constructor
        public ParkingData(string connetionString) : base(connetionString)
        {
        }
        #endregion
        public bool AddParking(InsertParkingRequest request)
        {
            try
            {
                ExecuteNonQuery(SP_ADD_PARKING,
                ("ParkingName", request.ParkingName),
                ("ParkingAddress", request.ParkingAddress),
                ("Email", request.Email),
                ("Password", request.Password));

                return true;
            }
            catch (SqlException)
            {
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Parking GetParkingByEmail(string email)
        {

            Parking parking = new Parking();
            DataNamesMapper<Parking> mapper = new DataNamesMapper<Parking>();
            try
            {
                DataSet dsResults = ExecuteDataSet(SP_GET_PARKING_BY_EMAIL,
                    ("Email", ConvertDTA(email)));
                if ((dsResults != null) && (dsResults.Tables[0].Rows.Count > 0))
                {
                    if (dsResults.Tables[0].Rows[0][0] != System.DBNull.Value)
                    {
                        parking = mapper.Map(dsResults.Tables[0].Rows[0]);
                    }
                }
                return parking;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
