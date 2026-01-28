
using ReadOrdersBetweenDatesApp.Classes;

namespace ReadOrdersBetweenDatesApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        Shown += MainForm_Shown;
    }

    private void MainForm_Shown(object? sender, EventArgs e)
    {
        var (validOrders, badLineNumbers) = Importer.Import();
        
        var badLines = badLineNumbers.Count;

        if (badLines > 0)
        {
            Dialogs.Information(this, $"Found {badLines} bad lines in the CSV file.");
        }

        Dialogs.Information(this, $"Successfully imported {validOrders.Count} orders.");
    }
}
