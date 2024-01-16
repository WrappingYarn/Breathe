using Godot;
using System;
using System.Diagnostics;

public partial class CameraController : Camera2D
{
	private Vector2 _maxPoint;
	private Vector2 _minPoint;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    internal void SetMinMax(Vector2 minPoint, Vector2 maxPoint)
    {
        _minPoint = minPoint;
		_maxPoint = maxPoint;
		LimitLeft = (int)minPoint.X;
		LimitTop = (int)minPoint.Y;
		LimitRight = (int)maxPoint.X;
		LimitBottom = (int)maxPoint.Y;
    }

}
