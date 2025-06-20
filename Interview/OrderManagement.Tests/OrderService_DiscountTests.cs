using System;
using System.Collections.Generic;
using OrderManagement.API.Models;
using OrderManagement.API.Services;
using Xunit;

namespace OrderManagement.Tests
{
    public class OrderService_DiscountTests
    {
        private readonly OrderService _service = new();

        [Fact]
        public void VIPCustomer_WithHighTotalHistory_Gets15PercentDiscount()
        {
            // Arrange
            var customer = new Customer { Segment = CustomerSegment.VIP };
            var order = new Order { TotalAmount = 1000 };
            var history = new List<Order>
            {
                new Order { TotalAmount = 7000 },
                new Order { TotalAmount = 4000 }
            };

            // Act
            var result = _service.ApplyDiscount(order, customer, history);

            // Assert
            Assert.Equal(850, result);
        }

        [Fact]
        public void RegularCustomer_WithMoreThan5Orders_Gets5PercentDiscount()
        {
            // Arrange
            var customer = new Customer { Segment = CustomerSegment.Regular };
            var order = new Order { TotalAmount = 1000 };
            var history = new List<Order>
            {
                new Order { TotalAmount = 200 },
                new Order { TotalAmount = 300 },
                new Order { TotalAmount = 400 },
                new Order { TotalAmount = 500 },
                new Order { TotalAmount = 600 },
                new Order { TotalAmount = 700 },
            };

            // Act
            var result = _service.ApplyDiscount(order, customer, history);

            // Assert
            Assert.Equal(950, result);
        }

        [Fact]
        public void NewCustomer_OrLowHistory_GetsNoDiscount()
        {
            // Arrange
            var customer = new Customer { Segment = CustomerSegment.New };
            var order = new Order { TotalAmount = 1000 };
            var history = new List<Order>
            {
                new Order { TotalAmount = 100 },
                new Order { TotalAmount = 200 }
            };

            // Act
            var result = _service.ApplyDiscount(order, customer, history);

            // Assert
            Assert.Equal(1000, result);
        }

        [Fact]
        public void VIPCustomer_WithLowHistory_GetsNoDiscount()
        {
            // Arrange
            var customer = new Customer { Segment = CustomerSegment.VIP };
            var order = new Order { TotalAmount = 1000 };
            var history = new List<Order>
            {
                new Order { TotalAmount = 3000 },
                new Order { TotalAmount = 4000 }
            };

            // Act
            var result = _service.ApplyDiscount(order, customer, history);

            // Assert
            Assert.Equal(1000, result);
        }
    }
}