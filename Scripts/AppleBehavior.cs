using Godot;
using System;




public partial class AppleBehavior : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.

	

	[Signal]
	public delegate void HoverOnAppleEventHandler();

	[Signal]

	public delegate void LeftAppleEventHandler();

	[Signal]
	public delegate void AppleClickedEventHandler();

	[Export]
	public bool Enabled { get; set; } = true;

	private Node Parent;
 
	public override void _Ready()
	{
		Parent = GetParent();
		MouseEntered += () =>
		{
			
			GD.Print($"{Parent.Name} Mouse Entered");
			Error Result =  EmitSignal(SignalName.HoverOnApple);
			GD.Print($"{Parent.Name} Result: {Result}");
		};

		MouseExited += () =>
		{
            GD.Print($"{Parent.Name} Mouse Left");
            Error Result = EmitSignal(SignalName.LeftApple);
            GD.Print($"{Parent.Name} Result: {Result}");
        };

		InputEvent += (view, Inputevent, shadeIdx) =>
		{
			
			EmitSignal(SignalName.AppleClicked);
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
