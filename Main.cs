using Godot;
using System;

public partial class Main : Node
{
	private Paddle _leftPaddle;
	private Paddle _rightPaddle;
	private Ball _ball;
	private Marker2D _leftStartPos;
	private Marker2D _rightStartPos;
	private Marker2D _ballStartPos;
	private Hud _hud;

	private int _leftScore;
	private int _rightScore;
	public override void _Ready()
	{
		_leftStartPos = GetNode<Marker2D>("LeftStartPos");
		_rightStartPos = GetNode<Marker2D>("RightStartPos");
		_ballStartPos = GetNode<Marker2D>("BallStartPos");
		_leftPaddle = GetNode<Paddle>("LeftPaddle");
		_rightPaddle = GetNode<Paddle>("RightPaddle");
		_ball = GetNode<Ball>("Ball");
		_hud = GetNode<Hud>("HUD");
		HideAll();
	}

	private void HideAll()
	{
		_leftPaddle.Hide();
		_rightPaddle.Hide();
		_ball.Hide();
	}

	private void ShowAll()
	{
		_leftPaddle.Show();
		_rightPaddle.Show();
		_ball.Show();
	}

	private void ResetPositions()
	{
		_leftPaddle.Position = _leftStartPos.Position;
		_rightPaddle.Position = _rightStartPos.Position;
		_ball.Reset(_ballStartPos.Position);
	}

	async public void NewGame()
	{
		GD.Print("Starting new game");
		ResetPositions();
		ShowAll();
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		_ball.Start();
	}
	
	public override void _Process(double delta)
	{
	}

	async private void OnBallExitedLeftSide()
	{
		GD.Print("Ball exited left side");
		_rightScore += 1;
		_hud.UpdateScores(_leftScore, _rightScore);
		ResetPositions();
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		_ball.Start();
	}

	async private void OnBallExitedRightSide()
	{
		GD.Print("Ball exited right side");
		_leftScore += 1;
		_hud.UpdateScores(_leftScore, _rightScore);
		ResetPositions();
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		_ball.Start();
	}
}
