using Godot;

public partial class AiControlComponent : Node
{

	private Paddle _parentPaddle;
	
	[Signal]
	public delegate void UpdateMovementVectorEventHandler(Vector2 movementVector);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_parentPaddle = GetParent<Paddle>();
	}

	public void OnBallPositionUpdate(Vector2 position)
	{
		Vector2 movement = Vector2.Zero;
		var collisionShape2D = _parentPaddle.GetNode<CollisionShape2D>("CollisionShape2D");
		var rect = collisionShape2D.Shape.GetRect();
		if (position.Y <  _parentPaddle.Position.Y)
		{
			movement += Vector2.Up;
		} else if(position.Y >= _parentPaddle.Position.Y && (_parentPaddle.Position.Y + rect.Size.Y) >= position.Y)
		{
			movement += Vector2.Zero;
		}
		else
		{
			movement += Vector2.Down;
		}

		EmitSignal(SignalName.UpdateMovementVector, movement);
	}
}
