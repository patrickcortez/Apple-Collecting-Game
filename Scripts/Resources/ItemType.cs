using Godot;
using System;

[GlobalClass]
public partial class ItemType : Resource
{
    [Export]
    public string ItemName { get; set; }

    [Export]
    public string ItemDescription { get; set; }

    [Export]
    public int MaxStackSize { get; set; } = 10;

    public ItemType()
    {
    }
}
