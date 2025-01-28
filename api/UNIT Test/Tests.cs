using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Runtime.InteropServices;

namespace api.UNIT_Test
{

    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ValidateData()
        {
            string username = "hanssuper400";
            string hashedpassword = "password";

            string email = "email@zbc.dk";

            // Future enum
            RoleEnum userRole;

            bool isDeleted = false;

            DateTime? deletionTime = null;
            return UserRepository.ValidateData(username, hashedpassword, email, userRole, isDeleted, deletionTime);
        }
        [TestMethod]
        public void CreateUser()
        {
            string username = "hanssuper400";
            string hashedpassword = "password";
            string email = "email@zbc.dk";

            // Future enum
            RoleEnum userRole;

            bool isDeleted = false;

            DateTime? deletionTime = null;

            return UserRepository.CreateUser(username, hashedpassword, email, userRole, isDeleted, deletionTime);
        }
        [TestMethod]
        public void GetUserById() 
        {
            int userid = 0;

            return UserRepository.GetUserById(userid);
        }


        [TestMethod]
        public void UpdateUser()
        {
            int userid = 0;
            string username = "megamind";
            string hashedpassword = "megamind1234";
            string email = "megamind@zbc.dk";

            RoleEnum userRole;

            bool isDeleted = true;
        }

        [TestMethod]
        public void DeleteUser()
        {
            int userid = 0;
            return UserRepository.DeleteUser(userid);
        }

        [TestMethod]
        public void GetDeletedUserInfo() 
        {
            int userid = 0;
            return UserRepository.GetDeletedUserInfo(userid);
        }

        [TestMethod]
        public void CreateHardware() 
        {
            string hardwareDescription = "Dette er en computer";

            enumstatus status = 1;

            int typeid = 0;

            return HardwareRepository.CreateHardware(hardwareDescription, enumstatus, typeid);
        }

        [TestMethod]
        public void UpdateHardware()
        {
            int hardwareid = 0;
            string hardwareDescription = "Computeren er nu opdateret";
            enumstatus status = 2;

            int typeid = 0;
            return HardwareRepository.UpdateHardware(hardwareid, hardwareDescription, status, typeid);
        }

        [TestMethod]
        public void DeleteHardware()
        {
            int hardwareid = 0;
            return HardwareRepository.DeleteHardware(hardwareid);
        }
        [TestMethod]
        public void GetHardwareById() 
        {
            int hardwareid = 0;
            return HardwareRepository.GetHardwareById(hardwareid);
        }
        [TestMethod]
        public void GetHardwareByTypeAndCategory() 
        {
            int typeid = 0;
            int categoryid = 0;
            return HardwareRepository.GetHardwareByTypeAndCategory(typeid, categoryid);
        }

        
        [TestMethod]
        public void CreateLoan() 
        {
            int HardwareID = 0;
            int UserID = 0; 

            DateTime startDate = DateTime.Now();
            DateTime? endingDate = null;
            DateTime? deliveryDate = null;
            bool isDelivered = false;

            return UserHardwareRepository.CreateLoan(HardwareID, UserID, startDate, endingDate, deliveryDate, isDelivered);
        }

        [TestMethod]
        public void FindLoanByID() 
        {
            int UserID = 0;
            
            return UserHardwareRepository.FindLoanByID(UserID);
        }

        [TestMethod]
        public void FindLoanByHardwareID() 
        {
            int HardwareID = 0;

            return UserHardwareRepository.FindLoanByHardwareID(HardwareID);
        }

        [TestMethod]
        public void FindAllActiveLoans() 
        {
            return UserHardwareRepository.FindAllActiveLoans();
        }

        [TestMethod]
        public void FindAllFinishedLoans() 
        {
            return UserHardwareRepository.FindAllFinishedLoans();
        }

        [TestMethod]
        public void FindAllFutureLoans() 
        {
            return UserHardwareRepository.FindAllFutureLoans();
        }
    }
}