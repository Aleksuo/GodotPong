using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	private Vector2 _velocity = Vector2.Zero;

	[Signal]
	public delegate void BallExitedRightSideEventHandler();

	[Signal]
	public delegate void BallExitedLeftSideEventHandler();
	
	[Export]
	public int BaseSpeed = 450;

	private Vector2 _screenSize = Vector2.Zero;

	public void Reset(Vector2 position)
	{
		_velocity = Vector2.Zero;
		Position = position;
	}

	public void Start()
	{
		var randInt = GD.RandRange(1, 10);
		var xDirection = (randInt % 2 == 0) ? 1 : -1;
		var randomDirection = new Vector2(xDirection, (float)GD.RandRange(-0.75, 0.75)).Normalized();
		_velocity = randomDirection * BaseSpeed;
		GD.Print(_velocity.ToString());
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(_velocity * (float)delta);
		if (collision != null)
		{
			GD.Print("Collided");
			_velocity = _velocity.Bounce(collision.GetNormal());
		}
		var collisionShape2d = GetNode<CollisionShape2D>("CollisionShape2D");
		var height = collisionShape2d.Shape.GetRect().Size.Y;
		if (Position.Y <= 0)
		{
			SetPosition(new Vector2(Position.X, 0));
			_velocity = _velocity.Bounce(Vector2.Down);
		}else if ((Position.Y + height) >= _screenSize.Y)
		{
			SetPosition(new Vector2(Position.X, _screenSize.Y - height));
			_velocity = _velocity.Bounce(Vector2.Up);
		}
	}

	private void OnScreenExited()
	{
		if (Position.X < 0)
		{
			EmitSignal(SignalName.BallExitedLeftSide);
		}
		else
		{
			EmitSignal(SignalName.BallExitedRightSide);
		}
	}
	
	
}
