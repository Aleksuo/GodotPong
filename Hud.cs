using Godot;
using System;

public partial class Hud : CanvasLayer
{
	private const String NewGameInfoMessage = "Use W and S to move the paddle.";

	private Label _leftScoreLabel;
	private Label _rightScoreLabel;
	private Label _message;
	private Button _newGameButton;

	[Signal]
	public delegate void NewGameEventHandler();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_leftScoreLabel = GetNode<Label>("LeftScoreLabel");
		_rightScoreLabel = GetNode<Label>("RightScoreLabel");
		_message = GetNode<Label>("Message");
		_newGameButton = GetNode<Button>("NewGameButton");

		_message.Text = NewGameInfoMessage;
		_leftScoreLabel.Hide();
		_rightScoreLabel.Hide();
	}

	public void UpdateScores(int leftScore, int rightScore)
	{
		_leftScoreLabel.Text = leftScore.ToString();
		_rightScoreLabel.Text = rightScore.ToString();
	}
	
	

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnNewGameButtonPressed()
	{
		_newGameButton.Hide();
		_message.Hide();
		_leftScoreLabel.Show();
		_rightScoreLabel.Show();
		EmitSignal(SignalName.NewGame);
	}
}
