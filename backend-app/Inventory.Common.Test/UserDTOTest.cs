using Inventory.DataAccess.DTO;
using Inventory.DataAccess.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inventory.DataAccess.Test
{
    [TestClass]
    public class UserDTOTest
    {
        public void UserDTO_Validate_Success()
        {
            //Arrange
            var username = new string('i', 45);
            var userDTO = new UserDTO(1, username);

            //Act
            var validationResult = userDTO.Validate();

            //Assert
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 0);
        }

        [TestMethod]
        public void UserDTO_Validate_Error_InvalidUsername()
        {
            //Arrange
            var username = new string('i', 46);
            var userDTO = new UserDTO(1, username);

            //Act
            var validationResult = userDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
        }
    }
}
