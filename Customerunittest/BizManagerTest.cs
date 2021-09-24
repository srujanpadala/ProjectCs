using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomerService.Manager;
using CustomerService.Models;
using CustomerService.Standard;
using FluentAssertions;
using NUnit.Framework;

namespace CustomerServiceTests
{
    class BizManagerTest
    {
        private IBizManager<Customer> _bizManager;

        [SetUp]
        public void Setup()
        {
            _bizManager = new custBizManager();
        }
        [Test]
        public void ShouldGenarateNewGuid_When_AddingNewCustomer()
        {
            var customer = new Customer() { FirstName = "srujan", LastName = "padala", DOB = "17/11/1998", SSN = "235-52-52489" };

            _bizManager.Add(customer);

            _bizManager.GetAll().Count.Should().Be(1);
            var result = _bizManager.GetAll().FirstOrDefault();
            result.Should().NotBeNull();
            result.Id.Should().NotBeNullOrEmpty();
            result.Id.Should().BeOfType<string>();
        }
        [Test]
        public void ShouldReturnNull_When_GetCustomerBYIDForNotExistingID()
        {
            var mockCustomer = new Customer() { FirstName = "srujan", LastName = "padala", DOB = "17/11/1998", SSN = "235-52-52489" };
            _bizManager.Add(mockCustomer);

            var result = _bizManager.GetByID("1234567890");

            result.Should().BeNull();
        }
        [Test]
        public void ShouldReturnCustomer_When_GetCustomerByIDForExistingID()
        {
            var customer = new Customer() { FirstName = "srujan", LastName = "padala", DOB = "17/11/1998", SSN = "235-52-52489" };
            _bizManager.Add(customer);
            var id = _bizManager.GetAll().FirstOrDefault().Id;

            var result = _bizManager.GetByID(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
        }
        [Test]
        public void ShouldReturnFalse_When_DeletingCustomerByNotExistingID()
        {
            var customer = new Customer() { FirstName = "srujan", LastName = "padala", DOB = "17/11/1998", SSN = "235-52-52489" };
            _bizManager.Add(customer);

            var result = _bizManager.DeleteByID("123456789");

            result.Should().BeFalse();
        }

        [Test]
        public void ShouldReturnTrue_When_DeletingCustomerByExistingID()
        {
            var customer = new Customer() { FirstName = "srujan", LastName = "padala", DOB = "17/11/1998", SSN = "235-52-52489" };
            _bizManager.Add(customer);
            var id = _bizManager.GetAll().FirstOrDefault().Id;

            var result = _bizManager.DeleteByID(id);

            result.Should().BeTrue();
        }
    }
}