using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
    }

    public void AddProduct(Product product) => _products.Add(product);

    public decimal GetShippingCost() => _customer.LivesInUSA() ? 5m : 35m;

    public decimal GetTotalCost() => _products.Sum(p => p.GetTotalCost()) + GetShippingCost();

    public string GetPackingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("PACKING LABEL");
        foreach (var p in _products)
        {
            sb.AppendLine($"{p.GetName()} (ID: {p.GetProductId()})");
        }
        return sb.ToString();
    }

    public string GetShippingLabel()
    {
        var sb = new StringBuilder();
        sb.AppendLine("SHIPPING LABEL");
        sb.AppendLine(_customer.GetName());
        sb.AppendLine(_customer.GetAddress().ToLabelString());
        return sb.ToString();
    }
}
