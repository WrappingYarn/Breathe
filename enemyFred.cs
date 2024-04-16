//using System;

using Godot;
public partial class enemyFred : RigidBody2D
{
	[Export]
	public float walkingSpeed { get; set; } = 50; //Change back to 150f
	[Export]
	public float jumpSpeed { get; set; } = 10f; //Change back to 100f

	
	public Vector2 moveDirection;
	Vector2 startPosition;
	Vector2 targetPosition;
	
	/*[Signal]
	public delegate void _on_start_pos_ready();
	*/
	//Instanciating something
	Enemy _turret;
	
	//private CollisionShape2D _collisionShape2D;
	//Declaring some variables
	public Vector2 ScreenSize;
	

	//Some basic calculation stuff
	//private const float MASS = 50f;
	//private const float GRAVITY = 9.8f;
	
	Vector2 min;
	Vector2 max;
	
	//Stuff and stuff
	public bool isMoving;
	
	Enemy _turrets;
	robot_fred _help;
	Godot.AnimatedSprite2D _sprite;
	/*public void TakeDamage(float damage, Vector2? attackFromVector){
		throw new NotImplementedException();
	}*/
	public override void _Ready()
	{
		_sprite = GetNode<Godot.AnimatedSprite2D>("AnimatedSprite2D");
		
		startPosition = GlobalPosition;
		targetPosition = startPosition + moveDirection;
	}
	public override void _PhysicsProcess(double delta)
	{
		max = new Vector2(60, 0);
		min = new Vector2(1, 0);
		//Allows the sprites to walk back and forth
		if (isMoving == false)
		{
			//_sprite.Rotation += (1/10f);
		//LockRotation does not stop actual rotation caused by code.
			LockRotation = true;
			
			_sprite.Play("walk");	
			GD.Print("GlobalPosition.X-----" + GlobalPosition.X);
			GlobalPosition = GlobalPosition.MoveToward(targetPosition, walkingSpeed * (float)delta);
        	if(GlobalPosition == targetPosition){	
				if(GlobalPosition == startPosition){
				if(targetPosition.X < min.X){
					targetPosition.X = min.X;
				}
				if(targetPosition.X < GlobalPosition.X){
					targetPosition.X = max.X;
				}
					targetPosition.X = startPosition.X + moveDirection.X;
					GD.Print("GlobalPosition.X-----" + GlobalPosition.X);
					_sprite.FlipH = false;
				}	
			else{
				targetPosition.X = startPosition.X;
				GD.Print("GlobalPosition.X-----" + GlobalPosition.X);
				_sprite.FlipH = true;
				
			}
		}
		if(GlobalPosition.X == 10){
			GlobalPosition = GlobalPosition.MoveToward(startPosition, walkingSpeed * (float)delta);
		}
//Test code
			/*if(GlobalPosition.X == min.X){
				startPosition = GlobalPosition;
				if(startPosition == GlobalPosition){
					targetPosition = startPosition - moveDirection;
				}
			}*/
		/*	if(GlobalPosition.X == min.X ){
				targetPosition = GlobalPosition - moveDirection;
				/*targetPosition = startPosition - moveDirection;*/
			//}
			/*if(Position.X > max.X){
				isMoving =false;
				_animatedSprite2D.Play("default");
				_animatedSprite2D.FlipH = true;
			
				Position = new Vector2(300, 20);
			}*/
			GD.Print("Rotation----- " + _sprite.RotationDegrees);
		}
		GD.Print("Rotation----- " + _sprite.GlobalRotationDegrees);
	}
	public void body_entered(RigidBody2D body)
	{
		if(body.IsInGroup("Enemy")){
			body.GlobalPosition = walkingSpeed * targetPosition;
			body.ToLocal(targetPosition);
		}
	}
	public void area_entered(Area2D area){
		if(area.IsInGroup("Enemy")){
			_turret.Hide();
			area = _turret;
		}
	}
/*
//Old function
	//Movement function
	public void GeneralMovement(){
		isMoving = true;
		if(isMoving != false){
		for(int i = 0; i<5;i++){
			_animatedSprite2D.FlipH = true;
			_animatedSprite2D.Rotate(42.5f);
			i++;	
		}
			GlobalPosition = GlobalPosition.MoveToward(targetPosition, walkingSpeed * 150f);
			GD.Print("Inside first if statement");
			if(GlobalPosition == targetPosition){
				GD.Print("Inside first nested if statement");
				if(GlobalPosition == startPosition){
					GD.Print("Inside the final nested if statement");
					targetPosition = startPosition + moveDirection;
				}
			}
			else{
				targetPosition = startPosition;
			}
		}
	}
*/	
    /*public void GameOver(){
    GD.Print("Game Over");
    _animatedSprite2D.Stop();
    GlobalPosition = new Vector2(0,0);
}*/

}





