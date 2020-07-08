﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeCommuteService
{
    public class OfficeCommuteDataInfo
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }        
        public string EmployeeType { get; set; }
        public float TravelDistance { get; set; }
        public double CommuteCharge { get; set; }

        public OfficeCommuteDataInfo()
        {

        }
        public OfficeCommuteDataInfo(string employeeId, string employeeName, string employeeType, float travelDistance, float commuteCharge)
        {
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            EmployeeType = employeeType;
            TravelDistance = travelDistance;
            CommuteCharge = commuteCharge;
        }
    }
}
