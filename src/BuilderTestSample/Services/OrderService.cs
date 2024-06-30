using BuilderTestSample.Exceptions;
using BuilderTestSample.Model;

namespace BuilderTestSample.Services
{
    public class OrderService
    {
        public void PlaceOrder(Order order)
        {
            ValidateOrder(order);

            ExpediteOrder(order);

            AddOrderToCustomerHistory(order);
        }

        private void ValidateOrder(Order order)
        {
            // throw InvalidOrderException unless otherwise noted.
            
            if (order.Id != 0) throw new InvalidOrderException("Order ID must be 0.");
            
            if (order.TotalAmount <= 0)
            {
                throw new InvalidOrderException("Order Total Amount must be greater than 0.");
            }

            if (order.Customer is null)
            {
                throw new InvalidOrderException("Order must have a customer associated with it.");
            }
            ValidateCustomer(order.Customer);
        }

        private void ValidateCustomer(Customer customer)
        {
            // throw InvalidCustomerException unless otherwise noted
            // create a CustomerBuilder to implement the tests for these scenarios
            
            if (customer.Id<=0)
            {
                throw new InvalidCustomerException("Customer Id must be greater than 0!");    
            }

            if (customer.HomeAddress is null)
            {
                throw new InvalidCustomerException("Customer Address cannot be null!");
            }

            if (string.IsNullOrEmpty(customer.FirstName) || string.IsNullOrEmpty(customer.LastName))
            {
                throw new InvalidCustomerException("Customer must have first and last name!");
            }

            if (customer.CreditRating<200)
            {
                throw new InsufficientCreditException("Customer has insufficient credit rating!");
            }
            
            if (customer.TotalPurchases<=0)
            {
                throw new InvalidCustomerException("Customer must have total purchases greater than 0!");
            }

            ValidateAddress(customer.HomeAddress);
        }

        private void ValidateAddress(Address homeAddress)
        {
            // throw InvalidAddressException unless otherwise noted
            // create an AddressBuilder to implement the tests for these scenarios

            if (string.IsNullOrEmpty(homeAddress.Street1))
            {
                throw new InvalidAddressException("Customer must have Address, Street1!");
            }
            
            if (string.IsNullOrEmpty(homeAddress.City))
            {
                throw new InvalidAddressException("Customer must have Address, City!");
            }
            
            if (string.IsNullOrEmpty(homeAddress.State))
            {
                throw new InvalidAddressException("Customer must have Address, State!");
            }
            
            if (string.IsNullOrEmpty(homeAddress.PostalCode))
            {
                throw new InvalidAddressException("Customer must have Address, Postcode!");
            }
            
            if (string.IsNullOrEmpty(homeAddress.Country))
            {
                throw new InvalidAddressException("Customer must have Address, Country!");
            }
        }

        private void ExpediteOrder(Order order)
        {
            if (order.Customer.TotalPurchases>5000 
                && order.Customer.CreditRating>500)
            {
                order.IsExpedited = true;
            }
            else
            {
                order.IsExpedited = false;
            }
        }

        private void AddOrderToCustomerHistory(Order order)
        {
            order.Customer.OrderHistory.Add(order);

            order.Customer.TotalPurchases += 1;
        }
    }
}
