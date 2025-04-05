using Godot;
using System;

[Flags]
public enum CoffinType
{
    None = 0,
    Single = 1,
    Multi = 2,
    Zombie = 4,
    All = Single | Multi | Zombie,
    EndMarker = All + 1,
}

public class CoffinData
{
    public PersonData PersonData { get; } = new PersonData();

    private CoffinType _types = CoffinType.Single;
    public CoffinType Types => _types | (PersonData.IsZombie ? CoffinType.Zombie : CoffinType.None);
    public int IconID = -1;

    public CoffinData(PersonData personData, CoffinType types)
    {
        PersonData = personData;
        _types = types;
        IconID = CoffinIconLoader.GetRandom(a => a.Type == Types).IconID;
    }

    public CoffinData()
    {
        IconID = CoffinIconLoader.GetRandom(a => a.Type == Types).IconID;
    }
}
