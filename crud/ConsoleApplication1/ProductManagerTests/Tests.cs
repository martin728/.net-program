using ConsoleApplication1.Classes;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CreateProduct_ShouldAddProductToList()
        {
            // Arrange
            var orderManager = new OrderManager();
            var product = new Product { Id = 1, Name = "Product A" };

            // Act
            orderManager.CreateProduct(product);

            // Assert
            var fetchedProduct = orderManager.GetProductById(1);
            Assert.NotNull(fetchedProduct);
            Assert.AreEqual("Product A", fetchedProduct.Name);
        }

        [Test]
        public void UpdateProduct_ShouldUpdateProductDetails()
        {
            // Arrange
            var orderManager = new OrderManager();
            var initialProduct = new Product { Id = 1, Name = "Initial Product" };
            orderManager.CreateProduct(initialProduct);

            var updatedProduct = new Product { Id = 1, Name = "Updated Product" };

            // Act
            orderManager.UpdateProduct(updatedProduct);

            // Assert
            var fetchedProduct = orderManager.GetProductById(1);
            Assert.NotNull(fetchedProduct);
            Assert.AreEqual("Updated Product", fetchedProduct.Name);
        }

        [Test]
        public void DeleteProduct_ShouldRemoveProductFromList()
        {
            // Arrange
            var orderManager = new OrderManager();
            var product = new Product { Id = 1, Name = "Product A" };
            orderManager.CreateProduct(product);

            // Act
            orderManager.DeleteProduct(1);

            // Assert
            var fetchedProduct = orderManager.GetProductById(1);
            Assert.Null(fetchedProduct);
        }
    }

}