using System.Diagnostics;
using System.Globalization;
using ReadOrdersBetweenDatesApp.Models;

namespace ReadOrdersBetweenDatesApp.Classes;

public class Importer
{
    public static (List<OrdersResults> validOrders, List<int> badLineNumbers) Import(string filePath = "data.csv")
    {
        List<int> badLineNumbers = [];
        
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        var results = new List<OrdersResults>();
        var lines = File.ReadAllLines(filePath);

        foreach ((int Index, string line) in lines.Index())
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var columns = line.Split(',');

            // Defensive programming: CSV row must have exactly 9 columns
            if (columns.Length != 9)
            {
                badLineNumbers.Add(Index);
                continue;
            }

            var order = new OrdersResults
            {
                OrderID = int.Parse(columns[0], CultureInfo.InvariantCulture),
                OrderDate = DateOnly.Parse(columns[1], CultureInfo.InvariantCulture),
                RequiredDate = DateOnly.Parse(columns[2], CultureInfo.InvariantCulture),
                ShippedDate = DateOnly.Parse(columns[3], CultureInfo.InvariantCulture),
                ShipAddress = columns[4],
                ShipCity = columns[5],
                ShipPostalCode = columns[6],
                ShipCountry = columns[7],
                CompanyName = columns[8]
            };

            results.Add(order);
        }

        return (results, badLineNumbers);
    }
}