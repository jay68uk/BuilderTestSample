using BuilderTestSample.Model;

namespace BuilderTestSample.Tests.TestBuilders;

public class CustomerBuilder
{
  private const int TestCustomerId = 123;
  private static readonly (string, string) _customerName = ("Joe", "Smith");
  
  private int _id;
  private Address _homeAddress;
  private string _firstName;
  private string _lastName;
  private int _creditRating;
  private decimal _totalPurchases;

  public CustomerBuilder WithId(int id)
  {
    _id = id;
    return this;
  }
  
  public CustomerBuilder WithTestValues()
  {
    _id = TestCustomerId;
    _firstName = _customerName.Item1;
    _lastName = _customerName.Item2;
    _homeAddress = new AddressBuilder().WithTestValues().Build();
    _creditRating = 200;
    _totalPurchases = 23;
    return this;
  }

  public CustomerBuilder WithAddress(Address address)
  {
    _homeAddress = address;
    return this;
  }

  public CustomerBuilder WithName(string firstName, string lastName)
  {
    _firstName = firstName;
    _lastName = lastName;
    return this;
  }

  public CustomerBuilder WithCreditRating(int rating)
  {
    _creditRating = rating;
    return this;
  }
  
  public CustomerBuilder WithTotalPurchases(decimal purchases)
  {
    _totalPurchases = purchases;
    return this;
  }

  public Customer Build()
  {
    return new Customer(_id)
    {
      HomeAddress = _homeAddress,
      FirstName = _firstName,
      LastName = _lastName,
      CreditRating = _creditRating,
      TotalPurchases = _totalPurchases
    };
  }

}