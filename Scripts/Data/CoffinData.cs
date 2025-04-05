using Godot;
using System;

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

    public CoffinData(PersonData personData, CoffinType types)
    {
        PersonData = personData;
        _types = types;
    }

    public CoffinData()
    {
    }
}
