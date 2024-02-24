using Godot;
using System;
using System.Diagnostics;

public partial class LogInteractable : Node2D
{
	private SaveState _saveState;
	private CanvasLayer _canvas;
	[Export]
	private AnimatedSprite2D _sprite;
	[Export]
	private RigidBody2D _player;
	[Export]
	private int _logNumber;
	[Export]
	private bool _isOpen = false;
	[Export]
	private Vector2 _position;
	[Export]
	private RichTextLabel _logView;
	[Export]
	private int _textSize;
	private Log _logToDisplay;
	private int _currentPage = 0;
	private float _timeLeftToGoNext = 0f;
	private float _timeToGoNext = .25f;
	private Key _interact;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		_saveState = GetNode<SaveState>("/root/SaveState");
		var options = GetNode<OptionsState>("/root/OptionsState").GetKeyBindings();
		_interact = options["Interact"];
		_canvas = GetNode<CanvasLayer>("CanvasLayer");
		_logToDisplay = Logs.GetLogNumber(_logNumber);
		_canvas.Hide();
		if(_sprite != null)
		{
			_position = _sprite.Position;
		}
		if(_textSize != 0)
		{
			// don't know the string name. find out later.
			_logView.AddThemeFontSizeOverride("", _textSize);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_player.Position.DistanceTo(_position) < 8)
		{
			if(Input.IsKeyPressed(_interact) && _timeLeftToGoNext <= 0)
			{
				if(_isOpen)
				{
					if(_currentPage == _logToDisplay.Message.Count)
					{
						_isOpen = false;
						_currentPage = 0;
						_timeLeftToGoNext = _timeToGoNext;
						GetTree().Paused = false;
						_saveState.AddLog(_logNumber);
						_canvas.Hide();
						return;
					}
					else
					{
						_logView.Text = $"{_logToDisplay.Message[_currentPage].Item1}\n\n>{_logToDisplay.Message[_currentPage].Item2}";
						_currentPage++;
						_timeLeftToGoNext = _timeToGoNext;
					}
				}
				else
				{
					GetTree().Paused = true;
					_isOpen = true;
					_canvas.Show();
					_logView.Text = $"{_logToDisplay.Message[_currentPage].Item1}\n\n>{_logToDisplay.Message[_currentPage].Item2}";
					_currentPage++;
					_timeLeftToGoNext = _timeToGoNext;
				}
			}
			if(_timeLeftToGoNext > 0){
				_timeLeftToGoNext -= (float)delta;
			}
		}
	}
}
