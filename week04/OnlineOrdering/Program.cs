using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        var addrUsa = new Address("123 Maple St", "Provo", "UT", "USA");
        var custUsa = new Customer("Anna Johnson", addrUsa);

        var addrItaly = new Address("Via Garibaldi 12", "Narni", "TR", "Italy");
        var custItaly = new Customer("Francesco Foresta", addrItaly);

        var order1 = new Order(custUsa);
        order1.AddProduct(new Product("USB-C Cable", "UC-001", 7.99m, 3));
        order1.AddProduct(new Product("Wireless Mouse", "WM-210", 24.50m, 1));
        order1.AddProduct(new Product("Notebook", "NB-045", 3.25m, 4));

        var order2 = new Order(custItaly);
        order2.AddProduct(new Product("Pasta Maker", "PM-777", 59.90m, 1));
        order2.AddProduct(new Product("Chef Knife", "CK-900", 34.99m, 2));

        var orders = new List<Order> { order1, order2 };
        var usCulture = CultureInfo.GetCultureInfo("en-US");

        int i = 1;
        foreach (var order in orders)
        {
            Console.WriteLine($"===== ORDER {i} =====");
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Shipping: {order.GetShippingCost().ToString("C", usCulture)}");
            Console.WriteLine($"TOTAL: {order.GetTotalCost().ToString("C", usCulture)}");
            Console.WriteLine(new string('-', 40));
            i++;
        }
    }
}
