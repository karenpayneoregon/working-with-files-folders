using Dapper;
using kp.Dapper.Handlers;
using Microsoft.Data.SqlClient;
using ReadOrdersBetweenDatesApp.Classes.Configuration;
using ReadOrdersBetweenDatesApp.Models;
using System.Data;
using System.Text;

namespace ReadOrdersBetweenDatesApp.Classes;

public static class OrdersCsvExporter
{
    /// <summary>
    /// Exports order data to a CSV file.
    /// </summary>
    /// <param name="outputFilePath">
    /// The full file path where the CSV file will be created or overwritten.
    /// </param>
    /// <remarks>
    /// This method retrieves order data from the database, formats it, and writes it to a CSV file.
    /// The output includes details such as order dates, shipping information, and customer company names.
    /// </remarks>
    public static void ExportOrdersToCsv(string outputFilePath)
    {
        // Allow Dapper to map DateOnly and TimeOnly types
        SqlMapper.AddTypeHandler(new SqlDateOnlyTypeHandler());
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());
        
        using IDbConnection connection = new SqlConnection(DataConnections.Instance.MainConnection);

        IEnumerable<OrdersResults> results = connection.Query<OrdersResults>(SqlStatements.GetOrdersBetweenDates);

        using var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8);

        foreach (var row in results)
        {
            writer.WriteLine(string.Join(",",
                row.OrderID,
                FormatDate(row.OrderDate),
                FormatDate(row.RequiredDate),
                FormatDate(row.ShippedDate),
                Escape(row.ShipAddress),
                Escape(row.ShipCity),
                Escape(row.ShipPostalCode),
                Escape(row.ShipCountry),
                Escape(row.CompanyName)
            ));
        }
    }

    private static string FormatDate(DateOnly date)
        => date == default ? string.Empty : date.ToString("yyyy-MM-dd");

    private static string Escape(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        // Standard CSV escaping
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n'))
        {
            value = value.Replace("\"", "\"\"");
            return $"\"{value}\"";
        }

        return value;
    }
}