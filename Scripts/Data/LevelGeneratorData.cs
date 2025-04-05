using Godot;
using System;
using System.Collections.Generic;

public class LevelGeneratorData
{
    public int ID { get; init; } = 0;
    // Coffins
    public int NormalCoffinCount { get; init; } = 0;
    public int ZombieCoffinCount { get; init; } = 0;
    public int MassCoffinCount { get; init; } = 0;
    public int CopyNewFamilyCoffinCount { get; init; } = 0; // Each one here copies the family name from another coffing here
    public int CopyOldFamilyCoffinCount { get; init; } = 0; // Each one here copies the family name from an old coffin
    public List<PersonData> HardcodedCoffins { get; init; } = new List<PersonData>();
    public PersonData MassCoffinData { get; init; } = new PersonData();
    // Graves
    public Vector2I SingleGravesGridSize { get; init; } = Vector2I.One;
    public List<Vector2I> SingleGravesBlacklist { get; init; } = new List<Vector2I>();
    public Dictionary<GraveType, CoffinType> GraveCompatibleTypes { get; init; } = new Dictionary<GraveType, CoffinType>()
    {
        { GraveType.Single, CoffinType.All },
        { GraveType.Underground, CoffinType.All },
        { GraveType.Mass, CoffinType.All },
    };
    public Dictionary<GraveType, int> GraveScoreMultipliers { get; init; } = new Dictionary<GraveType, int>()
    {
        { GraveType.Single, 1 },
        { GraveType.Underground, 1 },
        { GraveType.Mass, 1 },
    };
    // Events
    public string AssistantType { get; init; } = "1";
    public List<string> Events { get; init; } = new List<string>();

    public LevelGeneratorData(int copyNewFamilyCoffinCount, int copyOldFamilyCoffinCount, List<PersonData> hardcodedCoffins, Vector2I singleGravesGridSize, List<Vector2I> singleGravesBlacklist, Dictionary<GraveType, CoffinType> graveCompatibleTypes, List<string> events, string assistantType, int id, int normalCoffinCount, int zombieCoffinCount, int massCoffinCount, PersonData massCoffinData, Dictionary<GraveType, int> graveScoreMultipliers)
    {
        CopyNewFamilyCoffinCount = copyNewFamilyCoffinCount;
        CopyOldFamilyCoffinCount = copyOldFamilyCoffinCount;
        HardcodedCoffins = hardcodedCoffins;
        SingleGravesGridSize = singleGravesGridSize;
        SingleGravesBlacklist = singleGravesBlacklist;
        GraveCompatibleTypes = graveCompatibleTypes;
        Events = events;
        AssistantType = assistantType;
        ID = id;
        NormalCoffinCount = normalCoffinCount;
        ZombieCoffinCount = zombieCoffinCount;
        MassCoffinCount = massCoffinCount;
        MassCoffinData = massCoffinData;
        GraveScoreMultipliers = graveScoreMultipliers;
    }
}
