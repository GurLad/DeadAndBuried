using Godot;
using System;

public partial class UITooltip : FadeTransition
{
    [Export] private Label NamesLabel;
    [Export] private Label InscriptionLabel;

    public void SetPerson(PersonData person)
    {
        NamesLabel.Text = person.Name + " " + person.FamilyName;
        InscriptionLabel.Text = person.Inscription;
    }

    public void ShowTooltip(PersonData person, float delay = 0)
    {
        if (delay <= 0 || interpolator.Active)
        {
            SetPerson(person);
            TransitionIn();
        }
        else
        {
            interpolator.Delay(delay);
            interpolator.OnFinish = () => ShowTooltip(person, 0);
        }
    }

    public void HideTooltip()
    {
        TransitionOut();
    }
}
