using Godot;
using System;

public partial class StartButtons : Node
{
	private SaveState _saveState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_saveState = GetNode<SaveState>("/root/SaveState");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	private void OnPressedStart()
	{
		var lvlNumber = (int)_saveState.GetProp("LevelNumber");
		GetTree().ChangeSceneToFile($"res://Nodes/{LevelOrder.Get(lvlNumber)}.tscn");
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
