using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node
{
    public static SaveData SaveData { get; } = new SaveData();

    [Export] private LevelGenerator Generator;
    [Export] private UILevel UILevel;
    [Export] public UIConversationPlayer ConversationPlayer;

    private List<AGrave> Graves;
    private List<Coffin> Coffins;

    public override void _Ready()
    {
        base._Ready();
        LevelGeneratorData level = LevelGeneratorLoader.GetRandom(a => a.ID == SaveData.CurrentLevel);
        Coffins = Generator.GenerateCoffins(level);
        Graves = Generator.GenerateGraves(level, SaveData);
        UILevel.Init(level, Graves, Coffins);
        ConversationPlayer.BeginConversation(level.Events);
        Graves.ForEach(a => a.OnFilled += OnGraveFilled);
        UILevel.ScoreDisplay.SetScore(SaveData.Score, false);
        UILevel.NextLevelButton.FakeHide();
        MusicController.Play("Levels" + (SaveData.CurrentLevel > 2 ? "2" : "1"));
    }

    private void OnGraveFilled(AGrave grave, Coffin coffin, int score)
    {
        SaveData.Score += score;
        UILevel.ScoreDisplay.SetScore(SaveData.Score);
        Coffins.Remove(coffin);
        if (Coffins.Count <= 0)
        {
            UILevel.NextLevelButton.TransitionIn();
            UILevel.NextLevelButton.Pressed += FinishLevel;
        }
    }

    private void FinishLevel()
    {
        Graves.ConvertAll(a => a is SingleGrave grave ? grave : null).FindAll(a => a != null && a.Data.PersonData != null && !a.Data.PersonData.IsZombie)
            .ForEach(a =>
            {
                SaveData.Graves.AddOrSet(a.Pos, a.Data);
            });
        SaveData.CurrentLevel++;
        MusicController.Play("Silence");
        SceneController.Current.TransitionToScene("LevelIntro");
    }
}
