/*
This is mostly me trying to mess with things.
Trying toget the right movement without animation player. 
*/
using Godot;


public partial class squareBox : AnimatedSprite2D
{
	[Export]
	public float verticalMovement {get; set;} = -5f; // Change back to 50

/*
Several different vectors for convenience.	
Most of these vectors will be removed or renamed.
*/
	Vector2 _fallDirection;
	Vector2 _fallStart;
	Vector2 _fallEnd;
	Vector2 _riseStart;
	Vector2 _riseEnd;
	Vector2 _riseDirection;
	Vector2 _boxSpawn;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_riseStart = GlobalPosition;
		_riseEnd = _riseStart + _riseDirection;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_boxSpawn = new Vector2(281, 569);
		GlobalPosition = GlobalPosition.MoveToward(_boxSpawn, -verticalMovement * (float)delta);
		
		if(GlobalPosition == _boxSpawn){
			if(GlobalPosition == _riseEnd){
				if(GlobalPosition == _riseStart){
					_riseStart -= _riseEnd + _riseDirection;
				}
				else{
					_riseEnd += -_riseStart - _riseDirection;
				}
			}
		}
		if(GlobalPosition.Y > 5){
			GlobalPosition = GlobalPosition.MoveToward(_riseStart, verticalMovement * (float)delta);
		}
	}
}
