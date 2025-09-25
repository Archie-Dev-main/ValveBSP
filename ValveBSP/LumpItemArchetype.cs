namespace ValveBSP;

public readonly struct LumpItemArchetype(Type itemType, params LumpItemField[] fields)
{
    public readonly Type ItemType = itemType;
    public readonly LumpItemField[] Fields = fields;

    public bool IsSimple
    {
        get
        {
            return Fields.Length == 1 && ItemType.IsPrimitive;
        }
    }

    public readonly object[] Read(BinaryReader reader)
    {
        ReadOnlySpan<LumpItemField> fields = Fields;
        object[] values = new object[fields.Length];

        for (int i = 0; i < fields.Length; ++i)
        {
            values[i] = fields[i].ReadValue(reader);
        }

        return values;
    }

    public readonly int GetSize()
    {
        int size = 0;

        for (int i = 0; i < Fields.Length; ++i)
        {
            size += Fields[i].GetSize();
        }

        return size;
    }
}
