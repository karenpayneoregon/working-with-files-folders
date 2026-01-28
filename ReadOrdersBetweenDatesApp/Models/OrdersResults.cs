namespace ReadOrdersBetweenDatesApp.Models;
public class OrdersResults
{
    public int OrderID { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly RequiredDate { get; set; }

    public DateOnly ShippedDate { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    public string CompanyName { get; set; } = string.Empty;
}
