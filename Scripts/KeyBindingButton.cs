using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;

public partial class KeyBindingButton : Node
{
	[Export]
	private string _controlName;
	[Export]
	private RichTextLabel _description;
	[Export]
	private Array<Node> _buttons;
	[Export]
	private CanvasLayer _canvasLayer;
	private List<KeyBindingButton> _otherButtons = new();
	private SaveState _saveState;
	private OptionsState _optionsState;
	private bool _isEditing = false;
	private bool _isDisabled = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_saveState = GetNode<SaveState>("/root/SaveState");
		_optionsState = GetNode<OptionsState>("/root/OptionsState");

		foreach(var button in _buttons)
		{
			_otherButtons.Add(button as KeyBindingButton);
		}
		NameDescription();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventKey eventKey)
		{
			if (eventKey.Pressed && eventKey.Keycode == Key.Escape)
			{
				_canvasLayer.Hide();
				return;
			}
			if(eventKey.Pressed && _isEditing)
			{
				_optionsState.SetKeyBinding(_controlName, eventKey.Keycode);
				_optionsState.Save();
				_isEditing = false;
				foreach(var button in _otherButtons)
				{
					button.SetIsDisabled(false);
				}
				NameDescription();
			}
		}
	}
	
	private void OnButtonPressed()
	{
		if(_isDisabled) return;
		if(_isEditing)
		{
			foreach(var button in _otherButtons)
			{
				button.SetIsDisabled(false);
			}
			_isEditing = !_isEditing;
			return;
		}
		_isEditing = true;
		foreach(var button in _otherButtons){
			button.SetIsDisabled(true);
		}
	}

	private void SetIsDisabled(bool disable)
	{
		_isDisabled = disable;
	}

	private void NameDescription()
	{
		var keyBindings = _optionsState.GetKeyBindings();
		var currentBinding = keyBindings[_controlName];
		if(currentBinding == Key.Up || currentBinding == Key.Down || currentBinding == Key.Left || currentBinding == Key.Right)
		{
			_description.Text = currentBinding.ToString() + " Arrow";
		}
		else if(currentBinding == Key.Space)
		{
			_description.Text = currentBinding.ToString() + "bar";
		}
		else _description.Text = currentBinding.ToString();
	}

}
