using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeCommuteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Reflection;
using System.Configuration;
using System.Transactions;
using NUnit.Framework.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace OfficeCommuteService.Tests
{
    public class RollbackAttribute : Attribute, ITestAction
    {
        private TransactionScope transaction;

        public void BeforeTest(ITest test)
        {
            transaction = new TransactionScope();
        }
        public void AfterTest(ITest test)
        {
            transaction.Dispose();
        }
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
    [TestFixture]
    public class OfficeCommuteTests
    {
        Assembly assembly;
        Type clsOfficeCommuteDataOperations;
        Type clsOfficeCommuteDataInfo;
        Type clsProgram;

        OfficeCommuteDataOperations OfficeCommuteDataOperations;
        Program Program;

        [SetUp]
        public void SetUp()
        {
            OfficeCommuteDataOperations = new OfficeCommuteDataOperations();
            Program = new Program();
            assembly = Assembly.Load("OfficeCommuteService");
            clsOfficeCommuteDataOperations = assembly.GetType("OfficeCommuteService.OfficeCommuteDataOperations");
            clsOfficeCommuteDataInfo = assembly.GetType("OfficeCommuteService.OfficeCommuteDataInfo");
            clsProgram= assembly.GetType("OfficeCommuteService.Program");
        }

        [TestCase]
        public void TestBasicChecks()
        {
            NUnit.Framework.Assert.IsNotNull(clsOfficeCommuteDataInfo, "Class OfficeCommuteDataInfo NOT implemented OR check spelling");
            NUnit.Framework.Assert.IsNotNull(clsOfficeCommuteDataOperations, "Class OfficeCommuteDataOperations NOT implemented OR check spelling");
            NUnit.Framework.Assert.IsNotNull(clsProgram, "Class Program NOT implemented OR check spelling");
        }

        [TestCase]
        public void AddRecords_MethodExistence_Test()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            if (clsOfficeCommuteDataOperations != null)
            {
                MethodInfo addRecordsMethod = clsOfficeCommuteDataOperations.
                    GetMethod("AddRecords", allBindings);

                NUnit.Framework.Assert.That(addRecordsMethod != null, "Method AddRecords NOT implemented OR check spelling");
            }
            else
            {
                NUnit.Framework.Assert.Fail("No class with the name 'OfficeCommuteDataOperations' is implemented OR Did you change the class name");
            }
        }

        [TestCase]
        public void DisplayRecords_MethodExistence_Test()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            if (clsOfficeCommuteDataOperations != null)
            {
                MethodInfo displayRecordsMethod = clsOfficeCommuteDataOperations.
                    GetMethod("DisplayRecords", allBindings);

                NUnit.Framework.Assert.That(displayRecordsMethod != null, "Method DisplayRecords NOT implemented OR check spelling");
            }
            else
            {
                NUnit.Framework.Assert.Fail("No class with the name 'OfficeCommuteDataOperations' is implemented OR Did you change the class name");
            }
        }

        [TestCase]
        public void DisplayRecordsByHigestCommuteFare_MethodExistence_Test()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            if (clsOfficeCommuteDataOperations != null)
            {
                MethodInfo displayRecordsByHigestCommuteFareMethod = clsOfficeCommuteDataOperations.
                    GetMethod("DisplayRecordsByHigestCommuteFare", allBindings);

                NUnit.Framework.Assert.That(displayRecordsByHigestCommuteFareMethod != null, "Method DisplayRecordsByHigestCommuteFare NOT implemented OR check spelling");
            }
            else
            {
                NUnit.Framework.Assert.Fail("No class with the name 'OfficeCommuteDataOperations' is implemented OR Did you change the class name");
            }
        }
        [TestCase]
        public void CheckEmployeeId_WhenEmployeeIdFormatCorrect_ReturnsTrue()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeId = "EXT5001";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckEmployeeIdFormat", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeId=(bool)methodInfo.Invoke(classObject, new object[] { employeeId });
            NUnit.Framework.Assert.IsTrue(checkEmployeeId);
        }

        [TestCase]
        public void CheckEmployeeId_WhenEmployeeIdFormatInCorrect_ReturnsFalse()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeId = "ABCD1234";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckEmployeeIdFormat", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeId = (bool)methodInfo.Invoke(classObject, new object[] { employeeId });
            NUnit.Framework.Assert.IsFalse(checkEmployeeId);
        }

        [TestCase]
        public void CheckEmployeeId_WhenNoDuplicateIdPresentInDatabase_ReturnsTrue()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeId = "EXT5001";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckForDuplicateEmployeeId", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeId = (bool)methodInfo.Invoke(classObject, new object[] { employeeId });
            NUnit.Framework.Assert.IsTrue(checkEmployeeId);
        }

        [TestCase]
        public void CheckEmployeeId_WhenDuplicateIdIsPresentInDatabase_ReturnsFalse()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeId = "EXT1001";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckForDuplicateEmployeeId", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeId = (bool)methodInfo.Invoke(classObject, new object[] { employeeId });
            NUnit.Framework.Assert.IsFalse(checkEmployeeId);
        }

        [TestCase]
        public void CheckEmployeeType_WhenTypeEnteredIsExternalOrInternal_ReturnsTrue()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeType = "EXTERNAL";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckEmployeeTypeInput", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeType = (bool)methodInfo.Invoke(classObject, new object[] { employeeType });
            NUnit.Framework.Assert.IsTrue(checkEmployeeType);
        }

        [TestCase]
        public void CheckEmployeeType_WhenTypeEnteredIsNotExternalOrInternal_ReturnsFalse()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeType = "Vendor";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckEmployeeTypeInput", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeType = (bool)methodInfo.Invoke(classObject, new object[] { employeeType });
            NUnit.Framework.Assert.IsFalse(checkEmployeeType);
        }

        [TestCase]
        public void CheckEmployeeType_WhenTypeEnteredMatchesWithEmployeeIdType_ReturnsTrue()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeId = "EXT5001";
            string employeeType = "EXTERNAL";            
            MethodInfo methodInfo = clsProgram.GetMethod("CheckIfEmployeeTypeMatchesWithEmployeeIdType", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeType = (bool)methodInfo.Invoke
                (classObject, new object[] { employeeId.Substring(0,3),employeeType.Substring(0,3) });
            NUnit.Framework.Assert.IsTrue(checkEmployeeType);
        }

        [TestCase]
        public void CheckEmployeeType_WhenTypeEnteredDoesNotMatcheWithEmployeeIdType_ReturnsFalse()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeId = "EXT5001";
            string employeeType = "INTERNAL";
            MethodInfo methodInfo = clsProgram.GetMethod("CheckIfEmployeeTypeMatchesWithEmployeeIdType", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkEmployeeType = (bool)methodInfo.Invoke
                (classObject, new object[] { employeeId.Substring(0, 3), employeeType.Substring(0, 3) });
            NUnit.Framework.Assert.IsFalse(checkEmployeeType);
        }

        [TestCase]
        public void CheckTravelDistance_WhenTraveDistanceIsWithinLowerLimitValue_ReturnsTrue()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            float travelDistance = 2.8F;
            MethodInfo methodInfo = clsProgram.GetMethod("CheckTravelDistance", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkTravelDistance = (bool)methodInfo.Invoke
                (classObject, new object[] { travelDistance });
            NUnit.Framework.Assert.IsTrue(checkTravelDistance);
        }

        [TestCase]
        public void CheckTravelDistance_WhenTraveDistanceIsWithinUpperLimitValue_ReturnsTrue()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            float travelDistance = 30.0F;
            MethodInfo methodInfo = clsProgram.GetMethod("CheckTravelDistance", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkTravelDistance = (bool)methodInfo.Invoke
                (classObject, new object[] { travelDistance });
            NUnit.Framework.Assert.IsTrue(checkTravelDistance);
        }
        [TestCase]
        public void CheckTravelDistance_WhenTravelDistanceValueIsLessThanLowerLimitValue_ReturnsFalse()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            float travelDistance = 1.99F;
            MethodInfo methodInfo = clsProgram.GetMethod("CheckTravelDistance", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkTravelDistance = (bool)methodInfo.Invoke
                (classObject, new object[] { travelDistance });
            NUnit.Framework.Assert.IsFalse(checkTravelDistance);
        }

        [TestCase]
        public void CheckTravelDistance_WhenTravelDistanceValueIsMoreThanUpperLimitValue_ReturnsFalse()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            float travelDistance = 30.5F;
            MethodInfo methodInfo = clsProgram.GetMethod("CheckTravelDistance", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            bool checkTravelDistance = (bool)methodInfo.Invoke
                (classObject, new object[] { travelDistance });
            NUnit.Framework.Assert.IsFalse(checkTravelDistance);
        }

        [TestCase]
        public void FareCalculation_OnValidInputs_ReturnsExpectedFare()
        {
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            string employeeType = "EXTERNAL";
            float travelDistance = 5;
            MethodInfo methodInfo = clsProgram.GetMethod("FareCalculation", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            double Fare = (double)methodInfo.Invoke
                (classObject, new object[] { employeeType,travelDistance });
            NUnit.Framework.Assert.That(350,Is.EqualTo(Fare));
        }

        [TestCase]
        [Category("Database")]
        public void GetConnectionStringFromAppConfig()
        {
            string actualString = OfficeCommuteDataOperations.ConnectionString;
            string expectedString = ConfigurationManager.ConnectionStrings["SqlCon"].ConnectionString;
            NUnit.Framework.Assert.AreEqual(expectedString, actualString);
        }

        [TearDown]
        public void TearDown()
        {
            OfficeCommuteDataOperations = null;
        }

        [TestCase]
        [Category("Database")]
        public void ConnectAndDisconnectFromDatabase()
        {
            SqlConnection conn = new SqlConnection(OfficeCommuteDataOperations.ConnectionString);
            conn.Open();
            bool connected = conn.State == ConnectionState.Open;
            conn.Close();
            bool disconnected = conn.State == ConnectionState.Closed;
            NUnit.Framework.Assert.IsTrue(connected);
            NUnit.Framework.Assert.IsTrue(disconnected);
        }

        [TestCase]
        [Category("Database")]
        [Rollback]
        public void AddRecords_WhenValidInputs_ReturnsTrue()
        {
            string employeeId = "EXT5001";
            string employeeName = "Hammis";
            string employeeType = "EXTERNAL";
            float travelDistance = 8.3F;
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            MethodInfo methodInfo = clsProgram.GetMethod("FareCalculation", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            double Fare = (double)methodInfo.Invoke
                (classObject, new object[] { employeeType, travelDistance });

            MethodInfo insertMethodInfo = clsOfficeCommuteDataOperations.GetMethod("AddRecords", allBindings);
            ConstructorInfo commuteDataOperationsClassConstructor = clsOfficeCommuteDataOperations.GetConstructor(Type.EmptyTypes);
            object commuteDataOperationsClassObject = commuteDataOperationsClassConstructor.Invoke(new object[] { });
            bool inserted = (bool)insertMethodInfo.Invoke
                (commuteDataOperationsClassObject, new object[]
                {new OfficeCommuteDataInfo(){EmployeeId=employeeId,EmployeeName=employeeName,
                EmployeeType=employeeType,TravelDistance=travelDistance,CommuteCharge=Fare} });

            NUnit.Framework.Assert.IsTrue(inserted);
        }

        [TestCase]
        [Category("Database")]
        [Rollback]
        public void AddRecords_WhenInValidInputs_ReturnsFalse()
        {
            string employeeId = "EXT5001";
            string empoyeeName = null;
            string employeeType = "EXTERNAL";
            float travelDistance = 8.3F;            
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            MethodInfo methodInfo = clsProgram.GetMethod("FareCalculation", allBindings);
            ConstructorInfo classConstructor = clsProgram.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            double Fare = (double)methodInfo.Invoke
                (classObject, new object[] { employeeType, travelDistance });

            MethodInfo insertMethodInfo = clsOfficeCommuteDataOperations.GetMethod("AddRecords", allBindings);
            ConstructorInfo commuteDataOperationsClassConstructor = clsOfficeCommuteDataOperations.GetConstructor(Type.EmptyTypes);
            object commuteDataOperationsClassObject = commuteDataOperationsClassConstructor.Invoke(new object[] { });
            bool inserted = (bool)insertMethodInfo.Invoke
                (commuteDataOperationsClassObject, new object[]
                {new OfficeCommuteDataInfo(){EmployeeId=employeeId,EmployeeName=empoyeeName,
                EmployeeType=employeeType,TravelDistance=travelDistance,CommuteCharge=Fare} });
            NUnit.Framework.Assert.IsFalse(inserted);
        }

        [Test]
        [Category("Database")]
        [Rollback]
        public void DisplayRecords_WhenCalled_ReturnAllEmployeesCommuteRecords()
        {

            List<OfficeCommuteDataInfo> testOfficeCommuteRecords = new List<OfficeCommuteDataInfo>
            {
                new OfficeCommuteDataInfo{ EmployeeId="EXT6001",EmployeeName="Venkat",EmployeeType="External",TravelDistance=15.7F,CommuteCharge=1256},
                new OfficeCommuteDataInfo{ EmployeeId="INT6001",EmployeeName="Prakash",EmployeeType="Internal",TravelDistance=10.0F,CommuteCharge=500},
                new OfficeCommuteDataInfo{ EmployeeId="EXT6002",EmployeeName="Nivetha",EmployeeType="External",TravelDistance=15.7F,CommuteCharge=1256},
                new OfficeCommuteDataInfo{ EmployeeId="INT6001",EmployeeName="Gokul",EmployeeType="Internal",TravelDistance=8.0F,CommuteCharge=800}
            };
            for (int i = 0; i < testOfficeCommuteRecords.Count; i++)
            {
                OfficeCommuteDataOperations.AddRecords(testOfficeCommuteRecords[i]);
            }

            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            MethodInfo methodInfo = clsOfficeCommuteDataOperations.GetMethod("DisplayRecords", allBindings);
            ConstructorInfo classConstructor = clsOfficeCommuteDataOperations.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });
            
            IList<OfficeCommuteDataInfo> officeCommuteTableRecords =
               methodInfo.Invoke
                (classObject, new object[] { }) as IList<OfficeCommuteDataInfo>;

            NUnit.Framework.Assert.IsNotNull(officeCommuteTableRecords);
            NUnit.Framework.Assert.IsTrue(officeCommuteTableRecords.Count >= testOfficeCommuteRecords.Count);
        }

        [Test]
        [Rollback]
        [Category("Database")]
        public void DisplayRecords_WhenNoRecordsFoundInDB_ReturnAnEmptyList()
        {
            using (OfficeCommuteDataOperations._sqlConn = new SqlConnection(OfficeCommuteDataOperations.ConnectionString))
            {
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM tblOfficeCommuteApp", OfficeCommuteDataOperations._sqlConn);
                OfficeCommuteDataOperations._sqlConn.Open();
                deleteCmd.ExecuteNonQuery();
            }
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            MethodInfo methodInfo = clsOfficeCommuteDataOperations.GetMethod("DisplayRecords", allBindings);
            ConstructorInfo classConstructor = clsOfficeCommuteDataOperations.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });

            IList<OfficeCommuteDataInfo> officeCommuteTableRecords =
               methodInfo.Invoke
                (classObject, new object[] { }) as IList<OfficeCommuteDataInfo>;

            NUnit.Framework.Assert.That(officeCommuteTableRecords, Is.Empty);
        }

        [Test]
        [Category("Database")]
        [Rollback]
        public void DisplayRecordsByHigestCommuteFare_WhenCalled_ReturnsAllRecordsWithHigestCommuteFare()
        {
            List<OfficeCommuteDataInfo> testOfficeCommuteRecords = new List<OfficeCommuteDataInfo>
            {
                new OfficeCommuteDataInfo{ EmployeeId="EXT6001",EmployeeName="Venkat",EmployeeType="External",TravelDistance=15.7F,CommuteCharge=1256},
                new OfficeCommuteDataInfo{ EmployeeId="INT6001",EmployeeName="Prakash",EmployeeType="Internal",TravelDistance=10.0F,CommuteCharge=500},
                new OfficeCommuteDataInfo{ EmployeeId="EXT6002",EmployeeName="Nivetha",EmployeeType="External",TravelDistance=15.7F,CommuteCharge=1256},
                new OfficeCommuteDataInfo{ EmployeeId="INT6001",EmployeeName="Gokul",EmployeeType="Internal",TravelDistance=8.0F,CommuteCharge=800}
            };
            double maxFare = testOfficeCommuteRecords.Max(m => m.CommuteCharge);

            for (int i = 0; i < testOfficeCommuteRecords.Count; i++)
            {
                OfficeCommuteDataOperations.AddRecords(testOfficeCommuteRecords[i]);
            }

            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            MethodInfo methodInfo = clsOfficeCommuteDataOperations.GetMethod("DisplayRecordsByHigestCommuteFare", allBindings);
            ConstructorInfo classConstructor = clsOfficeCommuteDataOperations.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });

            IList<OfficeCommuteDataInfo> officeCommuteRecordsWithHigestFare =
               methodInfo.Invoke
                (classObject, new object[] { }) as IList<OfficeCommuteDataInfo>;

            NUnit.Framework.Assert.IsNotNull(officeCommuteRecordsWithHigestFare);
            NUnit.Framework.Assert.IsTrue(officeCommuteRecordsWithHigestFare.Count >= testOfficeCommuteRecords.FindAll(m => m.CommuteCharge == maxFare).Count);
        }

        [Test]
        [Rollback]
        [Category("Database")]
        public void DisplayRecordsByHigestCommuteFare_WhenNoRecordsFoundInDB_ReturnAnEmptyList()
        {
            using (OfficeCommuteDataOperations._sqlConn = new SqlConnection(OfficeCommuteDataOperations.ConnectionString))
            {
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM tblOfficeCommuteApp", OfficeCommuteDataOperations._sqlConn);
                OfficeCommuteDataOperations._sqlConn.Open();
                deleteCmd.ExecuteNonQuery();
            }
            var allBindings = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            MethodInfo methodInfo = clsOfficeCommuteDataOperations.GetMethod("DisplayRecordsByHigestCommuteFare", allBindings);
            ConstructorInfo classConstructor = clsOfficeCommuteDataOperations.GetConstructor(Type.EmptyTypes);
            object classObject = classConstructor.Invoke(new object[] { });

            IList<OfficeCommuteDataInfo> officeCommuteTableHigestFareRecord =
               methodInfo.Invoke
                (classObject, new object[] { }) as IList<OfficeCommuteDataInfo>;

            NUnit.Framework.Assert.That(officeCommuteTableHigestFareRecord, Is.Empty);
        }

    }
}