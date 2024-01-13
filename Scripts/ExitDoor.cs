using Godot;
using System;
using System.Diagnostics;

public partial class ExitDoor : Node
{
	private AnimatedSprite2D _animation;
	[Export]
	private DoorState _state;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animation = GetNode<AnimatedSprite2D>("DoorAnimation");
		if(_animation != null){
			if(_state == DoorState.Closed)
				_animation.Play("Closed");
			else
				_animation.Play("Opened");
		}
		else {
			Debug.WriteLine("not got animation door");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void Open()
	{
		_animation.Play("Opening");
	}
}
