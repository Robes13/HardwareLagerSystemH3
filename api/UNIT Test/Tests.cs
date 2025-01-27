using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;

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
        public void TestThree() 
        {
            
        }


        [TestMethod]
        public void TestFour()
        {

        }

        [TestMethod]
        public void TestFive() 
        {

        }
    }
}