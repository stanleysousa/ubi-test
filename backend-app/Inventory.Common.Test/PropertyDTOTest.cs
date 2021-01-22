using Inventory.DataAccess.DTO;
using Inventory.DataAccess.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inventory.DataAccess.Test
{
    [TestClass]
    public class PropertyDTOTest
    {
        [TestMethod]
        public void PropertyDTO_Validate_Success()
        {
            //Arrange
            var name = new string('i', 45);
            var value = new string('g', 45);
            var propertyDTO = new PropertyDTO(1, 1, name, value);

            //Act
            var validationResult = propertyDTO.Validate();

            //Assert
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 0);
        }

        [TestMethod]
        public void PropertyDTO_Validate_Error_InvalidName()
        {
            //Arrange
            var name = new string('i', 46);
            var value = new string('g', 45);
            var propertyDTO = new PropertyDTO(1, 1, name, value);

            //Act
            var validationResult = propertyDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
        }

        [TestMethod]
        public void PropertyDTO_Validate_Error_InvalidValue()
        {
            //Arrange
            var name = new string('i', 45);
            var value = new string('g', 46);
            var propertyDTO = new PropertyDTO(1, 1, name, value);

            //Act
            var validationResult = propertyDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
        }

        [TestMethod]
        public void PropertyDTO_Validate_Error_InvalidNameAndValue()
        {
            //Arrange
            var name = new string('i', 46);
            var value = new string('g', 46);
            var propertyDTO = new PropertyDTO(1, 1, name, value);

            //Act
            var validationResult = propertyDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 2);
        }
    }
}
