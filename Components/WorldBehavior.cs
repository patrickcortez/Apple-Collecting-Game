using Godot;
using System;

public partial class WorldBehavior : Node2D
{
	// Called when the node enters the scene tree for the first time.

	private TileMapLayer Map;

	private Node player;

	private Vector2I LastKnownMousePosition = Vector2I.Zero;
	private readonly Vector2I PointingTileAtlas = new Vector2I(0, 6);

	private Vector2I LastKnownCell;

	public override void _Ready()
	{
		Map = GetNode<TileMapLayer>("Indicator");
		player = GetTree().GetFirstNodeInGroup("Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		//GD.Print("Player name: ",player.Name);

		if (Input.IsMouseButtonPressed(MouseButton.Left))
		{
			Map.EraseCell(LastKnownMousePosition);
			LastKnownMousePosition = Map.LocalToMap(Map.ToLocal(GetGlobalMousePosition()));
			
            GD.Print("Setting Cell at ", LastKnownMousePosition);
            Map.SetCell(LastKnownMousePosition, 0, PointingTileAtlas,0);
		}

		if(player is Node2D play)
		{
			if(play.GetChild<CharacterBody2D>(0) is Player actual)
			{
				GD.Print("Player found!");
				Vector2I PlayerPosition = Map.LocalToMap(Map.ToLocal(actual.GlobalPosition));
				float Distance = PlayerPosition.DistanceTo(LastKnownMousePosition);
				GD.Print("With a position of ", PlayerPosition," and a distance of: ",Distance, " and a target of ",LastKnownMousePosition);

				if (PlayerPosition==LastKnownMousePosition)
				{	
					Map.EraseCell(LastKnownMousePosition);
				}
			}
		}

	}
}
