using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class Player : CharacterBody2D
{
	// Called when the node enters the scene tree for the first time.

	
	public override void _Ready()
	{
		// get all apples in the scene tree
		Apples = GetTree().GetNodesInGroup("Apples");
		LastKnownMouse = GlobalPosition;
		ThisSprite = GetNode<Sprite2D>("Sprite");
		ThisLabel = GetNode<Label>("Counter");
		ThisLabel.Text = $"Apples: {Apples.Count}";
        ListenToApples();
    }



	[Export]
	public float MovementSpeed { get; set; } =100.0f;
	
	// list of apples in the map
	private Array<Node> Apples;

	private Vector2 LastKnownMouse;
	private Vector2 LastKnownDirection;

	// textures
	private Texture2D left { get; } = GD.Load<Texture2D>("res://Assets/Seymor-Left.png");
    private Texture2D right { get; } = GD.Load<Texture2D>("res://Assets/Seymor-Right.png");
    
	//components
	private Sprite2D ThisSprite;
	private Label ThisLabel;
	

	[Export]
	public float ArrivalThreshold { get; set; } = 2.5f;

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	private void UpdateSprite()
	{
        if (LastKnownDirection.X > 0.0f)
        {
            ThisSprite.Texture = left;
        }
        else if (LastKnownDirection.X < 0.0f)
        {
            ThisSprite.Texture = right;
        }
    }

	private void ListenToApples()
	{
		foreach(Node node in Apples) // invalid cast exception, ffs
		{
			if(node is AppleBehavior Apple)
			{
                Apple.HoverOnApple += () =>
                {
                    GD.Print("Player Cursor changed to Pointer");
                    Input.SetDefaultCursorShape(Input.CursorShape.PointingHand);
                };

                Apple.LeftApple += () =>
                {
					GD.Print("Player Cursor changed to Arrow");
                    Input.SetDefaultCursorShape(Input.CursorShape.Arrow);
                };
			}
			else
			{
				GD.Print($"Node name: {node.Name}");
			}
		}
	}

	public override void _Process(double delta)
	{



		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{

			

			Vector2 MousePosition = GetGlobalMousePosition();
			LastKnownMouse = MousePosition;
			float distanceToPos = GlobalPosition.DistanceTo(MousePosition);
			GD.Print($"Distance of player to target: {distanceToPos}");

			if (distanceToPos > ArrivalThreshold)
			{
				Vector2 Direction = GlobalPosition.DirectionTo(MousePosition);
				LastKnownDirection = Direction;

				Velocity = Direction * MovementSpeed;

			
			}
			else
			{
				Velocity = Vector2.Zero;
			}
			

		}else if(this.GlobalPosition != LastKnownMouse)
		{

			UpdateSprite();

			if(GlobalPosition.DistanceTo(LastKnownMouse) > ArrivalThreshold)
			{
				Velocity = LastKnownDirection * MovementSpeed;
			}
			else
			{
				Velocity = Vector2.Zero;
			}

        }
		//else
		//{
		//	Velocity = Vector2.Zero;
		//}

        MoveAndSlide();
    }
}
