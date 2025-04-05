using Godot;
using System;
using System.Collections.Generic;

public partial class LevelGeneratorLoader : AGameLoader<LevelGeneratorLoader, LevelGeneratorData>
{
    private static readonly Vector2I SingleGravesGridSize = new Vector2I(10, 4);
    private static readonly List<Vector2I> SingleGravesBlacklist = new List<Vector2I>();
    private static Dictionary<GraveType, int> GraveScoreMultipliers { get; } = new Dictionary<GraveType, int>()
    {
        { GraveType.Single, 1000 },
        { GraveType.Underground, 500 },
        { GraveType.Mass, 10 },
        { GraveType.Burn, 20 },
        { GraveType.Spaceship, 50 },
    };

    protected static List<LevelGeneratorData> Levels { get; } = new List<LevelGeneratorData>()
    {
        new LevelGeneratorData
        (
            id: 0,
            normalCoffinCount: 4,
            zombieCoffinCount: 0,
            massCoffinCount: 0,
            copyNewFamilyCoffinCount: 1,
            copyOldFamilyCoffinCount: 0,
            hardcodedCoffins: new List<PersonData>(),
            massCoffinData: new PersonData(),
            singleGravesGridSize: SingleGravesGridSize,
            singleGravesBlacklist: SingleGravesBlacklist,
            graveScoreMultipliers: GraveScoreMultipliers,
            graveCompatibleTypes: new Dictionary<GraveType, CoffinType>()
            {
                { GraveType.Single, CoffinType.Single }
            },
            assistantType: "1",
            events: new List<string>()
            {
                "Temp Greet: Test",
                "Temp Point: Another test!"
            }
        ),
        new LevelGeneratorData
        (
            id: 1,
            normalCoffinCount: 4,
            zombieCoffinCount: 0,
            massCoffinCount: 0,
            copyNewFamilyCoffinCount: 0,
            copyOldFamilyCoffinCount: 2,
            hardcodedCoffins: new List<PersonData>()
            {
                new PersonData("Dr. Andy", "Dead", "Death claimed him before he could conquer it.", true)
            },
            massCoffinData: new PersonData(),
            singleGravesGridSize: SingleGravesGridSize,
            singleGravesBlacklist: SingleGravesBlacklist,
            graveScoreMultipliers: GraveScoreMultipliers,
            graveCompatibleTypes: new Dictionary<GraveType, CoffinType>()
            {
                { GraveType.Single, CoffinType.Single }
            },
            assistantType: "1",
            events: new List<string>()
            {
                "Wave: Test"
            }
        ),
    };

    protected override List<LevelGeneratorData> GetAllInternal(Func<LevelGeneratorData, bool> predicate)
    {
        return Levels.FindAll(a => predicate(a));
    }
}
