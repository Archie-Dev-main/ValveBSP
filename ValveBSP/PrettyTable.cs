namespace ValveBSP;

public class PrettyTable
{
    public int Spacing { get; set; } = 4;

    public string[][] Columns
    {
        get
        {
            return [.. _columns];
        }
    }

    private int _numColumns = 0;
    private readonly List<string> _headers = [];
    private readonly List<string[]> _columns = [];

    public void AddColumn<T>(string header, T[] column)
    {
        string[] strings = new string[column.Length];

        if (column == null)
        {
            return;
        }

        _numColumns++;
        _headers.Add(header);

        for (int i = 0; i < strings.Length; ++i)
        {
            T? nullTest = column[i];

            if (nullTest == null)
                continue;

            string? str = nullTest.ToString();

            if (str == null)
            {
                strings[i] = "null";
                continue;
            }

            strings[i] = str;
        }

        _columns.Add(strings);
    }

    private int MaxRowLen(int column)
    {
        int max = 0, len;

        for (int j = 0; j < _columns[column].Length; ++j)
        {
            len = _columns[column][j].Length;

            if (len > max)
                max = len;
        }

        return max;
    }

    private int MaxColumnLen()
    {
        int max = 0, len;

        for (int i = 0; i < _columns.Count; ++i)
        {
            len = _columns[i].Length;

            if (len > max)
                max = len;
        }

        return max;
    }

    public void Print()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        string str = string.Empty;
        int maxHeight = MaxColumnLen();

        int[] maxRows = new int[_numColumns];

        for (int i = 0; i < _numColumns; ++i)
        {
            maxRows[i] = MaxRowLen(i);

            if (_headers[i].Length > maxRows[i])
            {
                maxRows[i] = _headers[i].Length;
            }

            str = $"{str}{_headers[i]}{new string(' ', maxRows[i] - _headers[i].Length + Spacing)}";
        }

        str = $"{str}\n";

        for (int i = 0; i < maxHeight; ++i)
        {
            for (int j = 0; j < _numColumns; ++j)
            {
                if (i > _columns[j].Length)
                {
                    str = $"{str}null{new string(' ', maxRows[j] - 4 + Spacing)}";
                    continue;
                }

                str = $"{str}{_columns[j][i]}{new string(' ', maxRows[j] - _columns[j][i].Length + Spacing)}";
            }

            str = $"{str}\n";
        }

        return str;
    }
}
