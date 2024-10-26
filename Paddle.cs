using Godot;
using System;
public partial class Paddle : CharacterBody2D
{
	[Export] public int Speed = 400;
	private Vector2 _movementInput = Vector2.Zero;

	private Vector2 _screenSize = Vector2.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_screenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		if (_movementInput.Length() > 0)
		{
			velocity = _movementInput.Normalized() * Speed;
		}

		var newPos = Position + (velocity * (float)delta);
		newPos = new Vector2(
			x: Mathf.Clamp(newPos.X, 0, _screenSize.X),
			y: Mathf.Clamp(newPos.Y, 0, _screenSize.Y - GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.Y)
		);
		MoveAndCollide(newPos - Position);
	}
	public override void _Process(double delta)
	{
	}

	public void Start(Vector2 position)
	{
		Position = position;
	}

	private void OnUpdateMovementVector(Vector2 movementInput)
	{
		_movementInput.Y = movementInput.Y;
	}
}
