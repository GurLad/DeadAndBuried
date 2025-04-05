using Godot;
using System;

public class PersonData
{
    public string Name { get; init; } = "Error";
    public string FamilyName { get; set; } = "Errorson";
    public string Inscription { get; init; } = "I AM ERROR.";
    public bool IsZombie { get; init; } = false;

    public PersonData(string name, string familyName, string inscription, bool isZombie)
    {
        Name = name;
        FamilyName = familyName;
        Inscription = inscription;
        IsZombie = isZombie;
    }

    public PersonData() {}
}
