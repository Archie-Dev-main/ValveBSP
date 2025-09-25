namespace ValveBSP;

public class VBSPReader(LumpItemArchetype[] lumpArchetypes, LumpReader standardReader, Dictionary<int, LumpReader> exceptions) : BSPReader<VBSP>(lumpArchetypes, standardReader, exceptions)
{
    public override VBSP Read(string name, Stream stream)
    {
        using BinaryReader reader = new(stream);

        Header header = ReadHeader(reader);

        object[] finishedLumps = ReadLumps(header, reader);

        return new(name, header, finishedLumps);
    }

    public static Header ReadHeader(BinaryReader reader)
    {
        Header header = new(reader.ReadInt32(), reader.ReadInt32(), new Lump[Header.HEADER_LUMPS], 0);

        for (int i = 0; i < Header.HEADER_LUMPS; ++i)
        {
            header.Lumps[i] = ReadHeaderLump(reader);
        }

        header.MapRev = reader.ReadInt32();

        return header;
    }

    private static Lump ReadHeaderLump(BinaryReader reader)
    {
        return new(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadInt32(), reader.ReadInt32());
    }

    private object[] ReadLumps(Header header, BinaryReader reader)
    {
        object[] finishedLumps = new object[Header.HEADER_LUMPS];

        for (int i = 0; i < Header.HEADER_LUMPS; ++i)
        {
            Lump lump = header.Lumps[i];
            reader.BaseStream.Seek(lump.FileOffset, SeekOrigin.Begin);

            if (_exceptions.TryGetValue(i, out LumpReader? exception))
            {
                finishedLumps[i] = exception.ReadLump(reader, lump, _lumpArchetypes[i]);
            }
            else
            {
                finishedLumps[i] = _standardReader.ReadLump(reader, lump, _lumpArchetypes[i]);
            }
        }

        return finishedLumps;
    }
}
