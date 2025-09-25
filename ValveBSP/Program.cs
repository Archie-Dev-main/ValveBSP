// See https://aka.ms/new-console-template for more information
using ValveBSP;

string path = "C:\\Users\\archi\\source\\repos\\ValveBSP\\ValveBSP\\bsps\\officehours_floor1_a1.bsp";
byte[] fileBytes = File.ReadAllBytes(path);

MemoryStream stream = new(fileBytes);

Dictionary<int, LumpReader> exceptions = [];

exceptions[LumpTypes.ENTITIES] = new EntityReader();
exceptions[LumpTypes.VISIBILITY] = new VisReader();
exceptions[LumpTypes.OCCLUSION] = new OccluderReader();
exceptions[LumpTypes.UNUSED0] = new UnusedReader();
exceptions[LumpTypes.UNUSED1] = new UnusedReader();
exceptions[LumpTypes.UNUSED2] = new UnusedReader();
exceptions[LumpTypes.UNUSED3] = new UnusedReader();
exceptions[LumpTypes.PHYSCOLLIDE] = new PhysCollideReader();
exceptions[LumpTypes.DISP_LIGHTMAP_ALPHAS] = new ByteReader();
exceptions[LumpTypes.GAME_LUMP] = new GameLumpReader();
exceptions[LumpTypes.PAKFILE] = new ByteReader();
exceptions[LumpTypes.TEXDATA_STRING_DATA] = new TexDataStringDataReader();
exceptions[LumpTypes.PHYSCOLLIDESURFACE] = new UnusedReader();
exceptions[LumpTypes.XZIPPAKFILE] = new ByteReader();
exceptions[LumpTypes.MAP_FLAGS] = new ByteReader();
exceptions[61] = new UnusedReader();
exceptions[62] = new UnusedReader();
exceptions[63] = new UnusedReader();

VBSPReader vReader = new(LumpArchetypes.Instance.Archetypes, new StandardReader(), exceptions);

VBSP vbsp = vReader.Read(Path.GetFileNameWithoutExtension(path), stream);

Console.WriteLine(vbsp.Info());