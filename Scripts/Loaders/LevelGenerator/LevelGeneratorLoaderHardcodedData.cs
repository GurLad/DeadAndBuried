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
                "BPoint: My name is " + "John Jones!".RichTextColor(RED_COLOR).RichTextWave() + " I'll help you run this place!",
                "BSmile: All you have to do is bury the dead. Simply drag their coffins from the " + "left side of the screen...".RichTextWave().RichTextColor(RED_COLOR),
                "BWave: To " + "anywhere you want".RichTextWave().RichTextColor(RED_COLOR) + " in the middle! This is your cemetery, after all!",
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
                new PersonData("Dr.", "Dead", "Death claimed him before he could conquer it.", true)
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
                "BWave: Welcome back! I'm " + "sooooooo".RichTextWave().RichTextColor(RED_COLOR) + " glad you're back!",
                "BOops: My previous job fired me after one day, so getting to come here a second time is a " + "huge relief!".RichTextWave().RichTextColor(RED_COLOR),
                "BPoint: Anyway, we got another " + "shipment of corpses".RichTextWave() + " to bury during the day!",
                "BSmile: You know the drill by now - are you ready to " + "place some tombs?!".RichTextShake().RichTextColor(RED_COLOR),
            }
        ),
        new LevelGeneratorData
        (
            id: 2,
            normalCoffinCount: 5,
            zombieCoffinCount: 2,
            massCoffinCount: 0,
            copyNewFamilyCoffinCount: 1,
            copyOldFamilyCoffinCount: 1,
            hardcodedCoffins: new List<PersonData>(),
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
                "BWave: Hello again! " + "Missed me?".RichTextWave() + " I missed you!".RichTextColor(RED_COLOR),
                "BPoint: Guess what?\nThat's right! " + "We get to bury more people this night!".RichTextWave().RichTextColor(RED_COLOR),
                "BPoint: ...",
                "BThink: Say... Is it just me, or are we " + "missing a grave here?".RichTextShake().RichTextColor(RED_COLOR),
                "BOops: Uh... " + "I-It's probably nothing!".RichTextShake().RichTextColor(RED_COLOR) + " I mean, everyone misplaces a corpse once in a while, " + "right?".RichTextWave().RichTextColor(RED_COLOR),
                "BWave: Well, there's no point thinking about it when we've got " + "graves to dig!".RichTextWave(),
            }
        ),
        new LevelGeneratorData
        (
            id: 3,
            normalCoffinCount: 3,
            zombieCoffinCount: 4,
            massCoffinCount: 0,
            copyNewFamilyCoffinCount: 0,
            copyOldFamilyCoffinCount: 1,
            hardcodedCoffins: new List<PersonData>(),
            massCoffinData: new PersonData(),
            singleGravesGridSize: SingleGravesGridSize,
            singleGravesBlacklist: SingleGravesBlacklist,
            graveScoreMultipliers: GraveScoreMultipliers,
            graveCompatibleTypes: new Dictionary<GraveType, CoffinType>()
            {
                { GraveType.Single, CoffinType.Single },
                { GraveType.Underground, CoffinType.Single | CoffinType.Zombie },
            },
            assistantType: "1",
            events: new List<string>()
            {
                "HOops: " + "H-Hey there!".RichTextWave() + " Sorry I'm late, I had a bit of an... " + "accident".RichTextShake().RichTextColor(GREEN_COLOR) + " preparing our corpses.",
                "HThink: And... It looks like " + "some more graves".RichTextWave().RichTextColor(RED_COLOR) + " got dug out somehow...",
                "HOops: W-Well, maybe we should just try " + "digging them deeper?".RichTextWave().RichTextColor(RED_COLOR),
                "HSmile: You can move underground by pressing the " + " green arrow!".RichTextWave().RichTextColor(GREEN_COLOR),
                "HPoint: Anyway, " + "I think I'm gonna go now.".RichTextShake() + " Happy undertaking!",
            }
        ),
        new LevelGeneratorData
        (
            id: 4,
            normalCoffinCount: 2,
            zombieCoffinCount: 2,
            massCoffinCount: 5,
            copyNewFamilyCoffinCount: 0,
            copyOldFamilyCoffinCount: 0,
            hardcodedCoffins: new List<PersonData>()
            {
                new PersonData("John", "Jones", "Its been a pleasure working with you!", true)
            },
            massCoffinData: new PersonData("Bunch of", "Zombies", "Sorry, were running out of coffins here!", true),
            singleGravesGridSize: SingleGravesGridSize,
            singleGravesBlacklist: SingleGravesBlacklist,
            graveScoreMultipliers: GraveScoreMultipliers,
            graveCompatibleTypes: new Dictionary<GraveType, CoffinType>()
            {
                { GraveType.Single, CoffinType.Single | CoffinType.Zombie },
                { GraveType.Mass, CoffinType.Single | CoffinType.Zombie | CoffinType.Multi },
            },
            assistantType: "1",
            events: new List<string>()
            {
                "ZWave: " + "Hellooooooooo".RichTextWave().RichTextColor(GREEN_COLOR) + " again! Are you doing fantastic? I'm doing " + "fantastic!".RichTextWave().RichTextColor(GREEN_COLOR),
                "ZPoint: They opened a new " + "brains".RichTextShake() + " place near where I live, and it's " + "amaaaaaaazing!".RichTextWave().RichTextColor(GREEN_COLOR),
                "ZThink: Oh, but... It looks like " + "burying them deeper".RichTextWave() + " didn't work".RichTextColor(RED_COLOR) + ", huh?",
                "ZOops: I think those holes were just too shallow...",
                "ZPoint: So I dug a " + "biiiiiiiiiiiig".RichTextWave().RichTextColor(GREEN_COLOR) + " one in the back!",
                "ZSmile: That should go " + "great!".RichTextColor(GREEN_COLOR).RichTextWave(),
            }
        ),
        new LevelGeneratorData
        (
            id: 5,
            normalCoffinCount: 0,
            zombieCoffinCount: 0,
            massCoffinCount: 10,
            copyNewFamilyCoffinCount: 0,
            copyOldFamilyCoffinCount: 0,
            hardcodedCoffins: new List<PersonData>()
            {
                new PersonData("John", "Jones", "Goodbye, for real this time!", true)
            },
            massCoffinData: new PersonData("Too Many", "Zombies", "These coffins are bigger on the inside, trust me.", true),
            singleGravesGridSize: SingleGravesGridSize,
            singleGravesBlacklist: SingleGravesBlacklist,
            graveScoreMultipliers: GraveScoreMultipliers,
            graveCompatibleTypes: new Dictionary<GraveType, CoffinType>()
            {
                { GraveType.Single, CoffinType.Single | CoffinType.Zombie },
                { GraveType.Spaceship, CoffinType.Single | CoffinType.Zombie | CoffinType.Multi },
            },
            assistantType: "1",
            events: new List<string>()
            {
                "ZSmile: Heeeeeey... I've got " + "good news".RichTextWave().RichTextColor(GREEN_COLOR) + " and " + "bad news.".RichTextWave().RichTextColor(RED_COLOR),
                "ZOops: Bad news - " + "everyone climbed out of that hole.".RichTextWave().RichTextColor(RED_COLOR),
                "ZPoint: Good news - " + "I climbed out of that hole!".RichTextWave().RichTextColor(GREEN_COLOR),
                "ZSmile: But don't worry, I think I've got it now. I had a " + "grrrrrrrrreat".RichTextWave().RichTextColor(GREEN_COLOR) + " idea!",
                "ZWave: We'll send all the zombies to " + "Mars! ".RichTextColor(RED_COLOR) + "There's " + "nooooooo way".RichTextWave().RichTextColor(RED_COLOR) + " they'll ever get back from there!",
                "ZThink: You can't go deeper than the " + "DEPTHS".RichTextColor(RED_COLOR).RichTextWave() + " of space, after all.",
                "ZPoint: The rocket is ready - just " + "load the corpses!".RichTextWave().RichTextColor(GREEN_COLOR),
            }
        ),
    };

    protected override List<LevelGeneratorData> GetAllInternal(Func<LevelGeneratorData, bool> predicate)
    {
        return Levels.FindAll(a => predicate(a));
    }
}
