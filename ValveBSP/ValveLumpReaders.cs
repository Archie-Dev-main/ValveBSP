namespace ValveBSP;

public class EntityReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        if (lump.FileLength == 0)
            return "null";

        return System.Text.Encoding.ASCII.GetString(reader.ReadBytes((int)lump.FileLength));
    }
}

public class VisReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        if (lump.FileLength == 0)
            return new Vis();

        int numClusters = reader.ReadInt32();
        int[][] byteOffsets = new int[numClusters][];

        for (int i = 0; i < numClusters; ++i)
        {
            byteOffsets[i] = new int[2];

            byteOffsets[i][Vis.PVS] = reader.ReadInt32();
            byteOffsets[i][Vis.PAS] = reader.ReadInt32();
        }

        Vis vis = new(numClusters, byteOffsets);
        return vis;
    }
}

public class OccluderReader : LumpReader
{
    private readonly static StandardReader _standardReader = new();

    private readonly static LumpItemArchetype _occluderDataArchetype;
    private readonly static LumpItemArchetype _occluderPolyDataArchetype;

    static OccluderReader()
    {
        DataDescription desc = new();

        _occluderDataArchetype = desc.Begin(typeof(OccluderData))
                                     .Add(GIntField)
                                     .Add(GIntField)
                                     .Add(GIntField)
                                     .Add(GVectorField)
                                     .Add(GVectorField)
                                     .Add(GIntField)
                                     .End();

        _occluderPolyDataArchetype = desc.Begin(typeof(OccluderPolyData))
                                         .Add(GIntField)
                                         .Add(GIntField)
                                         .Add(GIntField)
                                         .End();
    }

    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        if (lump.FileLength == 0)
            return new Occluder();

        uint length = (uint)reader.ReadInt32();
        OccluderData[] data = VBSP.ConvertLump<OccluderData>(_standardReader.ReadLump(reader, new(lump.FileOffset, length, 0, 0), _occluderDataArchetype));

        uint skipLen = length + sizeof(int);
        length = (uint)reader.ReadInt32();
        OccluderPolyData[] polyData = VBSP.ConvertLump<OccluderPolyData>(_standardReader.ReadLump(reader, new(lump.FileOffset + skipLen, length, 0, 0), _occluderPolyDataArchetype));

        //skipLen += length + sizeof(int);
        length = (uint)reader.ReadInt32();
        int[] vertexIndices = new int[length];

        for (int i = 0; i < length; ++i)
        {
            vertexIndices[i] = reader.ReadInt32();
        }

        return new Occluder(data, polyData, vertexIndices);
    }
}

public class UnusedReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        return 0;
    }
}

public class PhysCollideReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        if (lump.FileLength == 0)
            return new PhysCollideData();

        List<PhysCollideData> collData = [];

        while (true)
        {
            PhysModel header = ReadHeader(reader);

            if (header.ModelIndex == -1)
                break;

            List<byte[]> pureData = [];

            for (int i = 0; i < header.SolidCount; ++i)
            {
                int size = reader.ReadInt32();
                pureData.Add(reader.ReadBytes(size));
            }

            collData.Add(new([.. pureData], reader.ReadBytes(header.KeyDataSize)));
        }

        object[] finalData = [.. collData];
        return finalData;
    }

    private static PhysModel ReadHeader(BinaryReader reader)
    {
        return new(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
    }
}

public class GameLumpReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        if (lump.FileLength == 0)
            return new GameLumpHeader();

        int lumpCount = reader.ReadInt32();
        GameLump[] gameLumps = new GameLump[lumpCount];

        for (int i = 0; i < lumpCount; ++i)
        {
            gameLumps[i] = ReadLump(reader);
        }

        return new GameLumpHeader(lumpCount, gameLumps);
    }

    private static GameLump ReadLump(BinaryReader reader)
    {
        return new(reader.ReadInt32(), reader.ReadUInt16(), reader.ReadInt16(), reader.ReadInt32(), reader.ReadInt32());
    }
}

public class TexDataStringDataReader : LumpReader
{
    public override object ReadLump(BinaryReader reader, Lump lump, LumpItemArchetype item)
    {
        if (lump.FileLength == 0)
            return Array.Empty<object>();

        List<string> texData = [];

        uint totalLen = lump.FileLength + lump.FileOffset;
        while (reader.BaseStream.Position < totalLen)
        {
            texData.Add(ReadString(reader, totalLen));
        }

        string[] finalData = [.. texData];
        return finalData;
    }

    private static string ReadString(BinaryReader reader, uint totalLen)
    {
        List<char> str = [];

        char c;
        while (reader.BaseStream.Position < totalLen)
        {
            c = (char)reader.ReadByte();

            if (c == '\0')
            {
                return new([.. str]);
            }

            str.Add(c);
        }

        return "null";
    }
}
