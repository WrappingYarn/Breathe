using System.Collections.Generic;

public class SaveData
{
	public int LevelNumber { get; set; }
	public string LevelName { get; set; }
	public List<int> LogsRead { get; set; } = new List<int>();
}
