using Godot;
using System;

public partial class PlayerControlComponent : Node
{

	[Signal]
	public delegate void UpdateMovementVectorEventHandler(Vector2 movementVector);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 movement = Vector2.Zero;
		if (Input.IsActionPressed("move_up"))
		{
			movement += Vector2.Up;
		}

		if (Input.IsActionPressed("move_down"))
		{
			movement += Vector2.Down;
		}

		EmitSignal(SignalName.UpdateMovementVector, movement);
	}
	
	
}
