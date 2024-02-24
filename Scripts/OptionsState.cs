using Godot;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

public partial class OptionsState : Node
{
	private OptionsData _data;
	private readonly string _savePath = "user://optionsFile.save";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (!FileAccess.FileExists(_savePath))
		{
			_data = new OptionsData
			{
				KeyBindings = new()
				{
					{
						"Jump",
						Key.Up
					},
					{
						"Left",
						Key.Left
					},
					{
						"Right",
						Key.Right
					},
					{
						"Interact",
						Key.Z
					},
					{
						"HoldBreath",
						Key.Space
					},
				}
			};
		}
		else
		{
			var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Read);
			_data = JsonSerializer.Deserialize<OptionsData>(file.GetLine());
			file.Close();
		}
	}
	
	public object GetProp(string propName)
	{
		return typeof(OptionsData).GetProperty(propName).GetValue(_data);
	}

	public void Save()
	{
		var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Write);
		file.StoreLine(JsonSerializer.Serialize(_data));
		file.Close();
	}

	public void SetKeyBinding(string actionName, Key binding)
	{
		_data.KeyBindings[actionName] = binding;
	}

	public Dictionary<string, Key> GetKeyBindings()
	{
		return _data.KeyBindings;
	}
}
