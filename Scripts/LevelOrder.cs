using System.Collections.Generic;

public static class LevelOrder
{
    private static readonly List<string> _levels = new()
    {
        "TestMap"
    };
    public static string Get(int levelNumber)
    {
        return _levels[levelNumber];
    }

    public static int Get(string levelName)
    {
        return _levels.IndexOf(levelName);
    }
}