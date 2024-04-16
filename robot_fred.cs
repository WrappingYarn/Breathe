using System;

using Godot;


public partial class robot_fred : CharacterBody2D
{
	[Export]
	public  float Speed {get; set;} = 50f;
	public  float JumpVelocity {get; set;} = 25f;

	public Vector2 _moveDirection;
	Vector2 _startPosition;		//Vertical start position
	Vector2 _targetPosition;	//Vertical target position

	Vector2 maxMove;
	Vector2 minMove;
	
	
	public bool isMoving;
	//RigidEnemy _turret;
	//Enemy _turret;
	//enemyFred _fred;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	//public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	Godot.AnimatedSprite2D _animatedSprite2D;

    public void TakeDamage(float damage, Vector2? attackFromVector)
    {
        throw new NotImplementedException();
	}

    public override void _Ready()
    {
		//_animatedSprite2D = GetNode<Godot.AnimatedSprite2D>("AnimatedSprite2D");
		_startPosition = GlobalPosition;
		_targetPosition = _startPosition + _moveDirection;
	   // base._Ready();
    }


    public override void _PhysicsProcess(double delta)
	{
		maxMove = new Vector2 (270f, 10f);
		minMove =new Vector2(1f,0f);
//		_animatedSprite2D.Play("default");
		//isMoving = true;
		if( isMoving == false){
			
			GlobalPosition = GlobalPosition.MoveToward(_targetPosition, Speed * (float)delta);
			if(GlobalPosition == _targetPosition){
				if(GlobalPosition == _startPosition){
					if(maxMove.X > _targetPosition.X){
						_targetPosition.X = maxMove.X;
					}
					_targetPosition.X += _startPosition.X + _moveDirection.X;
					//_animatedSprite2D.FlipH = false;
				}
				else{
					_targetPosition = _startPosition;
				//	_animatedSprite2D.FlipH = true;
				}
			}
			if(GlobalPosition.X == 10){
				GlobalPosition = GlobalPosition.MoveToward(_startPosition, Speed * (float)delta);
			}
		}
		/*
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();	
	}*/
	/*
	void body_entered(RigidBody2D body){
		if(body.IsInGroup("Enemy")){
			body.Show();
			if( body == null){
				body.QueueFree();
				body.Hide();
			}
		}
	}
	void area_entered(Area2D area){
		if(area.IsInGroup("Enemy")){
			_turret.Hide();
		}*/
	}
	
}


