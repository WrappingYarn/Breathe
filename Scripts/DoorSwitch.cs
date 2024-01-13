using Godot;
using System;
using System.Diagnostics;

public partial class DoorSwitch : Area2D
{
	private AnimatedSprite2D _animation;
	private ExitDoor _door;
	[Export]
	public Node DoorNode { get; set; }
	private float _timeToOpenDoor = 0f;
	private bool _charging = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animation = GetNode<AnimatedSprite2D>("DoorSwitchAnimation");
		if(_animation != null){
			_animation.Play("Off");
		}
		else {
			Debug.WriteLine("not got animation switch");
		}
		_door = DoorNode as ExitDoor;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(_charging){
			_timeToOpenDoor += (float)delta;
			if(_timeToOpenDoor > 1){
				_door.Open();
				_charging = false;
			}
		}
	}
	
	public void Open()
	{
		if(_timeToOpenDoor == 0f){
			_animation.Play("Charging");
			_charging = true;
		}
	}
}
