using Inventory.DataAccess.DTO;
using Inventory.DataAccess.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Inventory.DataAccess.Test
{
    [TestClass]
    public class ItemDTOTest
    {
        [TestMethod]
        public void ItemDTO_Validate_Success()
        {
            //Arrange
            var name = new string('i', 45);
            var game = new string('g', 45);
            var itemDTO = new ItemDTO(1, 1, name, game, DateTime.UtcNow.Date, 1);

            //Act
            var validationResult = itemDTO.Validate();

            //Assert
            Assert.IsTrue(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 0);
        }

        [TestMethod]
        public void ItemDTO_Validate_Error_InvalidName()
        {
            //Arrange
            var name = new string('i', 46);
            var game = new string('g', 45);
            var itemDTO = new ItemDTO(1, 1, name, game, DateTime.UtcNow.Date, 1);

            //Act
            var validationResult = itemDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
        }

        [TestMethod]
        public void ItemDTO_Validate_Error_InvalidGame()
        {
            //Arrange
            var name = new string('i', 45);
            var game = new string('g', 46);
            var itemDTO = new ItemDTO(1, 1, name, game, DateTime.UtcNow.Date, 1);

            //Act
            var validationResult = itemDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 1);
        }

        [TestMethod]
        public void ItemDTO_Validate_Error_InvalidNameAndGame()
        {
            //Arrange
            var name = new string('i', 46);
            var game = new string('g', 46);
            var itemDTO = new ItemDTO(1, 1, name, game, DateTime.UtcNow.Date, 1);

            //Act
            var validationResult = itemDTO.Validate();

            //Assert
            Assert.IsFalse(validationResult.IsValid);
            Assert.AreEqual(validationResult.Errors.Count, 2);
        }
    }
}
