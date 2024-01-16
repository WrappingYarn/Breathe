using Godot;
using System;
using System.Diagnostics;

public partial class BreathingController : Node
{
	[Export]
	private ActiveStatus _playerActivity = ActiveStatus.Resting;
	[Export]
	private float RangeDifference = 38f;
	[Export]
	private RigidBody2D _player;
	private PointLight2D _light;
	private CollisionShape2D _lightColider;
	private float _breath = 1;
	private float _breathSpeed = 1.5f;
	private bool _isHoldingBreath = false;
	private float _holdingBreathTimer = 0;
	private float _holdingBreathLength = 5;
	private bool _canHoldBreath = true;
	private float _oldTime = 0f;
	[Export]
	private float _breathingRange = 5f;
	private float _restingRate = 2f;
	private float _runningRate = .5f;
	private float _walkingRate = 1.25f;
	private float _exaustedRate = .25f;
	private float _breathingOutTakesLonger = 1.5f;
	private float _respitoryRate;
	private bool _activateSwitch = false;
	private Area2D _area;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_light = GetNode<PointLight2D>("PointLight2D");
		_lightColider = GetNode<CollisionShape2D>("CollisionShape2D");
		_light.Scale = Vector2.Zero;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_isHoldingBreath)
			return;

		GetRespitoryRate();
		if (_breath != 0)
		{
			float variance;
			if (_breath < 0)
			{
				variance = _respitoryRate * _breathingOutTakesLonger;
			}
			else
			{
				variance = _respitoryRate;
			}
			if (_oldTime >= variance)
			{
				_breath *= -1;
				_oldTime = 0f;
			}
			else
			{ 
				_light.Scale += (_breath * Vector2.One * (float)delta) * (_breathingRange / variance) * 10;
				if(_light.Scale <= Vector2.Zero)
				{
					_light.Scale = Vector2.Zero;
				}
				_oldTime += (float)delta;
			}
		}
		if(_activateSwitch){
			if(_area.Position.DistanceTo(_player.Position)-10 < _light.Scale.Y){
				_activateSwitch = false;
				var exit = _area as DoorSwitch;
				exit.Open();
			}
		}
	}
	private void HoldBreath(bool isHoldingBreath)
	{
		// if (_canHoldBreath && isHoldingBreath && _holdingBreathTimer == 0)
		// {
		//     _holdingBreathTimer = Time.time;
		//     _isHoldingBreath = isHoldingBreath;
		// }
		// else if (isHoldingBreath && _holdingBreathTimer + _holdingBreathLength < Time.time)
		// {
		//     _isHoldingBreath = false;
		//     _canHoldBreath = false;
		//     StartCoroutine("CatchBreathTimer", 5f);
		// }
		// else if (!isHoldingBreath)
		// {
		//     _isHoldingBreath = false;
		// }
	}

	// private IEnumerator CatchBreathTimer(float time)
	// {
	//     yield return new WaitForSeconds(time);
	//     _canHoldBreath = true;
	//     _holdingBreathTimer = 0;
	// }

	private void GetRespitoryRate()
	{
		switch (_playerActivity)
		{
			case ActiveStatus.Resting:
				_respitoryRate = _restingRate;
				break;
			case ActiveStatus.Running:
				_respitoryRate = _runningRate;
				break;
			case ActiveStatus.Walking:
				_respitoryRate = _walkingRate;
				break;
			case ActiveStatus.Exausted:
				_respitoryRate = _exaustedRate;
				break;
			default:
				break;
		}
	}

	public void Activity(ActiveStatus activity)
	{
		_playerActivity = activity;
	}

	private void _on_area_entered(Area2D area)
	{
		_activateSwitch = true;
		_area = area;
	}

	private void _on_area_exited(Area2D area)
	{
		_activateSwitch = false;
	}
}
