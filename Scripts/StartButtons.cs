using Godot;
using System;

public partial class StartButtons : Node
{
	[Export]
	private CanvasLayer _optionsCanvas;
	private SaveState _saveState;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_saveState = GetNode<SaveState>("/root/SaveState");
		_optionsCanvas.Hide();
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
		_optionsCanvas.Show();
	}
	
	private void OnPressedCredits()
	{
		// Replace with function body.
	}
}
