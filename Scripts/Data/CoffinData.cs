using Godot;
using System;

public enum CoffinType
{
    None = 0,
    Single = 1,
    Multi = 2,
    Zombie = 64,
    All = Single | Multi | Zombie
}

public class CoffinData
{
    public PersonData PersonData { get; } = new PersonData();

    private CoffinType _types = CoffinType.Single;
    public CoffinType Types => _types | (PersonData.IsZombie ? CoffinType.Zombie : CoffinType.None);
}
