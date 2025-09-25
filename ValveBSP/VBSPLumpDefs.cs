namespace ValveBSP;

public record struct Plane(Vector Normal, float Dist, int Type);

public record struct TexData(Vector Reflectivity, int NameStringTableId, int Width, int Height, int ViewWidth, int ViewHeight);

public record struct Vertex(Vector Point)
{
    public static implicit operator Vector(Vertex vertex) => vertex.Point;
    public static implicit operator Vertex(Vector vector) => new(vector);
}

public record struct Vis(int NumClusters, int[][] ByteOffsets)
{
    public const int PVS = 0;
    public const int PAS = 1;
}

public record struct Node(int PlaneNum, int[] Children, short[] Mins, short[] Maxs, ushort FirstFace, ushort NumFaces, short Area, short Padding)
{
    public const int NUM_CHILDREN = 2;
    public const int NUM_MIN_MAXS = 3;
}

public record struct TexInfo(float[][] TextureVecs, float[][] LightmapVecs, int Flags, int TexData)
{
    public const int VEC_NUM = 2;
    public const int VEC_LENGTH = 4;
}

public record struct Face(ushort PlaneNum, byte PlaneSide, byte OnNode, int FirstEdge, short NumEdges,
    short TexInfo, short DispInfo, short SurfaceFogVolumeId, byte[] Styles, int LightOffset, float Area,
    int[] LightmapTextureMinsInLuxels, int[] LightmapTextureSizeInLuxels, int OrigFace, ushort NumPrims,
    ushort FirstPrimId, uint SmoothingGroups)
{
    public const int LIGHTMAP_TEXTS_NUM = 2;
    public const int MAX_LIGHTMAPS = 4;
}

public record struct Lighting(ColorRGBExp32 LightTint)
{
    public static implicit operator ColorRGBExp32(Lighting lighting) => lighting.LightTint;
    public static implicit operator Lighting(ColorRGBExp32 tint) => new(tint);
}

public record struct Occluder(OccluderData[] Data, OccluderPolyData[] PolyData, int[] VertexIndices);

public record struct OccluderData(int Flags, int FirstPoly, int PolyCount, Vector Mins, Vector Maxs, int Area);

public record struct OccluderPolyData(int FirstVertexIndex, int VertexCount, int PlaneNum);

public record struct Leaf(int Contents, short Cluster, short AreaFlags, short[] Mins, short[] Maxs, ushort FirstLeafFace, ushort NumLeafFaces, ushort FirstLeafBrush,
                          ushort NumLeafBrushes, short LeafWaterDataId, byte[] AmbientLighting, short Padding)
{
    public const int NUM_MINS_MAXS = 3;
    public const int AMBIENT_LIGHTING_SIZE = 24;
}

//public record struct FaceId(short HammerFaceId)
//{
//    public static implicit operator short(FaceId id) => id.HammerFaceId;
//    public static implicit operator FaceId(short num) => new(num);
//}

public record struct Edge(ushort V1, ushort V2);

//public record struct SurfEdge(int Edge)
//{
//    public static implicit operator int(SurfEdge edge) => edge.Edge;
//    public static implicit operator SurfEdge(int num) => new(num);
//}

public record struct Model(Vector Mins, Vector Maxs, Vector Origin, int HeadNode, int FirstFace, int NumFaces);

public record struct WorldLight(Vector Origin, Vector Intensity, Vector Normal, int Cluster, EmitType Type, int Style,
                                float StopDot, float StopDot2, float Exponent, float Radius, float ConstantAttn,
                                float LinearAttn, float QuadraticAttn, int Flags, int TexInfo, int Owner);

//public record struct LeafFace(ushort Face)
//{
//    public static implicit operator ushort(LeafFace leafFace) => leafFace.Face;
//    public static implicit operator LeafFace(ushort num) => new(num);
//}

//public record struct LeafBrush(ushort Brush)
//{
//    public static implicit operator ushort(LeafBrush leafFace) => leafFace.Brush;
//    public static implicit operator LeafBrush(ushort num) => new(num);
//}

public record struct Brush(int FirstSide, int NumSides, int Contents);

public record struct BrushSide(ushort PlaneNum, short TexInfo, short DispInfo, short Bevel);

public record struct Area(int NumAreaPortals, int FirstAreaPortal);

public record struct AreaPortal(ushort PortalKey, ushort OtherArea, ushort FirstClipPortalVert, ushort ClipPortalVerts, int PlaneNum);

public record struct DispInfo(Vector StartPosition, int DispVertStart, int DispTriStart, int Power, int MinTess, float SmoothingAngle, int Contents,
                       ushort MapFace, int LightmapAlphaStart, int LightmapSamplePositionStart, DispNeighbor EdgeNeighbors,
                       DispCornerNeighbors CornerNeighbors, uint[] AllowedVerts)
{
    public static readonly int ALLOWEDVERTS_SIZE = VBSPConstants.PadNumber(VBSPConstants.MAX_DISPVERTS, 32) / 32;

    public readonly int NumVerts()
    {
        return VBSPConstants.NumDispPowerVerts(Power);
    }

    public readonly int NumTris()
    {
        return VBSPConstants.NumDispPowerTris(Power);
    }
}

