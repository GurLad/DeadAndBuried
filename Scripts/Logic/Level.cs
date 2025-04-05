using Godot;
using System;
using System.Collections.Generic;

public partial class Level : Node
{
    private static SaveData SaveData { get; } = new SaveData();

    [Export] private LevelGenerator Generator;
    [Export] private UILevel uiLevel;
    [Export] private UIConversationPlayer conversationPlayer;

    private List<AGrave> Graves;
    private List<Coffin> Coffins;

    public override void _Ready()
    {
        base._Ready();
        LevelGeneratorData level = LevelGeneratorLoader.GetRandom(a => a.ID == SaveData.CurrentLevel);
        Coffins = Generator.GenerateCoffins(level);
        Graves = Generator.GenerateGraves(level, SaveData);
        uiLevel.Init(Graves, Coffins);
        conversationPlayer.BeginConversation(level.Events);
    }
}
