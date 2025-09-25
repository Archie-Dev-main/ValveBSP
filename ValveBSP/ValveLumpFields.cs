global using static ValveBSP.ValveFields;

namespace ValveBSP;

public class VectorField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return new Vector(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
    }

    public override int GetSize()
    {
        return sizeof(float) * 3;
    }
}

public class AngleField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return (Angle)GVectorField.ReadValue(reader);
    }

    public override int GetSize()
    {
        return GVectorField.GetSize();
    }
}

public class Color32Field : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return new Color32(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
    }

    public override int GetSize()
    {
        return sizeof(byte) * 4;
    }
}

public class ColorRGBExp32Field : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return new ColorRGBExp32(reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadSByte());
    }

    public override int GetSize()
    {
        return sizeof(byte) * 3 + sizeof(sbyte);
    }
}

public class EmitTypeField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        return (EmitType)reader.ReadInt32();
    }

    public override int GetSize()
    {
        return sizeof(int);
    }
}

public class DispNeighborField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        DispNeighbor neighbor = new()
        {
            SubNeighbors = [ReadSubneighbor(reader), ReadSubneighbor(reader)]
        };

        return neighbor;
    }

    private static DispSubNeighbor ReadSubneighbor(BinaryReader reader)
    {
        return new()
        {
            Neighbor = reader.ReadUInt16(),
            NeighborOrientation = (NeighborOrientation)reader.ReadByte(),
            Span = (NeighborSpan)reader.ReadByte(),
            NeighborSpan = (NeighborSpan)reader.ReadByte()
        };
    }

    public override int GetSize()
    {
        return (sizeof(ushort) + sizeof(byte) + sizeof(sbyte) + sizeof(byte)) * 2;
    }
}

public class DispCornerNeighborsField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        DispCornerNeighbors cornerNeighbors = new();

        for (int i = 0; i < VBSPConstants.MAX_DISP_CORNER_NEIGHBORS; ++i)
        {
            cornerNeighbors.Neighbors[i] = reader.ReadInt16();
        }

        cornerNeighbors.NumNeighbors = reader.ReadByte();

        return cornerNeighbors;
    }

    public override int GetSize()
    {
        return sizeof(ushort) * VBSPConstants.MAX_DISP_CORNER_NEIGHBORS + sizeof(byte);
    }
}

public class CompressedLightCubeField : LumpItemField
{
    public override object ReadValue(BinaryReader reader)
    {
        CompressedLightCube compressedLightCube = new(new ColorRGBExp32[CompressedLightCube.COLOR_COUNT]);

        for (int i = 0; i < CompressedLightCube.COLOR_COUNT; ++i)
        {
            compressedLightCube.Color[i] = (ColorRGBExp32)GColorRGBExp32Field.ReadValue(reader);
        }

        return compressedLightCube;
    }

    public override int GetSize()
    {
        return GColorRGBExp32Field.GetSize() * CompressedLightCube.COLOR_COUNT;
    }
}

public static class ValveFields
{
    public static readonly VectorField GVectorField = new();
    public static readonly AngleField GAngleField = new();
    public static readonly Color32Field GColor32Field = new();
    public static readonly ColorRGBExp32Field GColorRGBExp32Field = new();
    public static readonly EmitTypeField GEmitTypeField = new();
    public static readonly DispNeighborField GDispNeighborField = new();
    public static readonly DispCornerNeighborsField GDispCornerNeighborsField = new();
    public static readonly CompressedLightCubeField GCompressedLightCubeField = new();
}