public record struct PhysDisp(ushort NumDisplacements)
{
    public static implicit operator ushort(PhysDisp leafFace) => leafFace.NumDisplacements;
    public static implicit operator PhysDisp(ushort num) => new(num);
}

public record struct PhysModel(int ModelIndex, int DataSize, int KeyDataSize, int SolidCount);

public record struct PhysCollideData(byte[][] Data, byte[] TextData);

public record struct DispVert(Vector Vec, float Dist, float Alpha);

public record struct GameLump(int Id, ushort Flags, short Version, int FileOffset, int FileLength);

public record struct LeafWaterData(float Surface2, float MinZ, short SurfaceTexInfoId);

public record struct Primitive(byte Type, ushort FirstIndex, ushort IndexCount, ushort FirstVert, ushort VertCount);

public record struct PrimVert(Vector Pos)
{
    public static implicit operator Vector(PrimVert primVert) => primVert.Pos;
    public static implicit operator PrimVert(Vector vector) => new(vector);
}

public record struct CubeMapSample(int[] Orgin, int Size)
{
    public const int ORIGIN_COUNT = 3;
}

public record struct Overlay(int Id, short TexInfo, ushort FaceCountAndRenderOrder, int[] OverlayFaces, float[] U, float[] V, Vector[] UVPoints,
                                    Vector Origin, Vector BasisNormal)
{
    public const int OVERLAY_RENDER_ORDER_NUM_BITS = 2;
    public const int OVERLAY_NUM_RENDER_ORDERS = (1 << OVERLAY_RENDER_ORDER_NUM_BITS);
    public const int OVERLAY_RENDER_ORDER_MASK = 0xC000;

    public const int OVERLAY_BSP_FACE_COUNT = 64;
    public const int UV_VEC_COUNT = 2;
    public const int UV_POINTS_COUNT = 4;
}

public class FaceMacroTextureInfo(ushort macroTextureNameID)
{
    public ushort MacroTextureNameID { get; set; } = macroTextureNameID;
}

public record struct DispTri(ushort Tags)
{
    public static implicit operator ushort(DispTri dispTri) => dispTri.Tags;
    public static implicit operator DispTri(ushort num) => new(num);
}

public record struct WaterOverlay(int Id, short TexInfo, ushort FaceCountAndRenderOrder, int[] OverlayFaces, float[] U, float[] V, Vector[] UVPoints,
                                  Vector Origin, Vector BasisNormal)
{
    public const int WATEROVERLAY_RENDER_ORDER_NUM_BITS = 2;
    public const int WATEROVERLAY_NUM_RENDER_ORDERS = (1 << WATEROVERLAY_RENDER_ORDER_NUM_BITS);
    public const int WATEROVERLAY_RENDER_ORDER_MASK = 0xC000;

    public const int WATEROVERLAY_BSP_FACE_COUNT = 256;
    public const int UV_VEC_COUNT = 2;
    public const int UV_POINTS_COUNT = 4;
}

public record struct LeafAmbientIndex(ushort AmbientSampleCount, ushort FirstAmbeintSample);

public record struct LeafAmbientLighting(CompressedLightCube Cube, byte X, byte Y, byte Z, byte Pad);

public record struct OverlayFade(float FadeDistMinSq, float FadeDistMaxSq);

public struct VBSP_StaticPropDictLump
{
    public const int NAME_ENTRY_LENGTH = 128;

    public int dict_entries;
    public char[][] name;
}

public struct VBSP_StaticPropLeafLump
{
    public int leaf_entries;
    public ushort[] leaf;
}

// need to find version of bsp :3
public struct VBSP_StaticPropLump
{
    public Vector origin;
    public Angle angle;

    public ushort prop_type;
    public ushort first_leaf;
    public ushort leaf_count;
    public byte solid;
    //public byte flags;

    public int skin;
    public float min_fade_dist;
    public float max_fade_dist;
    public Vector lighting_origin;

    public float forced_fade_scale;

    public ushort min_dx_level;
    public ushort max_dx_level;

    public uint flags;
    public ushort lightmap_res_x;
    public ushort lightmap_res_y;

    public byte min_cpu_level;
    public byte max_cpu_level;
    public byte min_gpu_level;
    public byte max_gpu_level;

    public Color32 diffuse_modulation;

    public bool disable_x360;

    public uint flags_ex;

    public float uniform_scale;
}

public struct VBSP_Texture
{
    public int id;
    public string orig_name;
    public string mapped_name;
    public Vector reflectivity;
    public int width, height;
    public int view_width, view_height;
}
