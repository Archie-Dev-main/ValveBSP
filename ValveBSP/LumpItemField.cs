global using static ValveBSP.PrimitiveFields;

namespace ValveBSP;

public abstract class LumpItemField
{
    public abstract object ReadValue(BinaryReader reader);
    public abstract int GetSize();
}

public abstract class NumberField(bool unsigned) : LumpItemField
{
    public abstract object Signed(BinaryReader reader);
    public abstract object UnSigned(BinaryReader reader);

    public override object ReadValue(BinaryReader reader)
    {
        return unsigned ? UnSigned(reader) : Signed(reader);
    }
}

public class CharField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return reader.ReadChar();
    }

    public override int GetSize()
    {
        return sizeof(char);
    }
}

public class BoolField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return reader.ReadBoolean();
    }

    public override int GetSize()
    {
        return sizeof(bool);
    }
}

public class ByteField(bool unsigned = true) : NumberField(unsigned)
{
    public override object Signed(BinaryReader reader)
    {
        return reader.ReadSByte();
    }

    public override object UnSigned(BinaryReader reader)
    {
        return reader.ReadByte();
    }

    public override int GetSize()
    {
        return sizeof(byte);
    }
}

public class ShortField(bool unsigned = false) : NumberField(unsigned)
{
    public override object Signed(BinaryReader reader)
    {
        return reader.ReadInt16();
    }

    public override object UnSigned(BinaryReader reader)
    {
        return reader.ReadUInt16();
    }

    public override int GetSize()
    {
        return sizeof(short);
    }
}

public class IntField(bool unsigned = false) : NumberField(unsigned)
{
    public override object Signed(BinaryReader reader)
    {
        return reader.ReadInt32();
    }

    public override object UnSigned(BinaryReader reader)
    {
        return reader.ReadUInt32();
    }

    public override int GetSize()
    {
        return sizeof(int);
    }
}

public class LongField(bool unsigned = false) : NumberField(unsigned)
{
    public override object Signed(BinaryReader reader)
    {
        return reader.ReadInt64();
    }

    public override object UnSigned(BinaryReader reader)
    {
        return reader.ReadUInt64();
    }

    public override int GetSize()
    {
        return sizeof(long);
    }
}

public class FloatField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return reader.ReadSingle();
    }

    public override int GetSize()
    {
        return sizeof(float);
    }
}

public class DoubleField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return reader.ReadDouble();
    }

    public override int GetSize()
    {
        return sizeof(double);
    }
}

public static class PrimitiveFields
{
    public static readonly CharField GCharField = new();
    public static readonly ByteField GByteField = new();
    public static readonly ByteField GSByteField = new(false);
    public static readonly ShortField GShortField = new();
    public static readonly ShortField GUShortField = new(true);
    public static readonly IntField GIntField = new();
    public static readonly IntField GUIntField = new(true);
    public static readonly LongField GLongField = new();
    public static readonly LongField GULongField = new(true);
    public static readonly FloatField GFloatField = new();
    public static readonly DoubleField GDoubleField = new();
}

public class ArrayField(Type type, LumpItemField field, int length) : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        Array array = Array.CreateInstance(type, length);

        for (int i = 0; i < length; ++i)
        {
            array.SetValue(field.ReadValue(reader), i);
        }

        return array;
    }

    public override int GetSize()
    {
        return field.GetSize() * length;
    }
}

public class MultiArrayField(Type type, LumpItemField field, params int[] lengths) : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        ArrayField array = new(type, field, lengths[^1]);
        Type arrayType = type.MakeArrayType();

        for (int i = lengths.Length - 2; i >= 0; --i)
        {
            array = new(arrayType, array, lengths[i]);
            arrayType = arrayType.MakeArrayType();
        }

        return array.ReadValue(reader);
    }

    public override int GetSize()
    {
        int size = field.GetSize();

        for (int i = 0; i < lengths.Length; ++i)
        {
            size *= lengths[i];
        }

        return size;
    }
}

public class NullField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return 0;
    }

    public override int GetSize()
    {
        return 0;
    }
}
