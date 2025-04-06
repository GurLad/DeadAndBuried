using Godot;
using System;
using System.Collections.Generic;

public partial class LevelGeneratorLoader : AGameLoader<LevelGeneratorLoader, LevelGeneratorData>
{
    private static readonly Vector2I SingleGravesGridSize = new Vector2I(10, 4);
    private static readonly List<Vector2I> SingleGravesBlacklist = new List<Vector2I>()
    {
        new Vector2I(0, 0),
        new Vector2I(1, 0),
        new Vector2I(4, 0),
        new Vector2I(5, 0),
        new Vector2I(6, 2),
        new Vector2I(7, 2),
        new Vector2I(6, 3),
        new Vector2I(7, 3),
        new Vector2I(8, 3),
    };
    private static Dictionary<GraveType, int> GraveScoreMultipliers { get; } = new Dictionary<GraveType, int>()
    {
        { GraveType.Single, 1000 },
        { GraveType.Underground, 500 },
        { GraveType.Mass, 10 },
        { GraveType.Burn, 20 },
        { GraveType.Spaceship, 50 },
    };

    private static readonly string RED_COLOR = "773f35";
    private static readonly string GREEN_COLOR = "#a39e55";

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
                "BWave: " + "HIIIIIIIIIIIII!".RichTextWave() + " Welcome to your new " + "cemetery!".RichTextColor(RED_COLOR).RichTextWave(),
                "BPoint: My name is " + "TBA!".RichTextColor(RED_COLOR).RichTextWave() + " I'll help you run this place!",
                "BSmile: All you have to do is bury the dead. Simply drag their coffins from the " + "left side of the screen...".RichTextWave().RichTextColor(RED_COLOR),
                "BWave: To " + "anywhere you want".RichTextWave().RichTextColor(RED_COLOR) + " in the middle! This is your cemetary, after all!",
                "BThink: Oh, but don't forget, " + "family members usually want to be buried together.".RichTextWave().RichTextColor(RED_COLOR),
                "BPoint: That's all from me! Have an " + "amazing night!".RichTextWave(),
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
                { GraveType.Single, CoffinType.Single | CoffinType.Zombie }
            },
            assistantType: "1",
            events: new List<string>()
            {
                "BWave: Test"
            }
        ),
    };

    protected override List<LevelGeneratorData> GetAllInternal(Func<LevelGeneratorData, bool> predicate)
    {
        return Levels.FindAll(a => predicate(a));
    }
}
