using System.Diagnostics;
using System.Globalization;
using ReadOrdersBetweenDatesApp.Models;
using Serilog;

namespace ReadOrdersBetweenDatesApp.Classes;

public class Importer
{
    /// <summary>
    /// Imports orders from a CSV file and returns the valid orders along with the line numbers of invalid entries.
    /// </summary>
    /// <param name="filePath">
    /// The path to the CSV file to import. Defaults to "data.csv" if not specified.
    /// </param>
    /// <returns>
    /// A tuple containing:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <see cref="List{T}"/> of <see cref="OrdersResults"/> representing the valid orders.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <see cref="List{T}"/> of <see cref="int"/> representing the line numbers of invalid entries in the CSV file.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// The method validates the structure and data types of each row in the CSV file. Rows with missing or invalid data
    /// are skipped, and their line numbers are recorded in the returned list of invalid entries.
    ///
    /// Optionally wrap the file reading and parsing logic in try-catch blocks to handle potential IO exceptions
    /// or parsing errors more gracefully.
    /// </remarks>
    public static (List<OrdersResults> validOrders, List<int> badLineNumbers) Execute(string filePath = "output.csv")
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

            // validate data types before parsing
            if (!int.TryParse(columns[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int orderId))
            {
                badLineNumbers.Add(Index);
                continue;
            }

            if (!DateOnly.TryParse(columns[1], CultureInfo.InvariantCulture, out var orderDate))
            {
                badLineNumbers.Add(Index);
                continue;
            }

            if (!DateOnly.TryParse(columns[2], CultureInfo.InvariantCulture, out var requiredDate))
            {
                badLineNumbers.Add(Index);
                continue;
            }
            
            // Try parsing ShippedDate, allowing for potential null or invalid date entries
            if (!DateOnly.TryParse(columns[3], CultureInfo.InvariantCulture, out var shippedDate))
            {
                badLineNumbers.Add(Index);
                continue;
            }

            
            var order = new OrdersResults
            {
                OrderID = orderId,
                OrderDate = orderDate,
                RequiredDate = requiredDate,
                ShippedDate = shippedDate,
                ShipAddress = columns[4],
                ShipCity = columns[5],
                ShipPostalCode = columns[6],
                ShipCountry = columns[7],
                CompanyName = columns[8]
            };

            results.Add(order);
        }

        badLineNumbers = badLineNumbers.Distinct().ToList();

        if (badLineNumbers.Count >0)
        {
            Log.Information("Imported orders from {F} with {BL} bad lines.", filePath, string.Join(", ", badLineNumbers));
        }

        return (results, badLineNumbers.Distinct().ToList());
    }
    
    
}