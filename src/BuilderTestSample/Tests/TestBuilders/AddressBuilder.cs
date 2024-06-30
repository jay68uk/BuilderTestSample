using BuilderTestSample.Model;

namespace BuilderTestSample.Tests.TestBuilders;

public class AddressBuilder
{
  private string _street1;
  private string _city;
  private string _state;
  private string _postcode;
  private string _country;

  public AddressBuilder WithTestValues()
  {
    _street1 = "1 The street";
    _city = "Cityville";
    _state = "Stateshire";
    _postcode = "NN1 1NN";
    _country = "UK";
    return this;
  }

  public AddressBuilder WithStreet1(string street1)
  {
    _street1 = street1;
    return this;
  }
  
  public AddressBuilder WithCity(string city)
  {
    _city = city;
    return this;
  }
  
  public AddressBuilder WithState(string state)
  {
    _state = state;
    return this;
  }
  
  public AddressBuilder WithPostcode(string postcode)
  {
    _postcode = postcode;
    return this;
  }
  
  public AddressBuilder WithCountry(string country)
  {
    _country = country;
    return this;
  }
  
  public Address Build()
  {
    return new Address()
    {
      Street1 = _street1,
      City = _city,
      State = _state,
      PostalCode = _postcode,
      Country = _country
    };
  }
}