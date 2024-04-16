using Godot;
using System;

public partial class scaleneCollision : CollisionPolygon2D
{
	public float movingUp {get; set;} = 100f;

    Vector2 _targetPosition;
	Vector2 _startPosition;
	public Vector2 movingDirection;

	scaleneCollision _collision;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//_collision = GetNode<scaleneCollision>("CollisionPolygon2D");
		_startPosition = GlobalPosition;
		_targetPosition = _startPosition + movingDirection;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_targetPosition, movingUp * (float)delta);
		if(GlobalPosition.X == _targetPosition.X){
			if(GlobalPosition.X == _startPosition.X){
				_targetPosition.X += _startPosition.X + movingDirection.X;
				//_targetPosition.Y += _startPosition.Y + movingDirection.Y;
				
				//Skew = 30;
			}
		}
		else{
			_targetPosition.X = _startPosition.X;
			
			//_collision.Skew = 10;
			GD.Print("_targetPosition.Y-----" + _targetPosition.Y);
			GD.Print("GlobalPosition.Y-----" + GlobalPosition.Y);
			GD.Print("In else statement-----" + GlobalPosition.Y);
		}
		if(GlobalPosition.X == 5){
			GlobalPosition = GlobalPosition.MoveToward(_startPosition, movingUp * (float)delta);
		}
		/*if(GlobalPosition <= _targetPosition){
			RotationDegrees = 0;
			Skew = -45;
			_targetPosition.Y = _startPosition.Y;
			GD.Print("Inside GlobalPosition == 81-----" + GlobalPosition.Y);
		}*/
/*		else if(GlobalPosition.Y == _targetPosition.Y){
			_targetPosition.Y += _startPosition.Y - movingDirection.Y;
		}*/
		/*else{
			_targetPosition += GlobalPosition - movingDirection;
		}*/
	}
}
