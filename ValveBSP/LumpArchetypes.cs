namespace ValveBSP;

public sealed class LumpArchetypes
{
    public readonly LumpItemArchetype[] Archetypes;

    private static LumpArchetypes? _instance = null;

    private LumpArchetypes()
    {
        Archetypes = new LumpItemArchetype[Header.HEADER_LUMPS];

        MultiArrayField vecArrayField = new(typeof(float), GFloatField, TexInfo.VEC_NUM, TexInfo.VEC_LENGTH);
        ArrayField lightMapField = new(typeof(int), GIntField, Face.LIGHTMAP_TEXTS_NUM);
        ArrayField minsMaxsField = new(typeof(short), GShortField, Leaf.NUM_MINS_MAXS);

        DataDescription desc = new();


        // Entity Lump
        // Uses a unique reader, doesn't need a real archetype
        Archetypes[LumpTypes.ENTITIES] = new();

        // Planes Lump
        Archetypes[LumpTypes.PLANES] = desc.Begin(typeof(Plane))
                                           .Add(GVectorField)
                                           .Add(GFloatField)
                                           .Add(GIntField)
                                           .End();

        // TexData Lump
        Archetypes[LumpTypes.TEXDATA] = desc.Begin(typeof(TexData))
                                            .Add(GVectorField)
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .End();

        // Vertex Lump
        Archetypes[LumpTypes.VERTEXES] = new(typeof(Vertex), GVectorField);

        // Vis Lump
        Archetypes[LumpTypes.VISIBILITY] = new(typeof(Vis));

        // Node Lump
        Archetypes[LumpTypes.NODES] = desc.Begin(typeof(Node))
                                          .Add(GIntField)
                                          .Add(new ArrayField(typeof(int), GIntField, Node.NUM_CHILDREN))
                                          .Add(new ArrayField(typeof(short), GShortField, Node.NUM_MIN_MAXS))
                                          .Add(new ArrayField(typeof(short), GShortField, Node.NUM_MIN_MAXS))
                                          .Add(GUShortField)
                                          .Add(GUShortField)
                                          .Add(GShortField)
                                          .Add(GShortField)
                                          .End();

        // TexInfo Lump
        Archetypes[LumpTypes.TEXINFO] = desc.Begin(typeof(TexInfo))
                                            .Add(vecArrayField)
                                            .Add(vecArrayField)
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .End();

        // Face Lump
        Archetypes[LumpTypes.FACES] = desc.Begin(typeof(Face))
                                          .Add(GUShortField)
                                          .Add(GByteField)
                                          .Add(GByteField)
                                          .Add(GIntField)
                                          .Add(GShortField)
                                          .Add(GShortField)
                                          .Add(GShortField)
                                          .Add(GShortField)
                                          .Add(new ArrayField(typeof(byte), GByteField, Face.MAX_LIGHTMAPS))
                                          .Add(GIntField)
                                          .Add(GFloatField)
                                          .Add(lightMapField)
                                          .Add(lightMapField)
                                          .Add(GIntField)
                                          .Add(GUShortField)
                                          .Add(GUShortField)
                                          .Add(GUIntField)
                                          .End();

        // Lighting Lump
        Archetypes[LumpTypes.LIGHTING] = desc.Begin(typeof(Lighting))
                                             .Add(GColorRGBExp32Field)
                                             .End();

        // Occlusion Lump
        // This lump is super complicated and uses custom reading function
        // It does not appear to need a serious archetype since its production is not a standard lump
        Archetypes[LumpTypes.OCCLUSION] = new();

        // Leaf Lump
        Archetypes[LumpTypes.LEAFS] = desc.Begin(typeof(Leaf))
                                          .Add(GIntField)
                                          .Add(GShortField)
                                          .Add(GShortField)
                                          .Add(minsMaxsField)
                                          .Add(minsMaxsField)
                                          .Add(GUShortField)
                                          .Add(GUShortField)
                                          .Add(GUShortField)
                                          .Add(GUShortField)
                                          .Add(GShortField)
                                          .Add(new ArrayField(typeof(byte), GByteField, Leaf.AMBIENT_LIGHTING_SIZE))
                                          .Add(GShortField)
                                          .End();

        // FaceId Lump
        Archetypes[LumpTypes.FACEIDS] = new(typeof(ushort), GUShortField);
            
                                        //desc.Begin(typeof(FaceId))
                                        //    .Add(GShortField)
                                        //    .End();

        // Edge Lump
        Archetypes[LumpTypes.EDGES] = desc.Begin(typeof(Edge))
                                          .Add(GUShortField)
                                          .Add(GUShortField)
                                          .End();

        // SurfEdge Lump
        Archetypes[LumpTypes.SURFEDGES] = new(typeof(int), GIntField);
            
                                      //desc.Begin(typeof(SurfEdge))
                                      //    .Add(GIntField)
                                      //    .End();

        // Model Lump
        Archetypes[LumpTypes.MODELS] = desc.Begin(typeof(Model))
                                           .Add(GVectorField)
                                           .Add(GVectorField)
                                           .Add(GVectorField)
                                           .Add(GIntField)
                                           .Add(GIntField)
                                           .Add(GIntField)
                                           .End();

        // WorldLights Lump
        Archetypes[LumpTypes.WORLDLIGHTS] = desc.Begin(typeof(WorldLight))
                                                .Add(GVectorField)
                                                .Add(GVectorField)
                                                .Add(GVectorField)
                                                .Add(GIntField)
                                                .Add(GEmitTypeField)
                                                .Add(GIntField)
                                                .Add(GFloatField)
                                                .Add(GFloatField)
                                                .Add(GFloatField)
                                                .Add(GFloatField)
                                                .Add(GFloatField)
                                                .Add(GFloatField)
                                                .Add(GFloatField)
                                                .Add(GIntField)
                                                .Add(GIntField)
                                                .Add(GIntField)
                                                .End();

        // LeafFace Lump
        Archetypes[LumpTypes.LEAFFACES] = new(typeof(ushort), GUShortField);
            
                                           //desc.Begin(typeof(LeafFace))
                                           //    .Add(GUShortField)
                                           //    .End();

        // LeafBrush Lump
        Archetypes[LumpTypes.LEAFBRUSHES] = new(typeof(ushort), GUShortField);
                                            
                                            //desc.Begin(typeof(LeafBrush))
                                            //    .Add(GUShortField)
                                            //    .End();

        // Brush Lump
        Archetypes[LumpTypes.BRUSHES] = desc.Begin(typeof(Brush))
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .Add(GIntField)
                                            .End();

        // BrushSide Lump
        Archetypes[LumpTypes.BRUSHSIDES] = desc.Begin(typeof(BrushSide))
                                               .Add(GUShortField)
                                               .Add(GShortField)
                                               .Add(GShortField)
                                               .Add(GShortField)
                                               .End();

        // Area Lump
        Archetypes[LumpTypes.AREAS] = desc.Begin(typeof(Area))
                                          .Add(GIntField)
                                          .Add(GIntField)
                                          .End();

        // AreaPortal Lump
        Archetypes[LumpTypes.AREAPORTALS] = desc.Begin(typeof(AreaPortal))
                                                .Add(GUShortField)
                                                .Add(GUShortField)
                                                .Add(GUShortField)
                                                .Add(GUShortField)
                                                .Add(GIntField)
                                                .End();

        // DispInfo Lump
        Archetypes[LumpTypes.DISPINFO] = desc.Begin(typeof(DispInfo))
                                             .Add(GVectorField)
                                             .Add(GIntField)
                                             .Add(GIntField)
                                             .Add(GIntField)
                                             .Add(GIntField)
                                             .Add(GFloatField)
                                             .Add(GIntField)
                                             .Add(GUShortField)
                                             .Add(GIntField)
                                             .Add(GIntField)
                                             .Add(GDispNeighborField)
                                             .Add(GDispCornerNeighborsField)
                                             .Add(new ArrayField(typeof(int), GIntField, DispInfo.ALLOWEDVERTS_SIZE))
                                             .End();

        // DispInfo Lump
        Archetypes[LumpTypes.ORIGINALFACES] = Archetypes[LumpTypes.FACES];

        // PhysDisp
        Archetypes[LumpTypes.PHYSDISP] = new(typeof(ushort), GUShortField);

        // PhysCollide
        // Super complex type, has a custom reader, does not produce a standard lump
        Archetypes[LumpTypes.PHYSCOLLIDE] = new();

        // VertNormal Lump
        // Making a huge assumption about how this lump works, most likely vectors
        Archetypes[LumpTypes.VERTNORMALS] = Archetypes[LumpTypes.VERTEXES];

        // VertNormalIndicies Lump
        Archetypes[LumpTypes.VERTNORMALINDICES] = new(typeof(ushort), GUShortField);

        // DispLightMapAlphas Lump
        // No clue, can't get a straight answer from the Source 2013 SDK
        // Assuming it's bytes because alpha, if it's not, then it won't cause an error
        Archetypes[LumpTypes.DISP_LIGHTMAP_ALPHAS] = new(typeof(byte), GByteField);

        // DispVerts Lump
        Archetypes[LumpTypes.DISP_VERTS] = desc.Begin(typeof(DispVert))
                                               .Add(GVectorField)
                                               .Add(GFloatField)
                                               .Add(GFloatField)
                                               .End();

        // LightMapSamplePositions
        Archetypes[LumpTypes.DISP_LIGHTMAP_SAMPLE_POSITIONS] = Archetypes[LumpTypes.VERTEXES];

        // Game Lump
        // Uses a custom reader
        Archetypes[LumpTypes.GAME_LUMP] = new();

        // LeafWaterData Lump
        Archetypes[LumpTypes.LEAFWATERDATA] = desc.Begin(typeof(LeafWaterData))
                                                  .Add(GFloatField)
                                                  .Add(GFloatField)
                                                  .Add(GShortField)
                                                  .End();

        // Primitives Lump
        Archetypes[LumpTypes.PRIMITIVES] = desc.Begin(typeof(Primitive))
                                               .Add(GByteField)
                                               .Add(GUShortField)
                                               .Add(GUShortField)
                                               .Add(GUShortField)
                                               .Add(GUShortField)
                                               .End();

        // PrimVerts Lump
        Archetypes[LumpTypes.PRIMVERTS] = new(typeof(PrimVert), GVectorField);

        // PrimIndicies Lump
        // No type is directly described, making an educated guess that the indicies are ushorts
        Archetypes[LumpTypes.PRIMINDICES] = new(typeof(ushort), GUShortField);

        // PakFile Lump
        // No idea how to meaningfully read a PakFile for actual usage, just gonna store a byte array and maybe deal with it
        Archetypes[LumpTypes.PAKFILE] = new(typeof(byte), GByteField);

        // ClipPortalVerts
        // Mysterious lump that I haven't researched, best guess, it stores vertexes
        Archetypes[LumpTypes.CLIPPORTALVERTS] = Archetypes[LumpTypes.VERTEXES];

        // CubeMaps
        Archetypes[LumpTypes.CUBEMAPS] = desc.Begin(typeof(CubeMapSample))
                                             .Add(new ArrayField(typeof(int), GIntField, CubeMapSample.ORIGIN_COUNT))
                                             .Add(GIntField)
                                             .End();

        // TexData Lump
        Archetypes[LumpTypes.TEXDATA_STRING_DATA] = new();

        // TexDataStringTable
        Archetypes[LumpTypes.TEXDATA_STRING_TABLE] = new(typeof(int), GIntField);

        // Overlays Lump
        Archetypes[LumpTypes.OVERLAYS] = desc.Begin(typeof(Overlay))
                                             .Add(GIntField)
                                             .Add(GShortField)
                                             .Add(GUShortField)
                                             .Add(new ArrayField(typeof(int), GIntField, Overlay.OVERLAY_BSP_FACE_COUNT))
                                             .Add(new ArrayField(typeof(float), GIntField, Overlay.UV_VEC_COUNT))
                                             .Add(new ArrayField(typeof(float), GIntField, Overlay.UV_VEC_COUNT))
                                             .Add(new ArrayField(typeof(Vector), GVectorField, Overlay.UV_POINTS_COUNT))
                                             .Add(GVectorField)
                                             .Add(GVectorField)
                                             .End();

        // LeafMinDistToWater Lump
        // I got no idea what this is, it's not well defined immediately, likely a float since distance being measured by a float would make sense
        Archetypes[LumpTypes.LEAFMINDISTTOWATER] = new(typeof(float), GFloatField);

        // FaceMacroTextureInfo Lump
        Archetypes[LumpTypes.FACE_MACRO_TEXTURE_INFO] = new(typeof(ushort), GUShortField);

        // DispTris Lump
        Archetypes[LumpTypes.DISP_TRIS] = new(typeof(ushort), GUShortField);

        // PhysCollideSurface Lump
        // This lump is deprecated, and thus will use the UnusedReader rather than a real LumpReader
        Archetypes[LumpTypes.PHYSCOLLIDESURFACE] = new();

        // WaterOverlays Lump
        Archetypes[LumpTypes.WATEROVERLAYS] = desc.Begin(typeof(WaterOverlay))
                                                  .Add(GIntField)
                                                  .Add(GShortField)
                                                  .Add(GUShortField)
                                                  .Add(new ArrayField(typeof(int), GIntField, WaterOverlay.WATEROVERLAY_BSP_FACE_COUNT))
                                                  .Add(new ArrayField(typeof(float), GIntField, WaterOverlay.UV_VEC_COUNT))
                                                  .Add(new ArrayField(typeof(float), GIntField, WaterOverlay.UV_VEC_COUNT))
                                                  .Add(new ArrayField(typeof(Vector), GVectorField, WaterOverlay.UV_POINTS_COUNT))
                                                  .Add(GVectorField)
                                                  .Add(GVectorField)
                                                  .End();

        // LeafAmbientIndexHDR Lump
        Archetypes[LumpTypes.LEAF_AMBIENT_INDEX_HDR] = desc.Begin(typeof(LeafAmbientIndex))
                                                           .Add(GUShortField)
                                                           .Add(GUShortField)
                                                           .End();

        // LeafAmbientIndex Lump
        Archetypes[LumpTypes.LEAF_AMBIENT_INDEX] = Archetypes[LumpTypes.LEAF_AMBIENT_INDEX_HDR];

        // LightingHDR Lump
        // Don't currently know how they store this lighting information
        // Will have to scrape vrad for this most likely
        // Will use Lighting
        Archetypes[LumpTypes.LIGHTING_HDR] = Archetypes[LumpTypes.LIGHTING];

        // WorldLightingHDR Lump
        Archetypes[LumpTypes.WORLDLIGHTS_HDR] = Archetypes[LumpTypes.WORLDLIGHTS];

        // LeafAmbientLightingHDR Lump
        Archetypes[LumpTypes.LEAF_AMBIENT_LIGHTING_HDR] = desc.Begin(typeof(LeafAmbientLighting))
                                                              .Add(GCompressedLightCubeField)
                                                              .Add(GByteField)
                                                              .Add(GByteField)
                                                              .Add(GByteField)
                                                              .Add(GByteField)
                                                              .End();

        // LeafAmbientLighting Lump
        Archetypes[LumpTypes.LEAF_AMBIENT_LIGHTING] = Archetypes[LumpTypes.LEAF_AMBIENT_LIGHTING_HDR];

        // XZipPakFile Lump
        // Listed as deprecated, will probably have it use the UnusedReader
        Archetypes[LumpTypes.XZIPPAKFILE] = new(typeof(byte), GByteField);

        // FacesHDR Lump
        Archetypes[LumpTypes.FACES_HDR] = Archetypes[LumpTypes.FACES];

        // MapFlags Lump
        // Can't find a data structure for this
        Archetypes[LumpTypes.MAP_FLAGS] = new(typeof(byte), GByteField);

        // OverlayFades Lump
        Archetypes[LumpTypes.OVERLAY_FADES] = desc.Begin(typeof(OverlayFade))
                                                  .Add(GUShortField)
                                                  .Add(GUShortField)
                                                  .End();
    }

    public static LumpArchetypes Instance
    {
        get
        {
            _instance ??= new();
            return _instance;
        }
    }
}
