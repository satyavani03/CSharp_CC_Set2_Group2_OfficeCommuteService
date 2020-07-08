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

       	//Fill your code to implement AddRecords method here
        
	
	
	//Fill your code to implement DisplayRecords method here
        

	
	//Fill your code to implement DisplayRecordsByHigestCommuteFare method here
        
    }
}
