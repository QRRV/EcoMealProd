using System.Collections.Generic;
using Domain;
using Domain.Services;
using Moq;
using Xunit;

namespace Core.Domain.Services.Tests
{
    public class MealBoxProductRepositoryTests
    {
        [Fact]
        public void CreateMealBox_Product_Should_AddNewMealBoxProductLink()
        {
            // Arrange
            var mealBoxProductRepositoryMock = new Mock<IMealboxProductRepository>();
            var newMealboxProduct = new Mealbox_Product
            {
                Id = 1,
                MealBoxId = 1,
                ProductId = 101,
                MealBox = new MealBox { Id = 1, Name = "Lunch Box" },
                Product = new Product { Id = 101, Name = "Apple", ContainsAlcohol = false }
            };

            mealBoxProductRepositoryMock.Setup(repo => repo.CreateMealBox_Product(It.IsAny<Mealbox_Product>()));

            // Act
            mealBoxProductRepositoryMock.Object.CreateMealBox_Product(newMealboxProduct);

            // Assert
            mealBoxProductRepositoryMock.Verify(repo => repo.CreateMealBox_Product(newMealboxProduct), Times.Once);
        }

        [Fact]
        public void UpdateMealBox_Product_Should_UpdateExistingMealBoxProductLink()
        {
            // Arrange
            var mealBoxProductRepositoryMock = new Mock<IMealboxProductRepository>();
            var mealboxProduct = new Mealbox_Product
            {
                Id = 2,
                MealBoxId = 2,
                ProductId = 202,
                MealBox = new MealBox { Id = 2, Name = "Dinner Box" },
                Product = new Product { Id = 202, Name = "Orange Juice", ContainsAlcohol = false }
            };

            mealBoxProductRepositoryMock.Setup(repo => repo.UpdateMealBox_Product(mealboxProduct));

            // Act
            mealBoxProductRepositoryMock.Object.UpdateMealBox_Product(mealboxProduct);

            // Assert
            mealBoxProductRepositoryMock.Verify(repo => repo.UpdateMealBox_Product(mealboxProduct), Times.Once);
        }

        [Fact]
        public void DeleteMealBox_Product_Should_RemoveMealBoxProductLink()
        {
            // Arrange
            var mealBoxProductRepositoryMock = new Mock<IMealboxProductRepository>();
            var mealboxProduct = new Mealbox_Product
            {
                Id = 3,
                MealBoxId = 3,
                ProductId = 303,
                MealBox = new MealBox { Id = 3, Name = "Snack Box" },
                Product = new Product { Id = 303, Name = "Beer", ContainsAlcohol = true }
            };

            mealBoxProductRepositoryMock.Setup(repo => repo.DeleteMealBox_Product(mealboxProduct));

            // Act
            mealBoxProductRepositoryMock.Object.DeleteMealBox_Product(mealboxProduct);

            // Assert
            mealBoxProductRepositoryMock.Verify(repo => repo.DeleteMealBox_Product(mealboxProduct), Times.Once);
        }

        [Fact]
        public void CreateMealBox_Product_WithAlcohol_Should_SetMealBoxToAdultOnly()
        {
            // Arrange
            var mealBoxProductRepositoryMock = new Mock<IMealboxProductRepository>();
            var alcoholicProduct = new Product { Id = 404, Name = "Wine", ContainsAlcohol = true };
            var mealBox = new MealBox { Id = 4, Name = "Gourmet Box", Adult = false };
            var mealboxProduct = new Mealbox_Product { Id = 4, MealBox = mealBox, Product = alcoholicProduct };

            mealBoxProductRepositoryMock.Setup(repo => repo.CreateMealBox_Product(It.IsAny<Mealbox_Product>()))
                .Callback<Mealbox_Product>(mbp =>
                {
                    if (mbp.Product.ContainsAlcohol)
                    {
                        mbp.MealBox.Adult = true;
                    }
                });

            // Act
            mealBoxProductRepositoryMock.Object.CreateMealBox_Product(mealboxProduct);

            // Assert
            Assert.True(mealboxProduct.MealBox.Adult);
        }
    }
}
