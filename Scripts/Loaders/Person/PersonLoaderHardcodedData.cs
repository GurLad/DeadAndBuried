using Godot;
using System;
using System.Collections.Generic;

public partial class PersonLoader : AGameLoader<PersonLoader, PersonData>
{
    protected enum Pronoun { She = 1, He = 2, They = 4, EndMarker = 8 };

    protected static readonly List<(string Name, Pronoun Pronoun)> PossibleNames = new List<(string Name, Pronoun Pronoun)>()
    {
        ("Alice", Pronoun.She),
        ("Bob", Pronoun.He),
    };

    protected static readonly List<string> PossibleFamilyNames = new List<string>()
    {
        "Doe",
        "Smith",
    };
    
    protected static readonly List<string> PossibleNormalInscriptions = new List<string>()
    {
        "Loved [their] dog more than anything.",
        "KFC tortures birds.",
    };
    
    protected static readonly List<string> PossibleZombieInscriptions = new List<string>()
    {
        "Best meal I've ever had.",
        "7.8/10 too much water.",
    };
}
