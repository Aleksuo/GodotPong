using Godot;
using System;

public partial class Hud : CanvasLayer
{
	private const String NewGameInfoMessage = "Use W and S to move the paddle. First to 3 points wins.";
	private const String PlayerWonMessage = "You won!";
	private const String PlayerLostMessage = "You lost. Try again.";

	private Label _leftScoreLabel;
	private Label _rightScoreLabel;
	private Label _message;
	private Button _newGameButton;

	[Signal]
	public delegate void NewGameEventHandler();
	
	public override void _Ready()
	{
		_leftScoreLabel = GetNode<Label>("LeftScoreLabel");
		_rightScoreLabel = GetNode<Label>("RightScoreLabel");
		_message = GetNode<Label>("Message");
		_newGameButton = GetNode<Button>("NewGameButton");
		
		ShowNewGameScreen();
	}

	async public void ShowNewGameScreen()
	{
		_message.Show();
		_newGameButton.Show();
		_message.Text = NewGameInfoMessage;
		
		_leftScoreLabel.Hide();
		_rightScoreLabel.Hide();
	}

	async public void ShowGameOverScreen(bool playerWon)
	{
		
		_leftScoreLabel.Hide();
		_rightScoreLabel.Hide();
		_newGameButton.Hide();
		_message.Show();
		_message.Text = playerWon ? PlayerWonMessage : PlayerLostMessage;

		await ToSignal(GetTree().CreateTimer(2.0), SceneTreeTimer.SignalName.Timeout);
		ShowNewGameScreen();
	}

	public void UpdateScores(int leftScore, int rightScore)
	{
		_leftScoreLabel.Text = leftScore.ToString();
		_rightScoreLabel.Text = rightScore.ToString();
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
