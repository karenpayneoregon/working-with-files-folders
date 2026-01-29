using System.Diagnostics;

namespace ReadOrdersBetweenDatesApp.Classes;


public static class DataGridViewExtensions
{

    public static void ExpandColumns(this DataGridView source, bool sizable = true)
    {
        foreach (DataGridViewColumn col in source.Columns)
        {
            if (col.ValueType.Name == "ICollection`1") continue;
            if (col.DataPropertyName == "Image") continue;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        if (!sizable) return;

        for (int index = 0; index <= source.Columns.Count - 1; index++)
        {
            var temp = source.Columns[index];
            if (temp.DataPropertyName == "Image")
            {
                continue;
            }
            int columnWidth = source.Columns[index].Width;
            source.Columns[index].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            source.Columns[index].Width = columnWidth;
        }
    }
    /// <summary>
    /// Split on upper-cased letters
    /// </summary>
    /// <param name="source"></param>
    public static void FixHeaders(this DataGridView source)
    {

        for (int index = 0; index < source.Columns.Count; index++)
        {
            source.Columns[index].HeaderText = SplitCamelCase(source.Columns[index].HeaderText);
        }
    }

    [DebuggerStepThrough]
    public static string SplitCamelCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        Span<char> result = stackalloc char[input.Length * 2];
        var resultIndex = 0;

        for (var index = 0; index < input.Length; index++)
        {
            var currentChar = input[index];

            if (index > 0 && char.IsUpper(currentChar))
            {
                result[resultIndex++] = ' ';
            }

            result[resultIndex++] = currentChar;
        }

        return result[..resultIndex].ToString();
    }
}