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
	private Node2D PossiblePlayer;
	private AudioStreamPlayer Music;

	private AudioStreamMP3 BG = GD.Load<AudioStreamMP3>("res://Assets/Music/bg-music.mp3");

	private StopWatch stopwatch;
	private Label stoplabel;

	public void PlayBG()
	{
		Music.Stop();
		Music.Stream = BG;
		Music.Play();
	}

	public override void _Ready()
	{
		Apples = GetTree().GetNodesInGroup("Apples").Count;
		Music = GetNode<AudioStreamPlayer>("SFXPlayer");
		stopwatch = GetNode<StopWatch>("StopWatch");
		stoplabel = GetNode<Label>("CanvasLayer/lbl_clock");

		if(Apples is 0)
		{
			GD.PrintErr("Apples is Empty!");
		}

		stopwatch.SecondsPassed += (seconds) =>
		{
			stoplabel.Text = $"{seconds}";
		};

		PossiblePlayer = GetNode<Node2D>("World/Player");

		Player player = (PossiblePlayer.GetNode<CharacterBody2D>("Body") is Player play) ? play : throw new NullReferenceException("Player is not FOund!");

		player.PickedUpApple += () =>
		{
			try
			{
				GD.Print($"{player.DisplayedName} picked up an Apple!");
				Music.Stream = GD.Load<AudioStream>("res://Assets/Music/pickup.mp3");
				Music.Play();
			}catch(Exception ex)
			{
				GD.PrintErr(ex);
			}
		};

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
