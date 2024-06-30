using System;
using BuilderTestSample.Exceptions;
using BuilderTestSample.Model;
using BuilderTestSample.Services;
using BuilderTestSample.Tests.TestBuilders;
using Xunit;

namespace BuilderTestSample.Tests
{
    public class OrderServicePlaceOrder
    {
        private readonly OrderService _orderService = new ();
        private readonly OrderBuilder _orderBuilder = new ();
        private readonly CustomerBuilder _customerBuilder = new();
        private readonly AddressBuilder _addressBuilder = new();

        private OrderBuilder _validOrder = new OrderBuilder();
        
        [Fact]
        public void ThrowsExceptionGivenOrderWithExistingId()
        {
            var order = _orderBuilder
                            .WithId(123)
                            .Build();

            Assert.Throws<InvalidOrderException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAmountZero()
        {
            var order = _orderBuilder
                .WithId(0)
                .WithAmount(0)
                .Build();

            Assert.Throws<InvalidOrderException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAmountLessThanZero()
        {
            var order = _orderBuilder
                .WithId(0)
                .WithAmount(-10)
                .Build();

            Assert.Throws<InvalidOrderException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerIsNull()
        {
            var order = _orderBuilder
                .WithId(0)
                .WithAmount(10)
                .WithCustomer(null)
                .Build();

            Assert.Throws<InvalidOrderException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerIdIsZero()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithId(0)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidCustomerException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerAddressIsNull()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithAddress(null)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidCustomerException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerFirstOrLastNameMissing()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithName(string.Empty, string.Empty)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidCustomerException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerCreditRatingBelow200()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithCreditRating(199)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InsufficientCreditException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerPurchasesAreZero()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithTotalPurchases(0)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidCustomerException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenCustomerPurchasesAreLessThanZero()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithTotalPurchases(-10)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidCustomerException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAddressStreet1IsNullOrEmpty()
        {
            var address = _addressBuilder
                .WithTestValues()
                .WithStreet1(string.Empty)
                .Build();
            
            var customer = _customerBuilder
                .WithTestValues()
                .WithAddress(address)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidAddressException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAddressCityIsNullOrEmpty()
        {
            var address = _addressBuilder
                .WithTestValues()
                .WithCity(string.Empty)
                .Build();
            
            var customer = _customerBuilder
                .WithTestValues()
                .WithAddress(address)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidAddressException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAddressStateIsNullOrEmpty()
        {
            var address = _addressBuilder
                .WithTestValues()
                .WithState(string.Empty)
                .Build();
            
            var customer = _customerBuilder
                .WithTestValues()
                .WithAddress(address)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidAddressException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAddressPostcodeIsNullOrEmpty()
        {
            var address = _addressBuilder
                .WithTestValues()
                .WithPostcode(string.Empty)
                .Build();
            
            var customer = _customerBuilder
                .WithTestValues()
                .WithAddress(address)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidAddressException>(() => _orderService.PlaceOrder(order));
        }
        
        [Fact]
        public void ThrowsExceptionWhenAddressCountryIsNullOrEmpty()
        {
            var address = _addressBuilder
                .WithTestValues()
                .WithCountry(string.Empty)
                .Build();
            
            var customer = _customerBuilder
                .WithTestValues()
                .WithAddress(address)
                .Build();

            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            
            Assert.Throws<InvalidAddressException>(() => _orderService.PlaceOrder(order));
        }

        [Fact]
        public void OrderExpediteOrderShouldBeFalse_WhenTotalPurchaseBelow5000_And_CreditRatingBelow500()
        {
            var order = _validOrder.Build();
            _orderService.PlaceOrder(order);
            Assert.False(order.IsExpedited);
        }
        
        [Fact]
        public void OrderExpediteOrderShouldBeTrue_WhenTotalPurchaseOver5000_And_CreditRatingOver500()
        {
            var customer = _customerBuilder
                .WithTestValues()
                .WithTotalPurchases(5001)
                .WithCreditRating(501)
                .Build();
            
            var order = _validOrder
                .WithCustomer(customer)
                .Build();
            _orderService.PlaceOrder(order);
            Assert.True(order.IsExpedited);

        }

        [Fact]
        public void OrderAddedToCustomerHistory_WhenOrderIsValid()
        {
            var order = _validOrder.Build();
            _orderService.PlaceOrder(order);
            
            Assert.Single(order.Customer.OrderHistory, order);
        }
        
        [Fact]
        public void CustomerTotalPurchasesUpdated_WhenOrderIsValid()
        {
            var order = _validOrder.Build();
            _orderService.PlaceOrder(order);

            Assert.Equal(24, order.Customer.TotalPurchases);
        }
    }
}
