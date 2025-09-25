namespace ValveBSP;

/// <summary>
/// A full rendition of the Valve BSP map standard with each lump as a named field.
/// Intended to work for map versions 19 and 20 primarily. (wider support to be added later)
/// </summary>
/// <param name="name">The name the map goes by (filename without extension)</param>
/// <param name="header">The header that contains extra map info like revision and size of lumps</param>
/// <param name="lumps">The raw data of lumps stored as objects, requires correct casting and is used to load the named lump fields with data.</param>
public partial class VBSP(string name, Header header, object[] lumps) : BSP
{
    public readonly string Name = name;
    public readonly Header Header = header;

    public readonly string                  EntityLump = (string)lumps[LumpTypes.ENTITIES];
    public readonly Plane[]                 PlaneLump = ConvertLump<Plane>(lumps[LumpTypes.PLANES]);
    public readonly TexData[]               TexDataLump = ConvertLump<TexData>(lumps[LumpTypes.TEXDATA]);
    public readonly Vertex[]                VertexLump = ConvertLump<Vertex>(lumps[LumpTypes.VERTEXES]);
    public readonly Vis                     VisLump = (Vis)lumps[LumpTypes.VISIBILITY];
    public readonly Node[]                  NodeLump = ConvertLump<Node>(lumps[LumpTypes.NODES]);
    public readonly TexInfo[]               TexInfoLump = ConvertLump<TexInfo>(lumps[LumpTypes.TEXINFO]);
    public readonly Face[]                  FaceLump = ConvertLump<Face>(lumps[LumpTypes.FACES]);
    public readonly Lighting[]              LightingLump = ConvertLump<Lighting>(lumps[LumpTypes.LIGHTING]);
    public readonly Occluder                OccluderLump = (Occluder)lumps[LumpTypes.OCCLUSION];
    public readonly Leaf[]                  LeafLump = ConvertLump<Leaf>(lumps[LumpTypes.LEAFS]);
    public readonly ushort[]                FaceIdLump = ConvertLump<ushort>(lumps[LumpTypes.FACEIDS]);
    public readonly Edge[]                  EdgeLump = ConvertLump<Edge>(lumps[LumpTypes.EDGES]);
    public readonly int[]                   SurfEdgeLump = ConvertLump<int>(lumps[LumpTypes.SURFEDGES]);
    public readonly Model[]                 ModelLump = ConvertLump<Model>(lumps[LumpTypes.MODELS]);
    public readonly ushort[]                LeafFaceLump = ConvertLump<ushort>(lumps[LumpTypes.LEAFFACES]);
    public readonly ushort[]                LeafBrushLump = ConvertLump<ushort>(lumps[LumpTypes.LEAFBRUSHES]);
    public readonly Brush[]                 BrushLump = ConvertLump<Brush>(lumps[LumpTypes.BRUSHES]);
    public readonly BrushSide[]             BrushSideLump = ConvertLump<BrushSide>(lumps[LumpTypes.BRUSHSIDES]);
    public readonly Area[]                  AreaLump = ConvertLump<Area>(lumps[LumpTypes.AREAS]);
    public readonly AreaPortal[]            AreaPortalLump = ConvertLump<AreaPortal>(lumps[LumpTypes.AREAPORTALS]);
    public readonly object                  Unused0 = 0;
    public readonly object                  Unused1 = 0;
    public readonly object                  Unused2 = 0;
    public readonly object                  Unused3 = 0;
    public readonly DispInfo[]              DispInfoLump = ConvertLump<DispInfo>(lumps[LumpTypes.DISPINFO]);
    public readonly Face[]                  OriginalFaceLump = ConvertLump<Face>(lumps[LumpTypes.ORIGINALFACES]);
    public readonly ushort[]                PhysDispLump = ConvertLump<ushort>(lumps[LumpTypes.PHYSDISP]);
    public readonly PhysCollideData[]       PhysCollideLump = ConvertLump<PhysCollideData>(lumps[LumpTypes.PHYSCOLLIDE]);
    public readonly Vertex[]                VertNormalsLump = ConvertLump<Vertex>(lumps[LumpTypes.VERTNORMALS]);
    public readonly ushort[]                VertNormalIndiciesLump = ConvertLump<ushort>(lumps[LumpTypes.VERTNORMALINDICES]);
    public readonly byte[]                  LightMapAlphasLump = (byte[])lumps[LumpTypes.DISP_LIGHTMAP_ALPHAS];
    public readonly DispVert[]              DispVertLump = ConvertLump<DispVert>(lumps[LumpTypes.DISP_VERTS]);
    public readonly Vertex[]                DispLightmapSamplePositionsLump = ConvertLump<Vertex>(lumps[LumpTypes.DISP_LIGHTMAP_SAMPLE_POSITIONS]);
    public readonly GameLumpHeader          GameLump = (GameLumpHeader)lumps[LumpTypes.GAME_LUMP];
    public readonly LeafWaterData[]         LeafWaterDataLump = ConvertLump<LeafWaterData>(lumps[LumpTypes.LEAFWATERDATA]);
    public readonly Primitive[]             PrimitiveLump = ConvertLump<Primitive>(lumps[LumpTypes.PRIMITIVES]);
    public readonly PrimVert[]              PrimVertLump = ConvertLump<PrimVert>(lumps[LumpTypes.PRIMVERTS]);
    public readonly ushort[]                PrimIndiciesLump = ConvertLump<ushort>(lumps[LumpTypes.PRIMINDICES]);
    public readonly byte[]                  PakFileLump = (byte[])lumps[LumpTypes.PAKFILE];
    public readonly Vertex[]                ClipPortalVertsLump = ConvertLump<Vertex>(lumps[LumpTypes.CLIPPORTALVERTS]);
    public readonly CubeMapSample[]         CubeMapsLump = ConvertLump<CubeMapSample>(lumps[LumpTypes.CUBEMAPS]);
    public readonly string[]                TexDataStringDataLump = ConvertLump<string>(lumps[LumpTypes.TEXDATA_STRING_DATA]);
    public readonly int[]                   TexDataStringTableLump = ConvertLump<int>(lumps[LumpTypes.TEXDATA_STRING_TABLE]);
    public readonly Overlay[]               OverlayLump = ConvertLump<Overlay>(lumps[LumpTypes.OVERLAYS]);
    public readonly float[]                 LeafMinDistToWaterLump = ConvertLump<float>(lumps[LumpTypes.LEAFMINDISTTOWATER]);
    public readonly ushort[]                FaceMacroTextureInfo = ConvertLump<ushort>(lumps[LumpTypes.FACE_MACRO_TEXTURE_INFO]);
    public readonly ushort[]                DispTriLump = ConvertLump<ushort>(lumps[LumpTypes.DISP_TRIS]);
    public readonly object                  PhysCollideSurfaceLump = 0; // deprecated
    public readonly WaterOverlay[]          WaterOverlaysLump = ConvertLump<WaterOverlay>(lumps[LumpTypes.WATEROVERLAYS]);
    public readonly LeafAmbientIndex[]      LeafAmbientIndexHDRLump = ConvertLump<LeafAmbientIndex>(lumps[LumpTypes.LEAF_AMBIENT_INDEX_HDR]);
    public readonly LeafAmbientIndex[]      LeafAmbientIndexLump = ConvertLump<LeafAmbientIndex>(lumps[LumpTypes.LEAF_AMBIENT_INDEX]);
    public readonly Lighting[]              LightingHDRLump = ConvertLump<Lighting>(lumps[LumpTypes.LIGHTING_HDR]);
    public readonly WorldLight[]            WorldLightsHDRLump = ConvertLump<WorldLight>(lumps[LumpTypes.WORLDLIGHTS_HDR]);
    public readonly LeafAmbientLighting[]   LeafAmbientLightingHDRLump = ConvertLump<LeafAmbientLighting>(lumps[LumpTypes.LEAF_AMBIENT_LIGHTING_HDR]);
    public readonly LeafAmbientLighting[]   LeafAmbientLightingLump = ConvertLump<LeafAmbientLighting>(lumps[LumpTypes.LEAF_AMBIENT_LIGHTING]);
    public readonly byte[]                  XZipPakFileLump = (byte[])lumps[LumpTypes.XZIPPAKFILE];
    public readonly Face[]                  FaceHDRLump = ConvertLump<Face>(lumps[LumpTypes.FACES_HDR]);
    public readonly byte[]                  MapFlagsLump = (byte[])lumps[LumpTypes.MAP_FLAGS];
    public readonly OverlayFade[]           OverlayFadesLump = ConvertLump<OverlayFade>(lumps[LumpTypes.OVERLAY_FADES]);

