using System;

public class Address
{
    private string _street;
    private string _city;
    private string _stateOrProvince;
    private string _country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        _street = street;
        _city = city;
        _stateOrProvince = stateOrProvince;
        _country = country;
    }

    public bool IsInUSA()
    {
        if (string.IsNullOrWhiteSpace(_country)) return false;
        var c = _country.Trim().ToUpperInvariant();
        return c == "USA" || c == "UNITED STATES" || c == "UNITED STATES OF AMERICA";
    }

    public string ToLabelString()
    {
        return $"{_street}\n{_city}, {_stateOrProvince}\n{_country}";
    }
}
