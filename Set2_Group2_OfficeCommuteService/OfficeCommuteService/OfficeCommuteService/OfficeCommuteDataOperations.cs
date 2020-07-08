using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace OfficeCommuteService
{
    public class OfficeCommuteDataOperations
    {
        public static readonly string insertRecords =
            "INSERT INTO tblOfficeCommuteApp(Employee_Id,Employee_Name,Employee_Type,Travel_Distance,Commute_Charge) " +
            "VALUES(@Employee_Id,@Employee_Name,@Employee_Type,@Travel_Distance,@Commute_Charge)";

        public static readonly string displayAllRecords = "SELECT * FROM tblOfficeCommuteApp";

        public static readonly string displayEmployeesWithMaxCommuteFare =
            "SELECT * FROM tblOfficeCommuteApp WHERE Commute_Charge=(SELECT MAX(Commute_Charge) FROM tblOfficeCommuteApp)";

        public SqlConnection _sqlConn = null;
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            }
        }

        public OfficeCommuteDataOperations()
        {

        }       

        

        public bool AddRecords(OfficeCommuteDataInfo commuteData)
        {
            try
            {
                using (_sqlConn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmdAddCommuteRecord = new SqlCommand(insertRecords, _sqlConn))
                    {
                        cmdAddCommuteRecord.Parameters.AddWithValue("@Employee_Id", commuteData.EmployeeId);
                        cmdAddCommuteRecord.Parameters.AddWithValue("@Employee_Name", commuteData.EmployeeName);
                        cmdAddCommuteRecord.Parameters.AddWithValue("@Employee_Type", commuteData.EmployeeType);
                        cmdAddCommuteRecord.Parameters.AddWithValue("@Travel_Distance", commuteData.TravelDistance);
                        cmdAddCommuteRecord.Parameters.AddWithValue("@Commute_Charge", commuteData.CommuteCharge);

                        _sqlConn.Open();

                        int rowsAdded = cmdAddCommuteRecord.ExecuteNonQuery();

                        if (rowsAdded > 0)
                            return true;
                        else
                            return false;
                    }
                }

            }
            catch (SqlException)
            {
                return false;
            }
        }

        public IList<OfficeCommuteDataInfo> DisplayRecords()
        {
            List<OfficeCommuteDataInfo> commuteDataList = new List<OfficeCommuteDataInfo>();

            try
            {
                using (_sqlConn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmdDisplayAllCommuteRecords = new SqlCommand(displayAllRecords, _sqlConn))
                    {

                        _sqlConn.Open();

                        using (SqlDataReader sqlData = cmdDisplayAllCommuteRecords.ExecuteReader())
                        {
                            if (sqlData.HasRows)
                            {
                                while (sqlData.Read())
                                {
                                    OfficeCommuteDataInfo commuteData = new OfficeCommuteDataInfo 
                                    {
                                        EmployeeId = sqlData["Employee_Id"].ToString(),
                                        EmployeeName = sqlData["Employee_Name"].ToString(),
                                        EmployeeType = sqlData["Employee_Type"].ToString(),
                                        TravelDistance = float.Parse(sqlData["Travel_Distance"].ToString()),
                                        CommuteCharge = double.Parse(sqlData["Commute_Charge"].ToString())
                                    };

                                    commuteDataList.Add(commuteData);
                                }                                
                            }
                        }
                    }
                }

            }
            catch (SqlException)
            {
                return null;
            }

            return commuteDataList; ;
        }

        public IList<OfficeCommuteDataInfo> DisplayRecordsByHigestCommuteFare()
        {
            List<OfficeCommuteDataInfo> commuteDataList = new List<OfficeCommuteDataInfo>();

            try
            {
                using (_sqlConn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmdDisplayCommuteRecordsByHigestFare = 
                        new SqlCommand(displayEmployeesWithMaxCommuteFare, _sqlConn))
                    {

                        _sqlConn.Open();

                        using (SqlDataReader sqlData = cmdDisplayCommuteRecordsByHigestFare.ExecuteReader())
                        {
                            if (sqlData.HasRows)
                            {
                                while (sqlData.Read())
                                {
                                    OfficeCommuteDataInfo commuteData = new OfficeCommuteDataInfo
                                    {
                                        EmployeeId = sqlData["Employee_Id"].ToString(),
                                        EmployeeName = sqlData["Employee_Name"].ToString(),
                                        EmployeeType = sqlData["Employee_Type"].ToString(),
                                        TravelDistance = float.Parse(sqlData["Travel_Distance"].ToString()),
                                        CommuteCharge = double.Parse(sqlData["Commute_Charge"].ToString())
                                    };

                                    commuteDataList.Add(commuteData);
                                }
                            }
                        }
                    }
                }

            }
            catch (SqlException)
            {
                return null;
            }

            return commuteDataList; ;
        }
    }
}
