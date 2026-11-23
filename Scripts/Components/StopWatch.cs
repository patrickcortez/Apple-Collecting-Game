using Godot;
using System;

[GlobalClass]
public partial class StopWatch : Node
{
	// Called when the node enters the scene tree for the first time.

	[Signal]
	public delegate void SecondsPassedEventHandler(int Seconds);

	public double ElapsedSeconds { get; set; }

	[Export]
	public bool IsRunning { get; set; } = false;

	public override void _Ready()
	{

		ElapsedSeconds = 0.0d;

	}

	 public void Play()
	{
		IsRunning = true;
	}

	public void Reset()
	{
		ElapsedSeconds = 0.0d;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsRunning)
		{
			ElapsedSeconds += delta;

		//	GD.Print("Running at ", ElapsedSeconds);


			EmitSignal(SignalName.SecondsPassed,(int)ElapsedSeconds);
			
		}



	}
}
