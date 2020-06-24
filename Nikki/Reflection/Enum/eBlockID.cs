﻿namespace Nikki.Reflection.Enum
{
    /// <summary>
    /// Enum of IDs related to all global blocks.
    /// </summary>
	internal enum eBlockID : uint
	{
        Padding            = 0x00000000, // varies
        FEngFont           = 0x00030201, // 0x10 Modular
        FEngFiles          = 0x00030203, // 0x10 Modular
        FNGCompress        = 0x00030210, // 0x10 Modular
        PresetRides        = 0x00030220, // 0x10 Modular
        MagazinesFrontend  = 0x00030230, // 0x10 Modular
        MagazinesShowcase  = 0x00030231, // 0x10 Modular
        WideDecals         = 0x00030240, // 0x10 Modular
        PresetSkins        = 0x00030250, // 0x08 Actual
        WorldBounds        = 0x00034027, // varies
        Tracks             = 0x00034201, // 0x10 Modular
        SunInfos           = 0x00034202, // varies
        CarTypeInfos       = 0x00034600, // 0x10 Modular
        CarSkins           = 0x00034601, // 0x10 Modular
        DBCarParts_Header  = 0x00034603, // varies
        DBCarParts_Array   = 0x00034604, // varies
        DBCarParts_Attribs = 0x00034605, // varies
        DBCarParts_Strings = 0x00034606, // varies
        SlotTypes          = 0x00034607, // varies
        CarInfoAnimHookup  = 0x00034608, // 0x10 Modular
        CarInfoAnimHideup  = 0x00034609, // varies
        DBCarParts_Structs = 0x0003460A, // varies
        DBCarParts_Models  = 0x0003460B, // varies
        DBCarParts_Offsets = 0x0003460C, // varies
        StyleMomentsInfo   = 0x00034A07, // 0x80 Modular
        GCareer_Races      = 0x00034A11, // varies
        GCareer_Shops      = 0x00034A12, // varies
        GCareer_Brands     = 0x00034A14, // varies
        GCareer_PartPerf   = 0x00034A15, // varies
        GCareer_Showcases  = 0x00034A16, // varies
        GCareer_Messages   = 0x00034A17, // varies
        GCareer_Stages     = 0x00034A18, // varies
        GCareer_Sponsors   = 0x00034A19, // varies
        GCareer_PerfTun    = 0x00034A1A, // varies
        GCareer_Challenges = 0x00034A1B, // varies
        GCareer_PartUnlock = 0x00034A1C, // varies
        GCareer_Strings    = 0x00034A1D, // varies
        GCareer_BankTrigs  = 0x00034A1E, // varies
        GCareer_CarUnlocks = 0x00034A1F, // varies
        DifficultyInfo     = 0x00034B00, // 0x80 Modular
        AcidEffects        = 0x00035020, // 0x80 Modular
        AcidEmmiters       = 0x00035021, // 0x80 Modular
        Stream37220        = 0x00037220, // varies
        Stream37240        = 0x00037240, // varies
        Stream37250        = 0x00037250, // varies
        Stream37260        = 0x00037260, // varies
        Stream37270        = 0x00037270, // varies
        STRBlocks          = 0x00039000, // 0x10 Modular
        LangFont           = 0x00039001, // 0x10 Modular
        Subtitles          = 0x00039010, // 0x10 Modular
        MovieCatalog       = 0x00039020, // 0x80 Modular
        ICECatalog         = 0x0003B200, // 0x80 Modular
        WWorld             = 0x0003B800, // varies
        WCollisionPack     = 0x0003B801, // varies
        WCollisionRaww     = 0x0003B802, // 0x800 Modular
        Collision          = 0x0003B901, // 0x10 Modular
        EmitterLibrary     = 0x0003BC00, // varies
        TPKSettings        = 0x0003BD00, // 0x80 Modular
        Vinyl_Header       = 0x0003CE01, // 0x08 Actual
        Vinyl_PointerTable = 0x0003CE02, // 0x0C Actual
        Vinyl_PathEntry    = 0x0003CE04, // varies
        Vinyl_PathData     = 0x0003CE05, // varies
        Vinyl_PathPoint    = 0x0003CE06, // varies
        Vinyl_FillEffect   = 0x0003CE07, // varies
        Vinyl_StrokeEffect = 0x0003CE08, // varies
        Vinyl_VectorLine   = 0x0003CE09, // varies
        Vinyl_InnerGlow    = 0x0003CE0A, // varies
        Vinyl_ShadowEffect = 0x0003CE0B, // varies
        SkinRegionDB       = 0x0003CE12, // 0x10 Modular
        VinylMetaData      = 0x0003CE13, // 0x10 Modular
        Materials          = 0x00135200, // 0x10 Modular
        EAGLSkeleton       = 0x00E34009, // 0x80 Modular
        EAGLAnimations     = 0x00E34010, // 0x10 Modular
        DDSTexture         = 0x30300200, // 0x80 Modular
        PCAWater0          = 0x30300300, // 0x80 Modular
        ColorCube          = 0x30300201, // 0x10 Modular
        TPK_InfoPart1      = 0x33310001, // 0x08 Actual
        TPK_InfoPart2      = 0x33310002, // 0x0C Actual
        TPK_InfoPart3      = 0x33310003, // varies
        TPK_InfoPart4      = 0x33310004, // varies
        TPK_InfoPart5      = 0x33310005, // varies
        TPK_AnimPart1      = 0x33312001, // varies
        TPK_AnimPart2      = 0x33312002, // varies
        TPK_DataPart1      = 0x33320001, // 0x08 Actual
        TPK_DataPart2      = 0x33320002, // 0x80 Modular
        TPK_DataPart3      = 0x33320003, // ????
        Nikki              = 0x42704D67, // varies
        LZCompressed       = 0x55441122, // varies
        SpeedScenery       = 0x80034100, // varies
        DBCarParts         = 0x80034602, // varies
        GCareer            = 0x80034A10, // 0x80 Modular
        EmitterTriggers    = 0x80036000, // varies
        AnimDirectory      = 0x80037050, // 0x10 Modular
        QuickSpline        = 0x8003B000, // 0x80 Modular
        IceCameraPart0     = 0x8003B200, // 0x10 Modular
        IceCameraPart1     = 0x8003B201, // 0x10 Modular
        IceCameraPart2     = 0x8003B202, // 0x10 Modular
        IceCameraPart3     = 0x8003B203, // 0x10 Modular
        IceCameraPart4     = 0x8003B204, // 0x10 Modular
        IceSettings        = 0x8003B209, // 0x10 Modular
        SoundStichs        = 0x8003B500, // 0x10 Modular
        EventSequence      = 0x8003B810, // varies
        DBCarBounds        = 0x8003B900, // 0x08 Actual
        VinylSystem        = 0x8003CE00, // 0x800 Modular
        Vinyl_PathSet      = 0x8003CE03, // varies
        LimitsTable        = 0x8003CE0D, // 0x10 Modular
        Geometry           = 0x80134000, // varies
        ELights            = 0x80135000, // varies
        SpecialEffects     = 0xB0300100, // 0x80 Modular
        PCAWeights         = 0xB0300300, // 0x80 Modular
        TPKBlocks          = 0xB3300000, // 0x80 Modular
        TPK_InfoBlock      = 0xB3310000, // 0x40 Modular
        TPK_BinData        = 0xB3312000, // varies
        TPK_AnimBlock      = 0xB3312004, // varies
        TPK_DataBlock      = 0xB3320000, // 0x80 Modular
    }
}
