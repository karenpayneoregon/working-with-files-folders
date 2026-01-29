
using ReadOrdersBetweenDatesApp.Classes;
using ReadOrdersBetweenDatesApp.Components;
using ReadOrdersBetweenDatesApp.Models;
using System.ComponentModel;
using System.Windows.Forms;

namespace ReadOrdersBetweenDatesApp;

public partial class MainForm : Form
{
    private SortableBindingList<OrdersResults> _ordersResults;
    private BindingSource _ordersBindingSource = new BindingSource();
    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        
        dataGridView1.AllowUserToAddRows = false;
        
        BindingNavigator1.AboutItemButton.Click += AboutItemButton_Click;
        BindingNavigator1.CurrentItemButton.Click += CurrentItemButton_Click;
        
        //OrdersCsvExporter.ExportOrdersToCsv("output.csv");
        
        ImportOrders();
    }

    private void CurrentItemButton_Click(object? sender, EventArgs e)
    {
        var current = _ordersBindingSource.Current as OrdersResults;
        Dialogs.Information(this, $"{current!.OrderID} {current.CompanyName}");
    }

    private void AboutItemButton_Click(object? sender, EventArgs e)
    {
        Dialogs.Information(this,"Shows creating a CSV file and reading the orders from the file.");
    }

    /// <summary>
    /// Imports orders from a CSV file, updates the data bindings, and displays any errors encountered during the import process.
    /// </summary>
    /// <remarks>
    /// This method utilizes the <see cref="Importer.Execute"/> method to parse a CSV file and retrieve valid orders 
    /// along with the line numbers of invalid entries. It then updates the data bindings for the UI components 
    /// to reflect the imported data.
    /// </remarks>
    /// <exception cref="FileNotFoundException">
    /// Thrown if the CSV file specified in the <see cref="Importer.Execute"/> method does not exist.
    /// </exception>
    /// <seealso cref="Importer.Execute"/>
    private void ImportOrders()
    {
        var (validOrders, badLineNumbers) = Importer.Execute();
        
        var badLines = badLineNumbers.Count;

        if (badLines > 0)
        {
            Dialogs.Information(this, $"Found {badLines} bad lines in the CSV file.");
        }

        _ordersResults = new SortableBindingList<OrdersResults>(validOrders);
        _ordersBindingSource.DataSource = _ordersResults;
        
        BindingNavigator1.BindingSource = _ordersBindingSource;
        dataGridView1.DataSource = _ordersBindingSource;
        
        dataGridView1.ExpandColumns();
        
    }
}
