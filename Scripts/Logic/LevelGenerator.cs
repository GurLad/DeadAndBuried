using Godot;
using System;
using System.Collections.Generic;

public partial class LevelGenerator : Node
{
    public List<AGrave> GenerateGraves(LevelGeneratorData level, SaveData saveData)
    {
        List<AGrave> result = new List<AGrave>();
        if (level.GraveCompatibleTypes.ContainsKey(GraveType.Single))
        {
            Dictionary<Vector2I, SingleGrave> singleGraves = new Dictionary<Vector2I, SingleGrave>();
            for (int x = 0; x < level.SingleGravesGridSize.X; x++)
            {
                for (int y = 0; y < level.SingleGravesGridSize.Y; y++)
                {
                    SingleGrave newGrave = new SingleGrave();
                    Vector2I index = new Vector2I(x, y);
                    newGrave.Data.PersonData = saveData.Graves.SafeGet(index.Serializable())?.PersonData;
                    newGrave.Data.IconID = saveData.Graves.SafeGet(index.Serializable())?.IconID ?? GraveIconLoader.GetRandom(a => a.Type == GraveType.Single).IconID;
                    newGrave.Data.CompatibleTypes = level.GraveCompatibleTypes[GraveType.Single];
                    index.GetNeighbors().ForEach(a =>
                    {
                        SingleGrave neighbor = singleGraves.SafeGet(a);
                        if (neighbor != null)
                        {
                            neighbor.AdjacentGraves.Add(newGrave);
                            newGrave.AdjacentGraves.Add(neighbor);
                        }
                    });
                    singleGraves.Add(index, newGrave);
                    if (level.GraveCompatibleTypes.ContainsKey(GraveType.Underground))
                    {
                        UndergroundGrave undergroundGrave = new UndergroundGrave();
                        undergroundGrave.Data.IconID = GraveIconLoader.GetRandom(a => a.Type == GraveType.Underground).IconID;
                        undergroundGrave.Data.CompatibleTypes = level.GraveCompatibleTypes[GraveType.Underground];
                        undergroundGrave.OvergroundGrave = newGrave;
                        result.Add(undergroundGrave);
                    }
                }
            }
            result.AddRange(singleGraves.Values);
        }
        // TBA: Multi
        return result;
    }

    public List<Coffin> GenerateCoffins(LevelGeneratorData level)
    {
        List<PersonData> people = GeneratePeople(level);
        List<CoffinData> coffinDatas = people.ConvertAll(a => new CoffinData(a, a.IsZombie ? CoffinType.Single : CoffinType.Zombie));
        for (int i = 0; i < level.MassCoffinCount; i++)
        {
            coffinDatas.Add(new CoffinData(level.MassCoffinData, CoffinType.Multi | CoffinType.Zombie));
        }
        return coffinDatas.ConvertAll(a => new Coffin(a));
    }

    private List<PersonData> GeneratePeople(LevelGeneratorData level)
    {
        List<PersonData> people = new List<PersonData>();
        List<PersonData> processedPeople = new List<PersonData>();
        for (int i = 0; i < level.NormalCoffinCount; i++)
        {
            people.Add(PersonLoader.GetRandom(a => !a.IsZombie));
        }
        for (int i = 0; i < level.ZombieCoffinCount; i++)
        {
            people.Add(PersonLoader.GetRandom(a => a.IsZombie));
        }
        for (int i = 0; i < level.CopyNewFamilyCoffinCount; i++)
        {
            if (people.Count <= 0)
            {
                GD.PushError("[Level]: Not enough people!");
                GetTree().Quit();
            }
            PersonData selected = people.RandomItemInList();
            people.Remove(selected);
            processedPeople.Add(selected);
            selected.FamilyName = people.RandomItemInList().FamilyName;
        }
        for (int i = 0; i < level.CopyOldFamilyCoffinCount; i++)
        {
            if (people.Count <= 0)
            {
                GD.PushError("[Level]: Not enough people!");
                GetTree().Quit();
            }
            PersonData selected = people.RandomItemInList();
            people.Remove(selected);
            processedPeople.Add(selected);
            // TODO
            GD.PushError("[Level]: Not implemented yet! CopyOldFamilyCoffinCount");
        }
        people.AddRange(processedPeople);
        people.AddRange(level.HardcodedCoffins);
        return people;
    }
}
