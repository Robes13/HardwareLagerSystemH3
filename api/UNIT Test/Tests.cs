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

        [TestMethod]
        public void CreateNotification() // Create Notification
        {
            int userHardwareId = 1;
            string message = "Hej med dig";

            return NotificationRepository.CreateNotification(userHardwareId, message);
        }
        [TestMethod]
        public void GetNotificationById() // Get Notification By Id
        {
            int notificationId = 1;

            return NotificationRepository.GetNotificationById(notificationId);
        }
        [TestMethod]
        public void DeleteNotification() // Delete Notification
        {
            int notificationId = 1;

            return NotificationRepository.DeleteNotification(notificationId);
        }
        [TestMethod]
        public void UpdateNotification() // Update Notification
        {
            int notificationId = 1;
            int userHardwareId = 0;
            string message = "Test opdatering #1";

            return NotificationRepository.UpdateNotification(notificationId, userHardwareId, message);
        }
        [TestMethod]
        public void GetNotificationByUserId() // Get Notification By User Id
        {
            int userId = 0;
            List<int> ids = new List<int>();

            ids = NotificationRepository.GetAllUserHardwareIdsByUserId(userId);

            return NotificationRepository.GetAllNotificationsByUserHardwareId(ids);
        }
        [TestMethod]
        public void GetAllTypes() // Get All Types
        {
            return TypeRepository.GetAllTypes();
        }
        [TestMethod]
        public void GetTypeById() // Get Type By Id
        {
            int typeId = 0;

            return TypeRepository.GetTypeById(typeId);
        }
        [TestMethod]
        public void CreateType() // Create Type
        {
            string typeName = "Oprettet type";

            return TypeRepository.CreateType(typeName);
        }
        [TestMethod]
        public void UpdateType() // Update Type
        {
            int typeId = 0;
            string typeName = "Opdateret type";

            return TypeRepository.UpdateType(typeId, typeName);
        }
        [TestMethod]
        public void DeleteType() // Delete Type
        {
            int typeId = 0;

            return TypeRepository.UpdateType(typeId);
        }
        [TestMethod]
        public void GetAllCategories() // Get All Categories
        {
            return CategoryRepository.GetAllCategories();
        }
        [TestMethod]
        public void GetCategoryById() // Get Category By Id
        {
            int categoryId = 0;

            return CategoryRepository.GetCategoryById(categoryId);
        }
        [TestMethod]
        public void CreateCategory() // Create Category
        {
            string categoryName = "Oprettet Kategori";

            return CategoryRepository.CreateCategory(categoryName);
        }
        [TestMethod]
        public void UpdateCategory() // Update Category
        {
            int categoryId = 0;
            string categoryName = "Opdateret Kategori";

            return CategoryRepository.UpdateCategory(categoryId, categoryName);
        }
        [TestMethod]
        public void DeleteCategory() // Delete Category
        {
            int categoryId = 0;

            return CategoryRepository.UpdateCategory(categoryId);
        }
    }
}