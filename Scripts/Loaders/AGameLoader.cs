using Godot;
using System;
using System.Collections.Generic;

public abstract partial class AGameLoader<ThisType, LoadedType> : Node where ThisType : AGameLoader<ThisType, LoadedType>
{
    protected static ThisType Instance { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Instance = (ThisType)this;
    }

    protected abstract List<LoadedType> GetAllInternal(Func<LoadedType, bool> predicate);
    protected virtual LoadedType GetRandomInternal(Func<LoadedType, bool> predicate) => GetAllInternal(predicate).RandomItemInList();

    public static List<LoadedType> GetAll(Func<LoadedType, bool> predicate) => Instance.GetAllInternal(predicate);
    public static LoadedType GetRandom(Func<LoadedType, bool> predicate) => Instance.GetRandomInternal(predicate);
}
