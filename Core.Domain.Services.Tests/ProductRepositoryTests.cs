using System.Diagnostics;
using Domain;
using Domain.Services;
using Moq;

namespace Core.Domain.Services.Tests
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void GetProducts_Should_ReturnAllProducts()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            List<Product?> products = new List<Product?>
            {
                new Product { Id = 1, Name = "Apple", ContainsAlcohol = false, ImageLink = "apple.jpg" },
                new Product { Id = 2, Name = "Beer", ContainsAlcohol = true, ImageLink = "beer.jpg" }
            };

            productRepositoryMock.Setup(repo => repo.GetProducts()).Returns(products);

            // Act
            var result = productRepositoryMock.Object.GetProducts();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p != null && p.Name == "Apple" && !p.ContainsAlcohol);
            Assert.Contains(result, p => p is { Name: "Beer" } && p.ContainsAlcohol);
        }

        [Fact]
        public void GetProductById_Should_ReturnProductById()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var product = new Product { Id = 1, Name = "Apple", ContainsAlcohol = false, ImageLink = "apple.jpg" };

            productRepositoryMock.Setup(repo => repo.GetProduct(1)).Returns(product);

            // Act
            var result = productRepositoryMock.Object.GetProduct(1);

            // Assert
            Assert.NotNull(result);
            Debug.Assert(result != null, nameof(result) + " != null");
            Assert.Equal("Apple", result.Name);
            Assert.False(result.ContainsAlcohol);
            Assert.Equal("apple.jpg", result.ImageLink);
        }

        [Fact]
        public void CreateProduct_Should_AddNewProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var newProduct = new Product { Id = 3, Name = "Orange", ContainsAlcohol = false, ImageLink = "orange.jpg" };

            productRepositoryMock.Setup(repo => repo.CreateProduct(It.IsAny<Product>()));

            // Act
            productRepositoryMock.Object.CreateProduct(newProduct);

            // Assert
            productRepositoryMock.Verify(repo => repo.CreateProduct(newProduct), Times.Once);
        }

        [Fact]
        public void UpdateProduct_Should_UpdateExistingProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var product = new Product { Id = 2, Name = "Beer", ContainsAlcohol = true, ImageLink = "beer.jpg" };

            productRepositoryMock.Setup(repo => repo.UpdateProduct(product));

            // Act
            productRepositoryMock.Object.UpdateProduct(product);

            // Assert
            productRepositoryMock.Verify(repo => repo.UpdateProduct(product), Times.Once);
        }

        [Fact]
        public void DeleteProduct_Should_RemoveProduct()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var product = new Product { Id = 2, Name = "Beer", ContainsAlcohol = true, ImageLink = "beer.jpg" };

            productRepositoryMock.Setup(repo => repo.DeleteProduct(product));

            // Act
            productRepositoryMock.Object.DeleteProduct(product);

            // Assert
            productRepositoryMock.Verify(repo => repo.DeleteProduct(product), Times.Once);
        }

        [Fact]
        public void Product_ContainsAlcohol_ShouldBeRequired()
        {
            // Arrange
            var productWithAlcohol = new Product { Id = 4, Name = "Wine", ContainsAlcohol = true, ImageLink = "wine.jpg" };
            var productWithoutAlcohol = new Product { Id = 5, Name = "Juice", ContainsAlcohol = false, ImageLink = "juice.jpg" };

            // Act & Assert
            Assert.True(productWithAlcohol.ContainsAlcohol);
            Assert.False(productWithoutAlcohol.ContainsAlcohol);
        }
    }
}
