namespace SortUsingNet10;

internal partial class Program
{
    static void Main(string[] args)
    {
        AnsiConsole.MarkupLine("[yellow]Hello[/]");
        var fileNames =
            """
                File1Test.txt
                File10Test.txt
                File2Test.txt
                File20Test.txt
                File3Test.txt
                File30Test.txt
                File4Test.txt
                File40Test.txt
                File5Test.txt
                File50Test.txt
                """.Split(Environment.NewLine);
        Console.ReadLine();
    }
}