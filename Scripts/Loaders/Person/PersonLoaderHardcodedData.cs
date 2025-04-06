using Godot;
using System;
using System.Collections.Generic;

public partial class PersonLoader : AGameLoader<PersonLoader, PersonData>
{
    protected enum Pronoun { She = 1, He = 2, They = 4, EndMarker = 8, Any = She | He | They };

    protected static readonly List<(string Name, Pronoun Pronoun)> PossibleNames = new List<(string Name, Pronoun Pronoun)>()
    {
        ("Alice", Pronoun.She),
        ("Alex", Pronoun.Any),
        ("Bob", Pronoun.He),
        ("Bobby", Pronoun.Any),
        ("Bill", Pronoun.He),
        ("Beatrice", Pronoun.She),
        ("Clara", Pronoun.She),
        ("Clair", Pronoun.She),
        ("Chris", Pronoun.He),
        ("Doug", Pronoun.He),
        ("Dr.", Pronoun.Any),
        ("Ellen", Pronoun.Any),
        ("Eleanor", Pronoun.She),
        ("Ed", Pronoun.He),
        ("Finn", Pronoun.He),
        ("Fiona", Pronoun.She),
        ("Gavin", Pronoun.He),
        ("Grace", Pronoun.She),
        ("Gloria", Pronoun.She),
        ("Heather", Pronoun.She),
        ("Helena", Pronoun.She),
        ("Hugh", Pronoun.He),
        ("Ivan", Pronoun.He),
        ("Ian", Pronoun.He),
        ("Iris", Pronoun.She),
        ("Ike", Pronoun.He),
        ("Jack", Pronoun.He),
        ("John", Pronoun.He),
        ("Jane", Pronoun.She),
        ("Ken", Pronoun.He),
        ("Laura", Pronoun.She),
        ("Lena", Pronoun.She),
        ("Liam", Pronoun.He),
        ("Lily", Pronoun.She),
        ("Mark", Pronoun.He),
        ("Molly", Pronoun.She),
        ("Marie", Pronoun.She),
        ("Michael", Pronoun.He),
        ("Norman", Pronoun.He),
        ("Nessa", Pronoun.She),
        ("Olivia", Pronoun.She),
        ("Patrick", Pronoun.He),
        ("Peggy", Pronoun.She),
        ("Ross", Pronoun.He),
        ("Rian", Pronoun.He),
        ("Selena", Pronoun.She),
        ("Sam", Pronoun.Any),
        ("Terry", Pronoun.He),
        ("Vivian", Pronoun.She),
        ("Will", Pronoun.He),
        ("Zack", Pronoun.He),
        ("Zoe", Pronoun.She),
    };

    protected static readonly List<string> PossibleFamilyNames = new List<string>()
    {
        "Doe",
        "Smith",
        "Jones",
        "Johnson",
        "Fields",
        "Brown",
        "Williams",
        "Phillips",
    };
    
    protected static readonly List<string> PossibleNormalInscriptions = new List<string>()
    {
        "Loved [their] dog more than me.",
        "KFC tortures birds.",
        "Spent 72 making a game without drinking.",
        "Always wanted to get hit by a truck.",
        "Free at last from writing their novel.",
        "Rest in peace.",
        "[Their] cheese always made everyone smile.",
        "When I die, please write this on my tombstone.",
        "Im not getting paid enough to write the entire Bee movie script.",
        "Couldnt handle the weight.",
        "Insert something heart-warming.",
        "Never paid for [their] tombstone.",
        "Wanted to be buried in Antarctica.",
        "Refused to brush their teeth.",
        "My only friend.",
    };
    
    protected static readonly List<string> PossibleZombieInscriptions = new List<string>()
    {
        "Best meal I've ever had.",
        "7.8/10 too much water.",
        "Too stupid to be nurturing",
        "This is [their] third death this week",
        "Walked off a cliff.",
        "Could only say one word.",
        "I loved [their] brain.",
        "Drank too much coffee.",
        "Can count to three.",
        "[Their] leg is still missing.",
        "Very loud and obnoxious.",
        "Prefers hearts to brains.",
        "Used to be a lawyer.",
    };
}
