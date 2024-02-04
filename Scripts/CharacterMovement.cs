using Godot;

public partial class CharacterMovement : RigidBody2D
{
	[Export]
	public float Speed { get; set; } = 150f;
	[Export]
	public float JumpSpeed { get; set; } = 300f;
	[Export]
	private Vector2 _maxPoint;
	[Export]
	private Vector2 _minPoint;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var camera = GetNode<Camera2D>("Camera2D");
		((camera as Node) as CameraController).SetMinMax(_minPoint, _maxPoint);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		var isMoving = false;
		if (Input.IsKeyPressed(Key.Right))
		{
			animatedSprite2D.FlipH = false;
			isMoving = true;
			float move = Speed;
			SetAxisVelocity(Vector2.Right * move);
		}
		else if (Input.IsKeyPressed(Key.Left))
		{
			animatedSprite2D.FlipH = true;
			isMoving = true;
			float move = Speed;
			SetAxisVelocity(Vector2.Left * move);
		}
		if (Input.IsKeyPressed(Key.Up) && LinearVelocity.Y == 0)
		{
			isMoving = true;
			float move = JumpSpeed;
			SetAxisVelocity(Vector2.Up * move);
		}

		if (isMoving)
			animatedSprite2D.Play();
		else
			animatedSprite2D.Stop();


		// if (velocity.X != 0)
		// {
		// 	animatedSprite2D.Animation = "walk";
		// 	animatedSprite2D.FlipV = false;
		// 	// See the note below about boolean assignment.
		// 	animatedSprite2D.FlipH = velocity.X < 0;
		// }
		// else if (velocity.Y != 0)
		// {
		// 	animatedSprite2D.Animation = "up";
		// 	animatedSprite2D.FlipV = velocity.Y > 0;
		// }
	}
}
