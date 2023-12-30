using Godot;
using System;

public partial class BreathingController : Node
{
    [Export]
    private ActiveStatus _playerActivity = ActiveStatus.Resting;
    private float _breath = 1;
    private bool _isHoldingBreath = false;
    private float _holdingBreathTimer = 0;
    private float _holdingBreathLength = 5;
    private bool _canHoldBreath = true;
    private float _oldTime;
    private float _breathingRange = .1f;
    private float _restingRate = 2f;
    private float _runningRate = .5f;
    private float _walkingRate = 1.25f;
    private float _exaustedRate = .25f;
    private float _breathingOutTakesLonger = 1.5f;
    private float _respitoryRate;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_isHoldingBreath)
            return;

        GetRespitoryRate();
        var currentTime = Time.time;
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
            //_lightScource.range += _breath * _breathingRange;
            if (_oldTime + variance < currentTime)
            {
                _breath = _breath * -1;
                _oldTime = Time.time;
            }
        }
        //_lightcollider.radius = _lightScource.range / 2;
	}
	private void HoldBreath(bool isHoldingBreath)
    {
        if (_canHoldBreath && isHoldingBreath && _holdingBreathTimer == 0)
        {
            _holdingBreathTimer = Time.time;
            _isHoldingBreath = isHoldingBreath;
        }
        else if (isHoldingBreath && _holdingBreathTimer + _holdingBreathLength < Time.time)
        {
            _isHoldingBreath = false;
            _canHoldBreath = false;
            StartCoroutine("CatchBreathTimer", 5f);
        }
        else if (!isHoldingBreath)
        {
            _isHoldingBreath = false;
        }
    }

    private IEnumerator CatchBreathTimer(float time)
    {
        yield return new WaitForSeconds(time);
        _canHoldBreath = true;
        _holdingBreathTimer = 0;
    }

    private void GetRespitoryRate()
    {
        switch (_playerActivity)
        {
            case (ActiveStatus.Resting):
                _respitoryRate = _restingRate;
                break;
            case (ActiveStatus.Running):
                _respitoryRate = _runningRate;
                break;
            case (ActiveStatus.Walking):
                _respitoryRate = _walkingRate;
                break;
            case (ActiveStatus.Exausted):
                _respitoryRate = _exaustedRate;
                break;
            default:
                break;
        }
    }
}
