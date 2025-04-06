using Godot;
using System;
using System.Collections.Generic;

public partial class PersonLoader : AGameLoader<PersonLoader, PersonData>
{
    private PersonData ZombieChecker = new PersonData("", "", "", true);

    protected override List<PersonData> GetAllInternal(Func<PersonData, bool> predicate)
    {
        return new List<PersonData>() { GetRandomInternal(predicate) };
    }

    protected override PersonData GetRandomInternal(Func<PersonData, bool> predicate)
    {
        bool isZombie = predicate(ZombieChecker);
        (string Name, Pronoun Pronoun) namePronoun = PossibleNames.RandomItemInList();
        string formattedInscription = FormatInscription(isZombie ? PossibleZombieInscriptions.RandomItemInList() : PossibleNormalInscriptions.RandomItemInList(), namePronoun.Pronoun);
        return new PersonData(
            namePronoun.Name,
            PossibleFamilyNames.RandomItemInList(),
            formattedInscription,
            isZombie
        );
    }

    private string FormatInscription(string inscription, Pronoun pronoun)
    {
        List<Pronoun> full = new List<Pronoun>();
        for (int i = 1; i < (int)Pronoun.EndMarker; i *= 2)
        {
            if (((int)pronoun & i) != 0)
            {
                full.Add((Pronoun)i);
            }
        }
        pronoun = full.RandomItemInList();
        return pronoun switch
        {
            Pronoun.She => inscription
                .Replace("[They]", "She").Replace("[Them]", "Her").Replace("[Their]", "Her")
                .Replace("[they]", "she").Replace("[them]", "her").Replace("[their]", "her"),
            Pronoun.He => inscription
                .Replace("[They]", "He").Replace("[Them]", "Him").Replace("[Their]", "His")
                .Replace("[they]", "he").Replace("[them]", "him").Replace("[their]", "his"),
            Pronoun.They => inscription
                .Replace("[They]", "They").Replace("[Them]", "Them").Replace("[Their]", "Their")
                .Replace("[they]", "they").Replace("[them]", "them").Replace("[their]", "their"),
            _ => FormatInscription(inscription, Pronoun.They)
        };
    }
}
