global using SwizzleField = (bool Negate, ValveBSP.Vector.FieldTypes Field);
using System.Reflection;
using System.Text;

namespace ValveBSP;

public static class VBSPConstants
{
    public const int MIN_MAP_DISP_POWER = 2;
    public const int MAX_MAP_DISP_POWER = 4;

    public const int MAX_DISP_CORNER_NEIGHBORS = 4;

    public const int MAX_MAP_DISPINFO = 2048;

    public const int MAX_MAP_DISP_VERTS = MAX_MAP_DISPINFO * ((1 << MAX_MAP_DISP_POWER) + 1) * ((1 << MAX_MAP_DISP_POWER) + 1);
    public const int MAX_MAP_DISP_TRIS = (1 << MAX_MAP_DISP_POWER) * (1 << MAX_MAP_DISP_POWER) * 2;

    public static readonly int MAX_DISPVERTS = NumDispPowerVerts(MAX_MAP_DISP_POWER);
    public static readonly int MAX_DISPTRIS = NumDispPowerTris(MAX_MAP_DISP_POWER);

    public static int NumDispPowerVerts(int power)
    {
        return ((1 << (power)) + 1) * ((1 << (power)) + 1);
    }

    public static int NumDispPowerTris(int power)
    {
        return (1 << (power)) * (1 << (power)) * 2;
    }

    public static int PadNumber(int number, int boundary)
    {
        return (number + (boundary - 1)) / boundary * boundary;
    }
}

public sealed class LumpTypes
{
    public const int ENTITIES = 0;
    public const int PLANES = 1;
    public const int TEXDATA = 2;
    public const int VERTEXES = 3;
    public const int VISIBILITY = 4;
    public const int NODES = 5;
    public const int TEXINFO = 6;
    public const int FACES = 7;
    public const int LIGHTING = 8;
    public const int OCCLUSION = 9;
    public const int LEAFS = 10;
    public const int FACEIDS = 11;
    public const int EDGES = 12;
    public const int SURFEDGES = 13;
    public const int MODELS = 14;
    public const int WORLDLIGHTS = 15;
    public const int LEAFFACES = 16;
    public const int LEAFBRUSHES = 17;
    public const int BRUSHES = 18;
    public const int BRUSHSIDES = 19;
    public const int AREAS = 20;
    public const int AREAPORTALS = 21;
    public const int UNUSED0 = 22;
    public const int UNUSED1 = 23;
    public const int UNUSED2 = 24;
    public const int UNUSED3 = 25;
    public const int DISPINFO = 26;
    public const int ORIGINALFACES = 27;
    public const int PHYSDISP = 28;
    public const int PHYSCOLLIDE = 29;
    public const int VERTNORMALS = 30;
    public const int VERTNORMALINDICES = 31;
    public const int DISP_LIGHTMAP_ALPHAS = 32;
    public const int DISP_VERTS = 33;
    public const int DISP_LIGHTMAP_SAMPLE_POSITIONS = 34;
    public const int GAME_LUMP = 35;
    public const int LEAFWATERDATA = 36;
    public const int PRIMITIVES = 37;
    public const int PRIMVERTS = 38;
    public const int PRIMINDICES = 39;
    public const int PAKFILE = 40;
    public const int CLIPPORTALVERTS = 41;
    public const int CUBEMAPS = 42;
    public const int TEXDATA_STRING_DATA = 43;
    public const int TEXDATA_STRING_TABLE = 44;
    public const int OVERLAYS = 45;
    public const int LEAFMINDISTTOWATER = 46;
    public const int FACE_MACRO_TEXTURE_INFO = 47;
    public const int DISP_TRIS = 48;
    public const int PHYSCOLLIDESURFACE = 49;
    public const int WATEROVERLAYS = 50;
    public const int LEAF_AMBIENT_INDEX_HDR = 51;
    public const int LEAF_AMBIENT_INDEX = 52;
    public const int LIGHTING_HDR = 53;
    public const int WORLDLIGHTS_HDR = 54;
    public const int LEAF_AMBIENT_LIGHTING_HDR = 55;
    public const int LEAF_AMBIENT_LIGHTING = 56;
    public const int XZIPPAKFILE = 57;
    public const int FACES_HDR = 58;
    public const int MAP_FLAGS = 59;
    public const int OVERLAY_FADES = 60;

    private static readonly FieldInfo[] _fields = typeof(LumpTypes).GetFields();

    public static string GetTypeName(int lumpType)
    {
        if (lumpType > 60)
        {
            return "null";
        }

        return _fields[lumpType].Name;
    }

    public static int NumTypes()
    {
        return _fields.Length;
    }
}

public enum DispTriFlags
{
    DISPTRI_TAG_SURFACE = 0x1,
    DISPTRI_TAG_WALKABLE = 0x2,
    DISPTRI_TAG_BUILDABLE = 0x4,
    DISPTRI_FLAG_SURFPROP1 = 0x8,
    DISPTRI_FLAG_SURFPROP2 = 0x10
}

public enum NeighborSpan : byte
{
    CORNER_TO_CORNER = 0,
    CORNER_TO_MIDPOINT = 1,
    MIDPOINT_TO_CORNER = 2
}

public enum NeighborOrientation : byte
{
    ORIENTATION_CCW_0 = 0,
    ORIENTATION_CCW_90 = 1,
    ORIENTATION_CCW_180 = 2,
    ORIENTATION_CCW_270 = 3
}

