using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Commands.AddNewProduct;
using System;
using FluentValidation.Results;

namespace Test.Validators
{
    [TestClass]
    public class TestAddNewProductCommandValidator
    {
        [TestMethod]
        public void ShouldBeValidInput()
        {
            var request = new AddNewProductCommand
            {
                Description = "Description",
                Name = "Name",
                Id = Guid.Empty
            };
            AddNewProductCommandValidator validator = new AddNewProductCommandValidator();

            ValidationResult results = validator.Validate(request);

            Assert.IsTrue(results.IsValid);
        }

        [DataRow("", "Product Name must be specifed")]
        [DataRow("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "Product Name exceeds the authorized size 100")]
        [DataTestMethod]
        public void ShouldBeInvalidName(string productName, string errorMessage)
        {
            var request = new AddNewProductCommand
            {
                Description = "Description",
                Name = productName,
                Id = Guid.Empty
            };
            AddNewProductCommandValidator validator = new AddNewProductCommandValidator();

            ValidationResult results = validator.Validate(request);

            Assert.IsFalse(results.IsValid);
            Assert.IsNotNull(results.Errors.Find(e => e.ErrorMessage == errorMessage));
        }

        [TestMethod]
        public void ShouldBeInvalidDescription()
        {
            string mockDescription = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            var request = new AddNewProductCommand
            {
                Description = mockDescription,
                Name = "Name",
                Id = Guid.Empty
            };
            AddNewProductCommandValidator validator = new AddNewProductCommandValidator();

            ValidationResult results = validator.Validate(request);

            Assert.IsFalse(results.IsValid);
            Assert.IsNotNull(results.Errors.Find(e => e.ErrorMessage == "Product Description exceeds the authorized size which is 400"));
        }
    }
}