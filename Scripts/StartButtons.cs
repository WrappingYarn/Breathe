using Godot;
using System;

public partial class StartButtons : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnPressedStart()
	{
		GetTree().ChangeSceneToFile("res://Nodes/TestMap.tscn");
	}
	
	private void OnPressedOptions()
	{
		// Replace with function body.
	}
	
	private void OnPressedCredits()
	{
		// Replace with function body.
	}
}
