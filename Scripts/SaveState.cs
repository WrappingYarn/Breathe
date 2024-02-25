using Godot;
using System;
using System.Diagnostics;
using System.Text.Json;

public partial class SaveState : Node
{
	private SaveData _data;
	private readonly string _savePath = "user://saveFile.save";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (!FileAccess.FileExists(_savePath))
		{
			_data = new SaveData
			{
				LevelName = LevelOrder.Get(0),
				LevelNumber = 0
			};
		}
		else
		{
			var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Read);
			_data = JsonSerializer.Deserialize<SaveData>(file.GetLine());
			file.Close();
		}
	}
	
	public object GetProp(string propName)
	{
		return typeof(SaveData).GetProperty(propName).GetValue(_data);
	}

	public void LoadLevel()
	{
		GetTree().ChangeSceneToFile($"res://Nodes/{_data.LevelName}.tscn");
	}

	public void Save()
	{
		var file = FileAccess.Open(_savePath, FileAccess.ModeFlags.Write);
		file.StoreLine(JsonSerializer.Serialize(_data));
		file.Close();
	}

	public void SetLevel(int levelNumber)
	{
		_data.LevelNumber = levelNumber;
		_data.LevelName = LevelOrder.Get(levelNumber);
	}

	public void AddLog(int logNumber)
	{
		_data.LogsRead.Add(logNumber);
	}

}
