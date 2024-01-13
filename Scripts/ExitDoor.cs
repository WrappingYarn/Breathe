using Godot;
using System;
using System.Diagnostics;

public partial class ExitDoor : Node2D
{
	private AnimatedSprite2D _animation;
	[Export]
	private DoorState _state;
	[Export]
	private RigidBody2D _player;
	[Export]
	private string _nextLevel;
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
		if(_state == DoorState.Open && Input.IsKeyPressed(Key.Space))
		{
			if(_player.Position.DistanceTo(Position) < 8)
				GetTree().ChangeSceneToFile($"res://Nodes/{_nextLevel}.tscn");
		}
	}
	
	public void Open()
	{
		_animation.Play("Opening");
		_state = DoorState.Open;
	}
}
