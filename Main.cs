using Godot;

public partial class Main : Node
{
	private Paddle _leftPaddle;
	private Paddle _rightPaddle;
	private Ball _ball;
	private Line2D _centerLine;
	private Marker2D _leftStartPos;
	private Marker2D _rightStartPos;
	private Marker2D _ballStartPos;
	private Hud _hud;

	private int _leftScore;
	private int _rightScore;

	[Export] public int WinScore = 3;
	public override void _Ready()
	{
		_leftStartPos = GetNode<Marker2D>("LeftStartPos");
		_rightStartPos = GetNode<Marker2D>("RightStartPos");
		_ballStartPos = GetNode<Marker2D>("BallStartPos");
		_leftPaddle = GetNode<Paddle>("LeftPaddle");
		_rightPaddle = GetNode<Paddle>("RightPaddle");
		_ball = GetNode<Ball>("Ball");
		_centerLine = GetNode<Line2D>("CenterLine");
		_hud = GetNode<Hud>("HUD");
		HideAll();
	}

	private void HideAll()
	{
		_leftPaddle.Hide();
		_rightPaddle.Hide();
		_ball.Hide();
		_centerLine.Hide();
	}

	private void ShowAll()
	{
		_leftPaddle.Show();
		_rightPaddle.Show();
		_ball.Show();
		_centerLine.Show();
	}

	private void ResetPositions()
	{
		_leftPaddle.Position = _leftStartPos.Position;
		_rightPaddle.Position = _rightStartPos.Position;
		_ball.Reset(_ballStartPos.Position);
	}

	private void ResetScores()
	{
		_leftScore = 0;
		_rightScore = 0;
		_hud.UpdateScores(_leftScore, _rightScore);
	}

	async private void StartKickoff()
	{
		await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
		_ball.Start();
	}

	public void NewGame()
	{
		ResetScores();
		ResetPositions();
		ShowAll();
		StartKickoff();
	}

	private bool IsGameOver()
	{
		return _leftScore >= WinScore || _rightScore >= WinScore;
	}

	private void OnBallExitedLeftSide()
	{
		_rightScore += 1;
		_hud.UpdateScores(_leftScore, _rightScore);
		if (IsGameOver())
		{
			ResetPositions();
			HideAll();
			_hud.ShowGameOverScreen(false);
		}
		else
		{
			ResetPositions();
			StartKickoff();
		}
	}

	private void OnBallExitedRightSide()
	{
		_leftScore += 1;
		_hud.UpdateScores(_leftScore, _rightScore);
		if (IsGameOver())
		{
			ResetPositions();
			HideAll();
			_hud.ShowGameOverScreen(true);
		}
		else
		{
			ResetPositions();
			StartKickoff();
		}
	}

	private void OnBallHit()
	{
		GetNode<AudioStreamPlayer>("BallHit").Play();
	}
}