public record struct Vector(float X, float Y, float Z)
{
    public enum FieldTypes
    {
        X,
        Y,
        Z
    }

    public override readonly string ToString()
    {
        return "[" + X + ", " + Y + ", " + Z + "]";
    }

    public static float GetField(Vector vector, FieldTypes field, bool negate = false)
    {
        float result = negate ? -1f : 1f;
        return result * field switch
        {
            FieldTypes.X => vector.X,
            FieldTypes.Y => vector.Y,
            FieldTypes.Z => vector.Z,
            _ => 0,
        };
    }

    public static Vector Swizzle(Vector vector, SwizzleField x, SwizzleField y, SwizzleField z)
    {
        float xf = GetField(vector, x.Field, x.Negate);
        float yf = GetField(vector, y.Field, y.Negate);
        float zf = GetField(vector, z.Field, z.Negate);

        return new(xf, yf, zf);
    }

    //public static implicit operator FlaxEngine.Vector3(VBSP_Vector v)
    //{
    //    return new(v.y, v.z, -v.x);
    //}

    //public static implicit operator FlaxEngine.Float3(VBSP_Vector v)
    //{
    //    return new(v.y, v.z, -v.x);
    //}
}

public record struct Angle(float Pitch, float Yaw, float Roll)
{
    public static implicit operator Vector(Angle angle) => new(angle.Pitch, angle.Yaw, angle.Roll);
    public static implicit operator Angle(Vector vector) => new(vector.X, vector.Y, vector.Z);
}

public record struct Color32(byte R, byte G, byte B, byte A);

public record struct ColorRGBExp32(byte R, byte G, byte B, sbyte Exponent);

public enum EmitType
{
    Surface,
    Point,
    SpotLight,
    SkyLight,
    QuakeLight,
    SkyAmbient
}

public record struct Lump(uint FileOffset, uint FileLength, int Version, int UncompressedSize)
{
    public readonly override string ToString()
    {
        return $"File Offset : {FileOffset}\nFile Length : {FileLength}\nVersion : {Version}\nUncompressed Size : {UncompressedSize}";
    }
}

public record struct Header(int Ident, int Version, Lump[] Lumps, int MapRev)
{
    public const uint HEADER_LUMPS = 64;
    public const uint HEADER_IDENT = ('P' << 24) + ('S' << 16) + ('B' << 8) + 'V';

    public readonly override string ToString()
    {
        return $"Identification : {Encoding.ASCII.GetString(BitConverter.GetBytes(Ident))}\nVersion : {Version}\nNumber of Lumps : {HEADER_LUMPS}\nMap Revision : {MapRev}";
    }
}

public struct DispSubNeighbor
{
    public ushort Neighbor;
    public NeighborOrientation NeighborOrientation;
    public NeighborSpan Span;
    public NeighborSpan NeighborSpan;

    public readonly bool IsValid()
    {
        return Neighbor != 0xFFFF;
    }

    public void SetInvalid()
    {
        Neighbor = 0xFFFF;
    }
}

public class DispNeighbor
{
    public const int NUM_SUBNEIGHBORS = 2;
    public DispSubNeighbor[] SubNeighbors = new DispSubNeighbor[NUM_SUBNEIGHBORS];

    public void SetInvalid()
    {
        SubNeighbors[0].SetInvalid();
        SubNeighbors[1].SetInvalid();
    }

    public bool IsValid()
    {
        return SubNeighbors[0].IsValid() || SubNeighbors[1].IsValid();
    }
}

public class DispCornerNeighbors
{
    public short[] Neighbors = new short[VBSPConstants.MAX_DISP_CORNER_NEIGHBORS];
    public byte NumNeighbors;

    public void SetInvalid()
    {
        NumNeighbors = 0;
    }
}

public record struct GameLumpHeader(int LumpCount, GameLump[] GameLump);

public enum PrimitiveType : byte
{
    TRILIST = 0,
    TRISTRIP = 1
}

public record struct CompressedLightCube(ColorRGBExp32[] Color)
{
    public const int COLOR_COUNT = 6;
}

//LUMP_SIZES[LumpTypes.PLANES] = 20;
//LUMP_SIZES[LumpTypes.VERTEXES] = 12;
//LUMP_SIZES[LumpTypes.FACES] = 56;
//LUMP_SIZES[LumpTypes.EDGES] = 4;
//LUMP_SIZES[LumpTypes.SURFEDGES] = sizeof(int);
//LUMP_SIZES[LumpTypes.LEAFFACES] = sizeof(ushort);
//LUMP_SIZES[LumpTypes.BRUSHES] = 12;
//LUMP_SIZES[LumpTypes.BRUSHSIDES] = 8;
//LUMP_SIZES[LumpTypes.NODES] = 32;
//LUMP_SIZES[LumpTypes.OLD_LEAFS] = 56;
//LUMP_SIZES[LumpTypes.LEAFS] = 32;
//LUMP_SIZES[LumpTypes.TEXINFO] = 72;
//LUMP_SIZES[LumpTypes.TEXDATA] = 32;
//LUMP_SIZES[LumpTypes.MODELS] = 48;
//LUMP_SIZES[LumpTypes.GAME_LUMP] = 16;
//LUMP_SIZES[LumpTypes.DISPINFO] = 176;
//LUMP_SIZES[LumpTypes.DISP_VERTS] = 20;
//LUMP_SIZES[LumpTypes.TEXDATA_STRING_TABLE] = sizeof(int);
//LUMP_SIZES[LumpTypes.TEXDATA_STRING_DATA] = 1;
