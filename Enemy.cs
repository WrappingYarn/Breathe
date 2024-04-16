
using System;
using Godot;


public partial class Enemy : Area2D
{
	[Export]
	public float MoveSpeed {get; set;} = 50f;
	public float HopSpeed {get; set;} = 25f;
	
	Vector2 moveDirection;
	Vector2 startPosition;
	Vector2 targetPosition;
	Vector2 max;

	public bool isMoving;
	AnimatedSprite2D turret;
	enemyFred _freddy;
	AnimatedSprite2D _animatedSprite2D;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//turret = GetNode<AnimatedSprite2D>("Turret");
	//	_animatedSprite2D = GetNode<AnimatedSprite2D>("Turret");
		startPosition = GlobalPosition;
		targetPosition = startPosition + moveDirection;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		//_animatedSprite2D.Rotation += 1 / 24f;
		max = new Vector2(156f, 8f);
		if(isMoving == false){
			GlobalPosition = GlobalPosition.MoveToward(targetPosition, MoveSpeed*(float)delta);
			if(GlobalPosition == targetPosition){
				if(GlobalPosition == startPosition){
					/*if(max > targetPosition){
						targetPosition = max;
					}
					if(max.X > targetPosition.X){
						targetPosition.X = max.X;
					}*/
					//_animatedSprite2D.FlipH = false;
					targetPosition.X += startPosition.X + moveDirection.X; //Keep this line!!!!!!!!!!!
				}
				else{
					targetPosition.X = startPosition.X;
				}
			}
			//body_entered();
			if(GlobalPosition.X == 10){
				GlobalPosition = GlobalPosition.MoveToward(startPosition, MoveSpeed*(float)delta);
			}
			/*if(GlobalPosition.Y == 4){
				GlobalPosition = GlobalPosition.MoveToward(targetPosition, MoveSpeed * (float)delta);
			}*/
			
		}
	}

	public void body_entered(Area2D body){
		if(body.IsInGroup("Enemy")){
			body.GlobalPosition = HopSpeed* targetPosition;
			body.ToLocal(targetPosition);
		}
	}
	public void body_entered(){
		if(_animatedSprite2D.IsInGroup("Enemy")){
			//_animatedSprite2D.Rotate(12f);
			_animatedSprite2D.Play("default");
			//GlobalPosition = GlobalPosition.MoveToward(targetPosition, MoveSpeed / 20f);
		}
	}

    private string GetDebuggerDisplay()
    {
        return ToString();
    }

}
