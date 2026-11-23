using Godot;
using System;

public partial class ParentPlayer : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public StopWatch timer;
	
	public override void _Ready()
	{
		timer = (GetTree().GetFirstNodeInGroup("clocks") is StopWatch watch) ? watch : null;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
