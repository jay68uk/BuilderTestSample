using BuilderTestSample.Model;

namespace BuilderTestSample.Tests.TestBuilders
{
    /// <summary>
    /// Reference: https://ardalis.com/improve-tests-with-the-builder-pattern-for-test-data
    /// </summary>
    public class OrderBuilder
    {
        private Order _order = new ();

        public OrderBuilder()
        {
            _order.TotalAmount = 100m;
            _order.Id = 0;
            _order.IsExpedited = false;
            
            _order.Customer = new CustomerBuilder()
                .WithTestValues()
                .Build();
            _order.Customer.HomeAddress = new AddressBuilder()
                .WithTestValues()
                .Build();
        }

        public OrderBuilder WithId(int id)
        {
            _order.Id = id;
            return this;
        }

        public OrderBuilder WithAmount(decimal amount)
        {
            _order.TotalAmount = amount;
            return this;
        }
        
        public OrderBuilder WithCustomer(Customer customer)
        {
            _order.Customer = customer;
            return this;
        }

        public Order Build()
        {
            return _order;
        }
    }
}
