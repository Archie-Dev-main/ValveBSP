using System.Reflection;

namespace ValveBSP;

public abstract class LumpReader
{
    public static Type[] GetTypes(object[] values)
    {
        Type[] types = new Type[values.Length];

        for (int i = 0; i < values.Length; ++i)
        {
            types[i] = values[i].GetType();
        }

        return types;
    }

    public abstract object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item);
}

public class StandardReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        uint numItems = lump.FileLength / (uint)item.GetSize();
        object[] items = new object[numItems];

        for (int i = 0; i < numItems; ++i)
        {
            items[i] = ReadLumpItem(reader, item);
        }

        return items;
    }

    public static object ReadLumpItem(BinaryReader reader, LumpItemArchetype item)
    {
        object[] values = item.Read(reader);

        if (item.IsSimple)
        {
            return values[0];
        }

        ConstructorInfo? ctor = item.ItemType.GetConstructor(GetTypes(values)) ?? throw new Exception("No acceptable constructor to use for mapping.");

        return ctor.Invoke(values);
    }
}

public class ByteReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        return reader.ReadBytes((int)lump.FileLength);
    }
}