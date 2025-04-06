using Godot;

public partial class PlayerControlComponent : Node
{

	[Signal]
	public delegate void UpdateMovementVectorEventHandler(Vector2 movementVector);
	
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
