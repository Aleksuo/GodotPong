using Godot;
using System;

public partial class Main : Node
{
	// Called when the node enters the scene tree for the first time.
	private Paddle _leftPaddle;
	private Paddle _rightPaddle;
	private Ball _ball;
	private Marker2D _leftStartPos;
	private Marker2D _rightStartPos;
	private Marker2D _ballStartPos;
	public override void _Ready()
	{
		_leftStartPos = GetNode<Marker2D>("LeftStartPos");
		_rightStartPos = GetNode<Marker2D>("RightStartPos");
		_ballStartPos = GetNode<Marker2D>("BallStartPos");
		_leftPaddle = GetNode<Paddle>("LeftPaddle");
		_rightPaddle = GetNode<Paddle>("RightPaddle");
		_ball = GetNode<Ball>("Ball");
		
		NewGame();
	}

	public void NewGame()
	{
		_leftPaddle.Position = _leftStartPos.Position;
		_rightPaddle.Position = _rightStartPos.Position;
		_ball.Reset(_ballStartPos.Position);
		_ball.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBallExitedLeftSide()
	{
		GD.Print("Ball exited left side");
	}

	private void OnBallExitedRightSide()
	{
		GD.Print("Ball exited right side");
	}
}