    private readonly object[] _originalLumps = lumps;

    //public readonly VBSP_Texture[]          CombinedTexData = [];                               // Custom Non-Lump

    //public readonly VBSP_StaticPropLump StaticPropLump = new();                                 // GameLump sprp

    public override string Info(ByteUnits unitType = ByteUnits.Bytes)
    {
        uint size = 0;

        for (int i = 0; i < Header.Lumps.Length; ++i)
        {
            size += Header.Lumps[i].FileLength;
        }

        string unitStr = "";
        double unitDivisor = 1;

        switch (unitType)
        {
            case ByteUnits.Bytes:
                unitStr = "bytes";
                unitDivisor = (double)ByteUnits.Bytes;
                break;
            case ByteUnits.Kilobytes:
                unitStr = "kilobytes";
                unitDivisor = (double)ByteUnits.Kilobytes;
                break;
            case ByteUnits.Megabytes:
                unitStr = "megabytes";
                unitDivisor = (double)ByteUnits.Megabytes;
                break;
            case ByteUnits.Gigabytes:
                unitStr = "gigabytes";
                unitDivisor = (double)ByteUnits.Gigabytes;
                break;
        }

        double unitSize = size / unitDivisor;
        string str = $"{Name}\nSize : {unitSize} {unitStr}\n{Header}\n\n";

        //str = $"{str}Lump\tName{new string(' ', maxNameLen)}Objects{new string(' ', 8)}Memory ({unitStr})\n\n";

        int[] lumpIds = new int[Header.HEADER_LUMPS];
        string[] lumpNames = new string[Header.HEADER_LUMPS];
        string[] numObjects = new string[Header.HEADER_LUMPS];
        double[] memSizes = new double[Header.HEADER_LUMPS];

        for (int i = 0; i < Header.HEADER_LUMPS; ++i)
        {

            lumpIds[i] = i;

            lumpNames[i] = LumpTypes.GetTypeName(i);

            memSizes[i] = Header.Lumps[i].FileLength / unitDivisor;

            if (_originalLumps[i] is not Array array)
            {
                numObjects[i] = "[variable]";
            }
            else
            {
                numObjects[i] = $"{array.Length}";

            }
        }

        PrettyTable table = new();

        table.AddColumn("Lump", lumpIds);
        table.AddColumn("Name", lumpNames);
        table.AddColumn("Objects", numObjects);
        table.AddColumn($"Memory ({unitStr})", memSizes);

        str = $"{str}{table}";

        return str;
    }

    public static T[] ConvertLump<T>(object lump)
    {
        return Array.ConvertAll((object[])lump, item => (T)item);
    }
}
