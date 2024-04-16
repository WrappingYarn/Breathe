using Godot;
using System;

public partial class Platform : Area2D
{
	[Export]
	public float _verticalMovement {get; set;} = 25f;

	public Vector2 _moveDirection;
	Vector2 _startPosition;
	Vector2 _targetPosition;
	
	Area2D _platform;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_platform = GetNode<Area2D>("Area2D");
		_startPosition.Y = GlobalPosition.Y;
		_targetPosition.Y = _startPosition.Y + _moveDirection.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_targetPosition, _verticalMovement + (float)delta);
		if(GlobalPosition == _targetPosition){
			if(GlobalPosition == _startPosition){
				_targetPosition.Y += _startPosition.Y + _moveDirection.Y; 
			}
		}
		else{
			_targetPosition += _startPosition;
		}
		if(GlobalPosition.Y == 10){
			GlobalPosition = GlobalPosition.MoveToward(_startPosition, _verticalMovement + (float)delta);
		}
	}
}
