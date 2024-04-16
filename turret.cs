using Godot;
using System;

public partial class turret : RigidBody2D
{
	[Export]
	public float upDown {get;set;} = 15F;
	// Called when the node enters the scene tree for the first time.
	AnimatedSprite2D _animatedSprite2D;
	enemyFred fred;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_animatedSprite2D.FlipH = true;
		if(Input.IsKeyPressed(Key.Space)){
			_animatedSprite2D.FlipV = true;
		}
			
		
		
	}
	public void Move(){
		if(fred != null){
			LookAt(fred.GlobalPosition);
			Vector2 direction = (GlobalPosition - fred.GlobalPosition).Normalized();
			LinearVelocity = direction * upDown;
		}
		else{
			LinearVelocity = Vector2.Zero;
		}
	}
}
