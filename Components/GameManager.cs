using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

public partial class GameManager : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int Apples;
	private Node Player;

	public override void _Ready()
	{
		Apples = GetTree().GetNodesInGroup("Apples").Count;

		if(Apples is 0)
		{
			GD.PrintErr("Apples is Empty!");
		}

		Player = GetNode<Player>("World/Player");

		Label Label = Player.GetNode<Label>("Body/Counter");
		GD.Print(String.Format("Apple Amount: {0}",Apples));
		Label.Text = $"Apple Amount: {Apples}";

		Player.GetNode<Label>("Body / Counter").Text = Label.Text;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
